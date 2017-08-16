using DashboardMobileApp.Models;
using DashboardMobileApp.ViewModels;
using Newtonsoft.Json;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DashboardMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        Rootobject projects;
        Projects[] metrics;
        string selectedProject;
        string selectedMetric;

        public DashboardPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            var client = new HttpClient();
            try
            {
                //Call to get all Projects
                //var response = await client.GetStringAsync("http://localhost:60931/api/GetLOV/Project");
                //var response = await client.GetStringAsync("http://192.168.247.45:7002/api/GetLOV/Project"); //@office
                var response = await client.GetStringAsync("http://192.168.1.97:7002/api/GetLOV/Project"); //@home

                //Call to fet all Metrics Response
                var metricResponse = await client.GetStringAsync("http://192.168.1.97:7003/api/ProjectMetricsData");

                projects = JsonConvert.DeserializeObject<Rootobject>(response);
                metrics = JsonConvert.DeserializeObject<Projects[]>(metricResponse);

                //Populate the picker for all the projects
                foreach (Models.Value val in projects.value)
                {
                    projPicker.Items.Add(ToTitleCase(val.projectName));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", "The services are not available right now.Please try later or contact the administrator", "OK");
            }
        }

        private void projPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                foreach (Projects proj in metrics)
                {
                    if (ToTitleCase(proj.projectName) == picker.Items[selectedIndex])
                    {
                        metricPicker.Items.Clear();
                        foreach (Projectmetric projMet in proj.projectMetrics)
                        {
                            if (!String.IsNullOrEmpty(projMet.metricName))
                                metricPicker.Items.Add(projMet.metricName);
                        }
                    }
                }
                selectedProject = picker.Items[selectedIndex];
            }
        }

        private void metricPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                selectedMetric = picker.Items[selectedIndex];

                var vSampleData = new OxyExData(selectedProject, selectedMetric, metrics);
                this.Title = "Line Series";
                this.BindingContext = vSampleData;
            }
        }

        private string ToTitleCase(string str)
        {
            string auxStr = str.ToLower();
            string[] auxArr = auxStr.Split(' ');
            string result = "";
            bool firstWord = true;
            foreach (string word in auxArr)
            {
                if (!firstWord)
                    result += " ";
                else
                    firstWord = false;

                result += word.Substring(0, 1).ToUpper() + word.Substring(1, word.Length - 1);
            }

            return result;

        }

    }
}
