using ELearningApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ELearningApp.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CoursePage : ContentPage
	{
        List<CourseInfo> courseList = new List<CourseInfo>();
        Connection c = new Connection();
        string url;

        public CoursePage (int id)
		{
			InitializeComponent ();

            SetBusyIndicator(true);

            url = "http://13.126.189.26:10008/api/Get_CourseList/" + id; //106

            JObject data = c.GetDetail(url);
             
            IList<JToken> results = data["list"].Children().ToList();

            //IList<SubjectInfo> searchRes = response["SubjectList"].Select(r => JsonConvert.DeserializeObject<SubjectInfo>(r.ToString())).ToList();

            foreach (JToken result in results)
            {
                System.Diagnostics.Debug.WriteLine(result); //just to check my json data.
                CourseInfo searchResult = JsonConvert.DeserializeObject<CourseInfo>(result.ToString()); //get exception on this line.
                searchResult.Imagepath = "http://13.126.189.26:10008/" + searchResult.Image;
                courseList.Add(searchResult);
            }
            if (courseList.Count == 0)
            {
                lbltxt.IsVisible = true;
                listViewGrid.IsVisible = false;
            }
            else
            {
                listViewGrid.ItemsSource = courseList;
                lbltxt.IsVisible = false;
            }
           
            SetBusyIndicator(false);

        }

        protected override void OnAppearing()
        {            
            SetBusyIndicator(false);
        }

        private void ListCorse_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            if (e?.SelectedItem == null)
            {
                return;
            }


            var item = (CourseInfo)e.SelectedItem;

            if (item != null)
            {
                Navigation.PushAsync(new MajorTopicPage(item.CourseId));
                SetBusyIndicator(false);
            }

            listViewGrid.SelectedItem = null;

            SetBusyIndicator(false);

        }

        public void SetBusyIndicator(bool isBusyIndicatorIsVisible) => BusyIndicator.IsRunning = BusyIndicator.IsVisible = isBusyIndicatorIsVisible;

    }
}