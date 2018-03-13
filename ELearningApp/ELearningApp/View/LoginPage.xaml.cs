using ELearningApp.Model;
using ELearningApp.ToastFile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ELearningApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {      
        UserInfo userinfo;
        Connection c = new Connection();
        string url = "http://13.126.189.26:10008/api/authenticate_User";

        public LoginPage()
        {
            InitializeComponent();
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            SetBusyIndicator(true);

            userinfo = new UserInfo
            {
                UserName = entUserName.Text,
                Password = entPassword.Text
            };

            if (await LoginValidation(entUserName.Text, entPassword.Text))
            {
                // PostLogin(url,userinfo);
                UserInfo data = await c.SaveUser(userinfo, url);

                if (!data.Equals(null))
                {
                    if (data.Messages.Equals("Successful"))
                    {
                        if(Device.OS == TargetPlatform.iOS)
                        {
                            await DisplayAlert("Message", "Login Successful", "Ok");
                        }
                        else
                        {
                             XFToast.ShortMessage("Login Successful");
                        }
                        App.Current.MainPage = new NavigationPage(new MasterPage());
                        SetBusyIndicator(false);                       
                    }
                    else
                    {
                        if (Device.OS == TargetPlatform.iOS)
                        {
                            await DisplayAlert("Message", "Invalid Credentia", "Ok");
                        }
                        else
                        {
                            XFToast.ShortMessage("Invalid Credential");
                        }

                        SetBusyIndicator(false);
                    }
                }
                else
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        await DisplayAlert("Message", "Invalid Credentia", "Ok");
                    }
                    else
                    {
                        XFToast.ShortMessage("Invalid Credential");
                    }
                    SetBusyIndicator(false);
                }

            }
            SetBusyIndicator(false);
            // Loading.IsVisible = true;
        }

        public void OnTapRegisterTapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        protected override void OnAppearing()
        {
            SetBusyIndicator(false);
        }

        public async Task<bool> LoginValidation(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                return true;
            }
            else
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    await DisplayAlert("Message", "Please enter the UserName and Password", "Ok");
                }
                else
                {
                    XFToast.ShortMessage("Please enter the UserName and Password ");
                }
               
                return false;
            }
        }

        public void SetBusyIndicator(bool isBusyIndicatorIsVisible) => BusyIndicator.IsRunning = BusyIndicator.IsVisible = isBusyIndicatorIsVisible;

    }
}