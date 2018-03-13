using ELearningApp.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ELearningApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SplashPage : ContentPage
	{
        static UserDatabase userDatabase;

        public SplashPage ()
		{
			InitializeComponent ();
		}

        public static UserDatabase Database
        {
            get
            {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabase(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath("User.db3"));
                }
                return userDatabase;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await lbltext.FadeTo(3, 3000);
            await lbltext.ScaleTo(1, 2000);
            await lbltext.ScaleTo(0.5, 500, Easing.Linear);
            await lbltext.ScaleTo(1, 500, Easing.Linear);
            var status = await SplashPage.Database.GetUserAsync();

            if (status.Count != 0)
            {
                Application.Current.MainPage = new NavigationPage(new MasterPage());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
    }
}