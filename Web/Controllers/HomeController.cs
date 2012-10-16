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
                yield return rnd.Next(57, 180) + Math.Round(rnd.NextDouble(), rnd.Next(0, 5));
            }
        }
        public ActionResult Index(int sample = 115)
        {
            ViewBag.Size = sample;
            ViewBag.Title = "Estadística Descriptiva";
            return View();
        }

        [HttpPost]
        public ActionResult Report(string data, string type)
        {
            ViewBag.Title = "Análisis";
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
            kernel.Bind<DataContainer>().ToMethod(context => new DataContainer(values)).InSingletonScope();

            ViewBag.Data = data;
            ViewBag.Type = type;
            var instance = kernel.Get<StatisticsInquirerBase>(type);
            Session.Add("kernel", kernel);

            return View(instance);
        }

        [ChildActionOnly]
        public PartialViewResult Histogram()
        {
            var table = ((StandardKernel)Session["kernel"]).Get<DataDistributionFrequencyInquirer>().GetTable();

            var catList = table.Select(row => (Interval)row.RealInterval).ToList();

            catList.Insert(0, new Interval(0, catList.First().From));
            var last = catList.Last();
            catList.Add(new Interval(last.To, last.To + last.To - last.From));

            var categories = catList.Select(interval => string.Format("{0} - {1}", interval.From, interval.To)).ToArray();
            var dataList = table.Select(row => (object)row.Frequency).ToList();
            dataList.Insert(0, 0);
            dataList.Add(0);

            var data = new Data(dataList.ToArray());

            Highcharts chart = new Highcharts("histogram")
                .InitChart(new Chart { 
                    DefaultSeriesType = ChartTypes.Column,
                    //BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#eee")),
                    //PlotBackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#fff"))
                })
                    .SetCredits(new Credits{Enabled = false})
                    .SetTitle(new Title { Text = "Histograma" })
                    .SetSubtitle(new Subtitle { Text = "Creado " + DateTime.Now.ToShortDateString() })
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
            var dataDistribution = ((StandardKernel)Session["kernel"]).Get<DataDistributionFrequencyInquirer>();
            var table = dataDistribution.GetTable();
            
            var catList = table.Select(row => (Interval)row.RealInterval).ToList();
            
            catList.Insert(0, new Interval(0, catList.First().From));
            var last = catList.Last();
            catList.Add(new Interval(last.To, last.To + last.To - last.From));
            
            var categories = catList.Select(interval => string.Format("{0} - {1}", interval.From, interval.To)).ToArray();
            var dataList = table.Select(row => (object)row.AcumulatedFrequency).ToList();
            dataList.Insert(0, 0);
            dataList.Add(dataList.Last());
            var data = new Data(dataList.ToArray());

            Highcharts chart = new Highcharts("ogive")
                .SetTitle(new Title { Text = "Ojiva" })
                .SetSubtitle(new Subtitle { Text = "Creado " + DateTime.Now.ToShortDateString() })
                .SetLegend(new Legend{Enabled = false})
                .SetTooltip(new Tooltip { 
                    BorderWidth = (Number) 1,
                    Formatter = "function() { return '<b>Intervalo:</b><br/> '+ this.x +'<br/>'+ '<b>F:</b> '+ this.y; }" 
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
                    Title = new YAxisTitle{Text = "Frecuencia Acumulada"},
                    Min = (Number) 0,
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