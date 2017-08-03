using DashboardMobileApp.Models;
using DashboardMobileApp.ViewModels;
using Newtonsoft.Json;
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
        public DashboardPage()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {

            var client = new HttpClient();
            //var response = await client.GetStringAsync("http://localhost:60931/api/GetLOV/Project");
            //var response = await client.GetStringAsync("http://192.168.247.45:7002/api/GetLOV/Project"); //@office
            var response = await client.GetStringAsync("http://192.168.1.97:7002/api/GetLOV/Project"); //@home
        

            Rootobject projects = JsonConvert.DeserializeObject<Rootobject>(response);

            foreach (Models.Value val in projects.value)
            {
                projPicker.Items.Add(ToTitleCase(val.projectName));
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

        private void projPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                ProjectNameLabel.Text = picker.Items[selectedIndex];
            }
        }
    }
}