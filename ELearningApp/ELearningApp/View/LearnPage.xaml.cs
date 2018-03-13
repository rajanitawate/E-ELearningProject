using ELearningApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System;

namespace ELearningApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LearnPage : ContentPage
	{
        List<SubjectInfo> SubjectList =  new List<SubjectInfo>();
        Connection c = new Connection();

        public LearnPage ()
		{
			InitializeComponent ();

            SetBusyIndicator(true);

            Getdata();
            
            listsubj.ItemsSource = SubjectList;

            SetBusyIndicator(false);

        }

        public List<SubjectInfo> Getdata()
        {           
            string url = "http://13.126.189.26:10008/Api/get-subject/" + 0;

            JObject data = c.GetDetail(url);

            IList<JToken> results = data["SubjectList"].Children().ToList();

            //IList<SubjectInfo> searchRes = response["SubjectList"].Select(r => JsonConvert.DeserializeObject<SubjectInfo>(r.ToString())).ToList();

            foreach (JToken result in results)
            {
                System.Diagnostics.Debug.WriteLine(result); //just to check my json data.
                SubjectInfo searchResult = JsonConvert.DeserializeObject<SubjectInfo>(result.ToString()); //get exception on this line.
                SubjectList.Add(searchResult);
            }
            return SubjectList;
        }

        protected override void OnAppearing()
        {
            SetBusyIndicator(false);
        }

        private void Listsubj_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e?.SelectedItem == null)
            {
                return;
            }


            var item = (SubjectInfo)e.SelectedItem;

            if (item != null)
            {
                Navigation.PushAsync(new CoursePage(item.SubjectID));
                SetBusyIndicator(false);
            }

            listsubj.SelectedItem = null;

            SetBusyIndicator(false);

        }

        public void SetBusyIndicator(bool isBusyIndicatorIsVisible) => BusyIndicator.IsRunning = BusyIndicator.IsVisible = isBusyIndicatorIsVisible;

    }
}