using DashboardMobileApp.Models;
using DashboardMobileApp.Services;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardMobileApp.ViewModels
{
    public class DashboardViewModel
    {
        private ApiServices _apiServices = new ApiServices();
        public PlotModel LineSeriesModel { get; set; }

        public Rootobject Projects { get; set; }

        public async void LoadProjects()
        {
            this.Projects = await _apiServices.GetProjects();
        }
    }
}

