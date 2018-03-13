using ELearningApp.Model;
using ELearningApp.ToastFile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ELearningApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public HttpClient client;

        Connection c = new Connection();
               
        UserInfo userInfo;

        string Url = "http://13.126.189.26:10008/api/insert-user";

        public RegisterPage()
        {
            client = new HttpClient();

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            SetBusyIndicator(false);
        }

        void bactap_clicked(object sender, EventArgs args)
        {
            Navigation.PopAsync();
        }

        public void SetBusyIndicator(bool isBusyIndicatorIsVisible) => BusyIndicator.IsRunning = BusyIndicator.IsVisible = isBusyIndicatorIsVisible;

        private async void BtnRegister_Clicked()
        {
            SetBusyIndicator(true);
            userInfo = new UserInfo
            {
                FirstName = entFName.Text,
                UserName = entUName.Text,
                Email = entEmail.Text,
                MobileNumber = entMobile.Text,
                Password = entPassword.Text,
                RoleId = 3,
                IsAgree = true,
                UserType = "Individual"
            };


            if (await CheckName(entFName.Text, true))
            {
                if (await CheckName(entUName.Text, false))
                {
                    if (await CheckPhoneNo(entMobile.Text))
                    {
                        if (await CheckEmailId(entEmail.Text))
                        {
                            if (await CheckPassword(entPassword.Text))
                            {
                                if (await CheckPassword(entCPassword.Text))
                                {
                                    if (entPassword.Text.Equals(entCPassword.Text))
                                    {                                        
                                        UserInfo data = await c.SaveUser(userInfo, Url);

                                        if (data.Messages.Equals("Successful Inserted"))
                                        {
                                            if (Device.OS == TargetPlatform.iOS)
                                            {
                                                await DisplayAlert("Message", "Registered Successfully", "Ok");
                                            }
                                            else
                                            {
                                                XFToast.ShortMessage("Registered Successfully");
                                            }
                                                                                      
                                            App.Current.MainPage = new NavigationPage(new MasterPage());
                                            SetBusyIndicator(false);
                                        }
                                        else
                                        {
                                            SetBusyIndicator(false);
                                            if (Device.OS == TargetPlatform.iOS)
                                            {
                                                await DisplayAlert("Message", "Invalid credential", "Ok");
                                            }
                                            else
                                            {
                                                XFToast.ShortMessage("Invalid credential");
                                            }
                                           
                                        }
                                    }
                                    else
                                    {
                                        SetBusyIndicator(false);
                                        if (Device.OS == TargetPlatform.iOS)
                                        {
                                            await DisplayAlert("Message", "The password and Confirm Password should be same", "Ok");
                                        }
                                        else
                                        {
                                            XFToast.ShortMessage("The password and Confirm Password should be same");
                                        }
                                       
                                    }
                                    SetBusyIndicator(false);
                                }
                                SetBusyIndicator(false);
                            }
                            SetBusyIndicator(false);
                        }
                        SetBusyIndicator(false);
                    }
                    SetBusyIndicator(false);
                }
                SetBusyIndicator(false);
            }
            SetBusyIndicator(false);
        }

        public async Task<bool> CheckPassword(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (password.Length > 5)
                {
                    return true;
                }
                else
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        await DisplayAlert("Message", "The password lenght should be above 6", "Ok");
                    }
                    else
                    {
                        XFToast.ShortMessage("The password lenght should be above 6");
                    }
                    
                    return false;
                }
            }
            else
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    await DisplayAlert("Message", "Please Enter the password", "Ok");
                }
                else
                {
                    XFToast.ShortMessage("Please Enter the password");
                }
               
                return false;
            }
        }

        public async Task<bool> CheckName(string name, bool flag)
        {
            if (!string.IsNullOrEmpty(name))
            {

                return true;
            }
            else
            {
                if (flag)
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        await DisplayAlert("Message", "Please enter the Name", "Ok");
                    }
                    else
                    {
                        XFToast.ShortMessage("Please enter the Name");
                    }                    
                }
                else
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        await DisplayAlert("Message", "Please enter the User Name", "Ok");
                    }
                    else
                    {
                        XFToast.ShortMessage("Please enter the User Name");
                    }                    
                }
                return false;
            }
        }

        public async Task<bool> CheckEmailId(string emailId)
        {
            if (!string.IsNullOrEmpty(emailId))
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(emailId);
                if (match.Success)
                {
                    return true;
                }
                else
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        await DisplayAlert("Message", "Please enter proper EmailId", "Ok");
                    }
                    else
                    {
                        XFToast.ShortMessage("Please enter proper EmailId");
                    }
                   
                    return false;
                }
            }
            else
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    await DisplayAlert("Message", "Please enter the EmailId", "Ok");
                }
                else
                {
                    XFToast.ShortMessage("Please enter the EmailId");
                }               
                return false;
            }
        }

        public async Task<bool> CheckPhoneNo(string phoneNo)
        {
            if (!string.IsNullOrEmpty(phoneNo))
            {
                if (phoneNo.Length == 10)
                {
                    return true;
                }
                else
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        await DisplayAlert("Message", "Please enter proper mobile No", "Ok");
                    }
                    else
                    {
                        XFToast.ShortMessage("Please enter proper mobile No");
                    }                    
                    return false;
                }
            }
            else
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    await DisplayAlert("Message", "Please enter the Mobile No.", "Ok");
                }
                else
                {
                    XFToast.ShortMessage("Please enter the Mobile No.");
                }
              
                return false;
            }
        }
    }
}