/*
 * Copyright (C) 2012 Hector Eduardo Diaz Campos
 * 
 * This file is part of Mx.DotNet.Statistics.
 * 
 * Mx.DotNet.Statistics is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version.
 * 
 * Mx.DotNet.Statistics is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Mx.DotNet.Statistics.  If not, see <http://www.gnu.org/licenses/>.
 */
namespace Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using Ninject;
    using Mx.Ipn.Esime.Statistics.Core.Base;
    using Mx.Ipn.Esime.Statistics.GroupedData;
    using Mx.Ipn.Esime.Statistics.UngroupedData;
    using Web.Models;

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

        public string RandomSample(int size)
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
            ViewBag.Title = "Inicio";

            var model = new StubModel{
                Data = RandomSample(sample),
                Type = "ungrouped"
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string Data, string Type)
        {
            ViewBag.Title = "Mx.DotNet.Statistics";

            HttpContext.Application.Clear();
            var values = Data.Split(',').Select(number => Double.Parse(number)).ToList();

            var kernel = globalKernel.Get<StandardKernel>();
            kernel.Bind<DataContainer>().ToMethod(context => new DataContainer(values)).InSingletonScope();

            var inquirer = kernel.Get<StatisticsInquirerBase>(Type);

            HttpContext.Application.Add("inquirer", inquirer);
            HttpContext.Application.Add("data", Data);

            ViewBag.Type = Type;
            return View("Report");
        }

        public ActionResult About()
        {
            ViewBag.Title = "Acerca de";

            return View();
        }

        [ChildActionOnly]
        public PartialViewResult CentralTendencySummary()
        {
            dynamic statistics = HttpContext.Application["inquirer"];

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
            dynamic statistics = HttpContext.Application["inquirer"];

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
            dynamic statistics = HttpContext.Application["inquirer"];

            var model = new DataSummaryModel{
                Data = HttpContext.Application["data"].ToString(),
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
            dynamic statistics = HttpContext.Application["inquirer"];
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

        [ChildActionOnly]
        public PartialViewResult XileCalculator()
        {
            var model = new XileCalculatorModel{
                Xile = (int)Xiles.Quartile,
                NthXile = 3
            };

            return PartialView(model);
        }

        [HttpPost]
        public string Xile(int NthXile, int Xile)
        {
            try
            {
                dynamic statistics = HttpContext.Application["inquirer"];

                var info = new XileInfo((Xiles)Xile, NthXile);
                var result = Math.Round(statistics.GetXile(info), 4);

                return result.ToString();
            } catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}