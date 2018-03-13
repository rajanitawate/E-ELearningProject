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
	public partial class MasterPage : TabbedPage
	{
		public MasterPage ()
		{
			InitializeComponent ();

            this.CurrentPageChanged += CurrentPageHasChanged;
        }

        protected void CurrentPageHasChanged(object sender, EventArgs e)
        {
            this.Title = this.CurrentPage.Title;            
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");
                if (result == true)
                {
                    System.Environment.Exit(0);
                }
                else
                {
                    
                }                
            });

            return true;
        }
    }
}