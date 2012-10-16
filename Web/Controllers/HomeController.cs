using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Mx.Ipn.Esime.Statistics.Core.Base;
using Mx.Ipn.Esime.Statistics.GroupedData;
using Mx.Ipn.Esime.Statistics.UngroupedData;
using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Helpers;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly static Random rnd;

        private readonly static StandardKernel kernel;
        
        static HomeController()
        {
            rnd = new Random(DateTime.Now.Millisecond);
            kernel = new StandardKernel();
        }
        
        public static IEnumerable<double> GetRandomDataSample(int size)
        {
            for (int i = 1; i <= size; i++)
            {
                yield return rnd.Next(57, 180) + Math.Round(rnd.NextDouble(), rnd.Next(0, 5));
            }
        }
        public ActionResult Index(int size)
        {
            ViewBag.Size = size;
            ViewBag.Title = "Estadística Descriptiva";
            return View();
        }

        [HttpPost]
        public ActionResult Report(string data, string type)
        {
            ViewBag.Title = "Análisis";
            var values = data.Split(',').Select(number => Double.Parse(number)).ToList();
            kernel.Rebind<GroupedXileInquirer>().ToSelf().InSingletonScope();
            kernel.Rebind<GroupedCentralTendecyInquirer>().ToSelf().InSingletonScope();
            kernel.Rebind<GroupedDispersionInquirer>().ToSelf().InSingletonScope();
            kernel.Rebind<DataDistributionFrequencyInquirer>().ToSelf().InSingletonScope();
            kernel.Rebind<UngroupedXileInquirer>().ToSelf().InSingletonScope();
            kernel.Rebind<UngroupedCentralTendecyInquirer>().ToSelf().InSingletonScope();
            kernel.Rebind<UngroupedDispersionInquirer>().ToSelf().InSingletonScope();
            kernel.Rebind<StatisticsInquirerBase>().To<UngroupedStatisticsInquirer>().InSingletonScope().Named("ungrouped");
            kernel.Rebind<StatisticsInquirerBase>().To<GroupedStatisticsInquirer>().InSingletonScope().Named("grouped");
            kernel.Rebind<DataContainer>().ToMethod(context => new DataContainer(values)).InSingletonScope();

            ViewBag.Data = data;
            ViewBag.Type = type;
            var instance = kernel.Get<StatisticsInquirerBase>(type);
           
            return View(instance);
        }

        [ChildActionOnly]
        public PartialViewResult Histogram()
        {
            Highcharts chart = new Highcharts("histogram")
                .SetXAxis(new XAxis
                          {
                    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                })
                    .SetSeries(new Series
                               {
                        Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
                    });
            
            return PartialView("HighChart", chart);
        }

        [ChildActionOnly]
        public PartialViewResult Ogive()
        {
            Highcharts chart = new Highcharts("ogive")
                .SetXAxis(new XAxis
                          {
                    Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
                })
                    .SetSeries(new Series
                               {
                        Data = new Data(new object[] { 29.9, 71.5, 106.4, 129.2, 144.0, 176.0, 135.6, 148.5, 216.4, 194.1, 95.6, 54.4 })
                    });
            
            return PartialView("HighChart", chart);
        }
    }
}