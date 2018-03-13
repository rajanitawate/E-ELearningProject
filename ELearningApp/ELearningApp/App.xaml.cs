using ELearningApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ELearningApp
{
	public partial class App : Application
	{ 
 		public App ()
		{ 
			InitializeComponent();

            MainPage =new NavigationPage(new SplashPage());
		}
        
        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
