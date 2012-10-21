using DotNet.Highcharts.Enums;

namespace Web.Models
{
    using DotNet.Highcharts.Helpers;
    using System.Drawing;
    using Mx.Ipn.Esime.Statistics.GroupedData;
    using System.Linq;
    using DotNet.Highcharts.Options;
    using System.Collections.Generic;
    using DotNet.Highcharts;

    public class GroupedReportModel
    {
        public GroupedReportModel()
        {
            Table = new List<dynamic>();
        }

        public double Range
        {
            get;
            set;
        }

        public int GroupsCount
        {
            get;
            set;
        }

        public double Amplitude
        {
            get;
            set;
        }

        public List<dynamic> Table
        {
            get;
            set;
        }

        private Highcharts GetChart(string name, string yaxis)
        {
            //Based on http://jsfiddle.net/jlbriggs/9LGVA/
            
            var catList = Table.Select(row => (Interval)row.RealInterval).ToList();
            
            catList.Insert(0, new Interval(0, catList.First().From));
            var last = catList.Last();
            catList.Add(new Interval(last.To, last.To + last.To - last.From));
            var categories = catList.Select(interval => string.Format("{0} - {1}", interval.From, interval.To)).ToArray();
            
            var chart = new Highcharts(name)
                .SetCredits(new Credits{Enabled = false})
                    .SetTitle(new Title { Text = "" })
                    .SetSubtitle(new Subtitle { Text = ""})
                    .SetLegend(new Legend{Enabled = false}).SetXAxis(new XAxis{
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
                        Title = new YAxisTitle{Text = yaxis},
                        GridLineColor = ColorTranslator.FromHtml("#e9e9e9"),
                        TickColor = ColorTranslator.FromHtml("#ccc"),
                        LineColor = ColorTranslator.FromHtml("#ccc")
                    });
            
            return chart;
        }

        public Highcharts Histogram
        {
            get
            {
                var dataList = Table.Select(row => (object)row.Frequency).ToList();
                dataList.Insert(0, 0);
                dataList.Add(0);
                var data = new Data(dataList.ToArray());

                var model = GetChart("histogram", "Frecuencia")
                    .InitChart(new Chart {DefaultSeriesType = ChartTypes.Column})
                        .SetTooltip(new Tooltip { 
                            BorderWidth = (Number) 1,
                            Formatter = "function() { return '<b>Intervalo:</b><br/> '+ this.x +'<br/>'+ '<b>f:</b> '+ this.y; }" 
                        }).SetPlotOptions(new PlotOptions{
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
                        .SetSeries(new Series[]{
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

                return model;
            }
        }

        public Highcharts Ogive
        {
            get
            {
                var dataList = Table.Select(row => (object)row.AcumulatedFrequency).ToList();
                dataList.Insert(0, 0);
                dataList.Add(dataList.Last());
                var data = new Data(dataList.ToArray());

                var model = GetChart("ogive", "Frecuencia Acumulada")
                    .SetTooltip(new Tooltip { 
                        BorderWidth = (Number) 1,
                        Formatter = "function() { return '<b>Intervalo:</b><br/> '+ this.x +'<br/>'+ '<b>F:</b> '+ this.y; }" 
                    })
                        .SetSeries(new Series[]{
                            new Series{
                                Name = "Frecuencia (F)",
                                Type = ChartTypes.Areaspline,
                                Data = data
                            }
                        });

                return model;
            }
        }
    }
}