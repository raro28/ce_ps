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
            Session.Clear();
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

            var model = new GroupedReportModel{
                Range = statistics.Range,
                GroupsCount = statistics.GroupsCount,
                Amplitude = statistics.Amplitude,
                Table = Enumerable.ToList(statistics.GetTable())
            };

            return PartialView(model);
        }


        [ChildActionOnly]
        public PartialViewResult Histogram()
        {
            //Based on http://jsfiddle.net/jlbriggs/9LGVA/
            dynamic statistics = Session["inquirer"];
            statistics.AddAcumulatedRelativeFrequencies();
            statistics.AddRealClassIntervals();
            List<dynamic> table = Enumerable.ToList(statistics.GetTable());
            
            var catList = table.Select(row => (Interval)row.RealInterval).ToList();
            
            catList.Insert(0, new Interval(0, catList.First().From));
            var last = catList.Last();
            catList.Add(new Interval(last.To, last.To + last.To - last.From));
            
            var categories = catList.Select(interval => string.Format("{0} - {1}", interval.From, interval.To)).ToArray();
            var dataList = table.Select(row => (object)row.Frequency).ToList();
            dataList.Insert(0, 0);
            dataList.Add(0);
            
            var data = new Data(dataList.ToArray());
            this.ViewBag.ChartTitle = "Histograma";
            Highcharts chart = new Highcharts("histogram")
                .InitChart(new Chart { 
                    DefaultSeriesType = ChartTypes.Column,
                    //BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#eee")),
                    //PlotBackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#fff"))
                })
                    .SetCredits(new Credits{Enabled = false})
                    .SetTitle(new Title { Text = "" })
                    .SetSubtitle(new Subtitle { Text = DateTime.Now.ToLongDateString() +" "+ DateTime.Now.ToShortTimeString() })
                    .SetLegend(new Legend{Enabled = false})
                    .SetTooltip(new Tooltip { 
                        BorderWidth = (Number) 1,
                        Formatter = "function() { return '<b>Intervalo:</b><br/> '+ this.x +'<br/>'+ '<b>f:</b> '+ this.y; }" 
                    })
                    .SetPlotOptions(new PlotOptions{
                        Column = new PlotOptionsColumn{
                            Shadow = false,
                            BorderWidth = (Number) 0.5,
                            BorderColor = ColorTranslator.FromHtml("#666"),
                            PointPadding = (Number) 0,
                            GroupPadding = (Number) 0,
                            Color = ColorTranslator.FromHtml("#D9CCCCCC"),
                        },
                        Spline = new PlotOptionsSpline{
                            Shadow = false,
                            Marker = new PlotOptionsSeriesMarker{
                                Radius = (Number)1
                            }
                        },
                        Areaspline = new PlotOptionsAreaspline{
                            Color = ColorTranslator.FromHtml("#D2B48C"),
                            //FillColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFDEAD")),
                            Shadow = false,
                            Marker = new PlotOptionsSeriesMarker{
                                Radius = (Number)1
                            }
                        }
                    })
                    .SetXAxis(new XAxis{
                        Categories = categories,
                        Title = new XAxisTitle{Text = "Intervalos Reales"},
                        Labels = new XAxisLabels{
                            Rotation = (Number)(-90),
                            Y = (Number) 40,
                            Style = "fontSize:'12px', fontWeight:'normal', color:'#333'"
                        },
                        LineWidth = (Number) 0,
                        LineColor = ColorTranslator.FromHtml("#999"),
                        TickLength = (Number) 70,
                        TickColor = ColorTranslator.FromHtml("#ccc")
                    })
                    .SetYAxis(new YAxis{
                        Title = new YAxisTitle{Text = "Frecuencia"},
                        GridLineColor = ColorTranslator.FromHtml("#e9e9e9"),
                        TickWidth = (Number) 1,
                        TickLength = (Number) 3,
                        TickColor = ColorTranslator.FromHtml("#ccc"),
                        LineColor = ColorTranslator.FromHtml("#ccc"),
                        TickInterval = 25
                    }).SetSeries(new Series[]{
                        new Series{
                            Name = "Bins",
                            Data = data
                        },
                        new Series{
                            Name = "Curve",
                            Data = data,
                            Type = ChartTypes.Spline,
                            PlotOptionsSeries = new PlotOptionsSeries
                            {
                                Visible = true,
                                Color = ColorTranslator.FromHtml("#D9CCCCFF")
                            }
                        },
                        new Series{
                            Name = "Filled Curve",
                            Data = data,
                            Type = ChartTypes.Areaspline,
                            PlotOptionsSeries = new PlotOptionsSeries
                            {
                                Visible = true,
                                Color = ColorTranslator.FromHtml("#D9CCCCFF")
                            }
                        }
                    });
            
            return PartialView("HighChart", chart);
        }
        
        [ChildActionOnly]
        public PartialViewResult Ogive()
        {
            dynamic statistics = Session["inquirer"];
            statistics.AddAcumulatedFrequencies();

            List<dynamic> table = Enumerable.ToList(statistics.GetTable());
            
            var catList = table.Select(row => (Interval)row.RealInterval).ToList();
            
            catList.Insert(0, new Interval(0, catList.First().From));
            var last = catList.Last();
            catList.Add(new Interval(last.To, last.To + last.To - last.From));
            
            var categories = catList.Select(interval => string.Format("{0} - {1}", interval.From, interval.To)).ToArray();
            var dataList = table.Select(row => (object)row.AcumulatedFrequency).ToList();
            dataList.Insert(0, 0);
            dataList.Add(dataList.Last());
            var data = new Data(dataList.ToArray());
            this.ViewBag.ChartTitle = "Ojiva";
            Highcharts chart = new Highcharts("ogive")
                .SetTitle(new Title { Text = "" })
                    .SetSubtitle(new Subtitle { Text = DateTime.Now.ToLongDateString() +" "+ DateTime.Now.ToShortTimeString() })
                    .SetLegend(new Legend{Enabled = false})
                    .SetTooltip(new Tooltip { 
                        BorderWidth = (Number) 1,
                        Formatter = "function() { return '<b>Intervalo:</b><br/> '+ this.x +'<br/>'+ '<b>F:</b> '+ this.y; }" 
                    })
                    .SetCredits(new Credits{Enabled = false})
                    .SetXAxis(new XAxis{
                        Categories = categories,
                        Title = new XAxisTitle{Text = "Intervalos Reales"},
                        Labels = new XAxisLabels{
                            Rotation = (Number)(-90),
                            Y = (Number) 40,
                            Style = "fontSize:'12px', fontWeight:'normal', color:'#333'"
                        },
                        LineWidth = (Number) 0,
                        LineColor = ColorTranslator.FromHtml("#999"),
                        TickLength = (Number) 70,
                        TickColor = ColorTranslator.FromHtml("#ccc")
                    })
                    .SetYAxis(new YAxis{
                        Title = new YAxisTitle{Text = "Frecuencia Acumulada"},
                        Min = (Number) 0,
                        Max = (Number) statistics.Container.DataCount,
                        GridLineColor = ColorTranslator.FromHtml("#e9e9e9"),
                        TickWidth = (Number) 1,
                        TickLength = (Number) 3,
                        TickColor = ColorTranslator.FromHtml("#ccc"),
                        LineColor = ColorTranslator.FromHtml("#ccc"),
                        EndOnTick = true
                    })
                    .SetSeries(new Series[]{
                        new Series{
                            Name = "Frecuencia (F)",
                            Type = ChartTypes.Areaspline,
                            Data = data
                        }
                    });            
            return PartialView("HighChart", chart);
        }
    }
}