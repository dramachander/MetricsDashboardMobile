using DashboardMobileApp.Services;
using DashboardMobileApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DashboardMobileApp.ViewModels
{
    public class LoginViewModel
    {
        private ApiServices _apiServices = new ApiServices();

        public string Username { get; set; }
        public string Password { get; set; }
        public string Access_Token { get; set; }

        public string Message { get; set; }
        public async Task<bool> LoginUser()
        {
            var isSuccess = await _apiServices.LoginAsync(Username, Password);
            return isSuccess;
        }
    }
}
