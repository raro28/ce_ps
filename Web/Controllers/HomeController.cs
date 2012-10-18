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
using DotNet.Highcharts.Enums;
using System.Drawing;
using Web.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly static Random rnd;

        private readonly static StandardKernel globalKernel;

        static HomeController()
        {
            rnd = new Random(DateTime.Now.Millisecond);
            globalKernel = new StandardKernel();
            globalKernel.Bind<StandardKernel>().ToMethod(ctx => {
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

                return kernel;
            });
        }

        public string GetRandomDataSample(int size)
        {
            var result = "";

            for (int i = 1; i <= size; i++)
            {
                result += rnd.Next(57, 180) + Math.Round(rnd.NextDouble(), rnd.Next(0, 5)) + ",";
            }

            result = result.Substring(0, result.Length - 1);

            return result;
        }

        public ActionResult Index(int sample = 115)
        {
            ViewBag.Title = "Estadística Descriptiva";

            var model = new StubModel{
                Data = GetRandomDataSample(sample),
                Type = "ungrouped"
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string Data, string Type)
        {
            var values = Data.Split(',').Select(number => Double.Parse(number)).ToList();

            var kernel = globalKernel.Get<StandardKernel>();
            kernel.Bind<DataContainer>().ToMethod(context => new DataContainer(values)).InSingletonScope();

            var inquirer = kernel.Get<StatisticsInquirerBase>(Type);

            Session.Add("inquirer", inquirer);
            Session.Add("data", Data);

            ViewBag.Type = Type;
            return View("Report");
        }

        [ChildActionOnly]
        public PartialViewResult CentralTendencySummary()
        {
            dynamic statistics = Session["inquirer"];

            var model = new CentralTendencySummaryModel{
                Mean = statistics.GetMean(),
                Median = statistics.GetMedian(),
                Modes = statistics.GetModes()
            };

            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult DispersionSummary()
        {
            dynamic statistics = Session["inquirer"];

            var model = new DispersionSummaryModel{
                DataRange = statistics.GetDataRange(),
                InterQuartileRange = statistics.GetInterQuartileRange(),
                InterDecileRange = statistics.GetInterDecileRange(),
                InterPercentileRange = statistics.GetInterPercentileRange(),
                AbsoluteDeviation = statistics.GetAbsoluteDeviation(),
                Variance = statistics.GetVariance(),
                StandarDeviation = statistics.GetStandarDeviation(),
                CoefficientOfVariation = statistics.GetCoefficientOfVariation() * 100,
                CoefficientSymmetry = statistics.GetCoefficientOfSymmetry(),
                CoefficientOfKourtosis = statistics.GetCoefficientOfKourtosis()
            };
            
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult DataSummary()
        {
            dynamic statistics = Session["inquirer"];

            var model = new DataSummaryModel{
                Data = Session["data"].ToString(),
                DataCount = statistics.Container.DataCount,
                DecimalCount = statistics.Container.DataPrecision,
                Precision = statistics.Container.DataPrecisionValue,
                Max = statistics.Container.Max,
                Min = statistics.Container.Min
            };
            
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult GroupedReport()
        {
            dynamic statistics = Session["inquirer"];
            statistics.AddAcumulatedRelativeFrequencies();
            statistics.AddRealClassIntervals();
            statistics.AddAcumulatedFrequencies();

            var model = new GroupedReportModel{
                Range = statistics.Range,
                GroupsCount = statistics.GroupsCount,
                Amplitude = statistics.Amplitude,
                Table = Enumerable.ToList(statistics.GetTable())
            };

            return PartialView(model);
        }
    }
}