using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DashboardMobileApp.Models;

namespace DashboardMobileApp.ViewModels
{
    public class OxyExData
    {
        private string selectedProject;
        private string selectedMetric;
        private Projects[] metrics;

        public PlotModel LineSeriesModel { get; set; }

        public OxyExData()
        {
            LineSeriesModel = CreateLineSeriesChart();
        }

        public OxyExData(string selectedProject, string selectedMetric, Projects[] metrics)
        {
            this.selectedProject = selectedProject;
            this.selectedMetric = selectedMetric;
            this.metrics = metrics;

            LineSeriesModel = CreateLineSeriesChart();
        }

        private PlotModel CreateLineSeriesChart()
        {
            var model = new PlotModel
            {
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.TopCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };

            var ll = new LineSeries { StrokeThickness = 2, MarkerSize = 2, MarkerStroke = OxyColors.Red, Smooth = false, MarkerType = MarkerType.Circle };
            var ul = new LineSeries { StrokeThickness = 2, MarkerSize = 2, MarkerStroke = OxyColors.Blue, Smooth = false, MarkerType = MarkerType.Circle };
            var actuals = new LineSeries { StrokeThickness = 2, MarkerSize = 2, MarkerStroke = OxyColors.Green, Smooth = false, MarkerType = MarkerType.Circle };

            float maxval = 0;

            foreach (Projects proj in metrics)
            {
                if (proj.projectName.ToLower().Equals(selectedProject.ToLower()))
                {
                    foreach (Projectmetric projMetric in proj.projectMetrics)
                    {
                        if (projMetric.metricName.ToLower().Equals(selectedMetric.ToLower()))
                        {
                            for (int i = 0; i < projMetric.ll.Length; i++)
                            {
                                ll.Points.Add(new DataPoint(i, projMetric.ll[i]));
                                if (projMetric.ll[i] > maxval)
                                    maxval = projMetric.ll[i];
                            }
                            for (int i = 0; i < projMetric.ul.Length; i++)
                            {
                                ul.Points.Add(new DataPoint(i, projMetric.ul[i]));
                                if (projMetric.ul[i] > maxval)
                                    maxval = projMetric.ll[i];
                            }
                            for (int i = 0; i < projMetric.actuals.Length; i++)
                            {
                                actuals.Points.Add(new DataPoint(i, projMetric.actuals[i]));
                                if (projMetric.actuals[i] > maxval)
                                    maxval = projMetric.ll[i];
                            }
                        }
                    }
                    //break;
                }
            }

            model.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, });
            model.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, AbsoluteMinimum = 0 });
            model.Series.Add(ll);
            model.Series.Add(ul);
            model.Series.Add(actuals);

            return model;

        }

    }
}
