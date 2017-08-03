using DashboardMobileApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DashboardMobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            var User = new LoginViewModel
            {
                Username = varUsername.Text,
                Password = varPassword.Text
            };

            var isValid = User.LoginUser();
            if (await isValid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new DashboardPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                varMessage.Text = "Login Failed!";
                varPassword.Text = "";
            }
        }
    }
}