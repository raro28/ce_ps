using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Mx.Ipn.Esime.Statistics.Core.Base;
using Mx.Ipn.Esime.Statistics.GroupedData;
using Mx.Ipn.Esime.Statistics.UngroupedData;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly static Random rnd;
        
        static HomeController()
        {
            rnd = new Random(DateTime.Now.Millisecond);
        }
        
        public static IEnumerable<double> GetRandomDataSample(int size)
        {
            for (int i = 1; i <= size; i++)
            {
                yield return rnd.Next(57, 180) + Math.Round(rnd.NextDouble(), 2);
            }
        }
        public ActionResult Index(int? size)
        {
            ViewBag.Size = size ?? 100;
            ViewBag.Title = "Index";
            return View();
        }

        [HttpPost]
        public ActionResult Report(string data, string type)
        {
            ViewBag.Title = "Report";
            var values = data.Split(',').Select(number => Double.Parse(number)).ToList();
            var kernel = new StandardKernel();
            kernel.Bind<GroupedXileInquirer>().ToSelf().InSingletonScope();
            kernel.Bind<GroupedCentralTendecyInquirer>().ToSelf().InSingletonScope();
            kernel.Bind<GroupedDispersionInquirer>().ToSelf().InSingletonScope();
            kernel.Bind<DataDistributionFrequencyInquirer>().ToSelf().InSingletonScope();
            kernel.Bind<UngroupedXileInquirer>().ToSelf().InSingletonScope();
            kernel.Bind<UngroupedCentralTendecyInquirer>().ToSelf().InSingletonScope();
            kernel.Bind<UngroupedDispersionInquirer>().ToSelf().InSingletonScope();
            kernel.Bind<StatisticsInquirerBase>().To<UngroupedStatisticsInquirer>().InSingletonScope().Named("ungrouped");
            kernel.Bind<StatisticsInquirerBase>().To<GroupedStatisticsInquirer>().InSingletonScope().Named("grouped");
            kernel.Rebind<DataContainer>().ToMethod(context => new DataContainer(values)).InSingletonScope();

            ViewBag.Data = data;
            ViewBag.Type = type;
            var instance = kernel.Get<StatisticsInquirerBase>(type);
           
            return View(instance);
        }

        [ChildActionOnly]
        public PartialViewResult GroupedReport(GroupedStatisticsInquirer model)
        {
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult UngroupedReport(UngroupedDispersionInquirer model)
        {
            return PartialView(model);
        }
    }
}