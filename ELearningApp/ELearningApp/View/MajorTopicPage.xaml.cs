using ELearningApp.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
	public partial class MajorTopicPage : ContentPage
	{
        public List<MajorTopicInfo> majorTopicList = new List<MajorTopicInfo>();
        public List<TopicInfo> TopicList = new List<TopicInfo>();
        Connection c = new Connection();
        IList<JToken> mresults, tresults;


        public MajorTopicPage (int courseId)
		{
			InitializeComponent ();

            SetBusyIndicator(true);

            string MajorTopicUrl = "http://13.126.189.26:10008/api/GetMajorAndTopic/" + courseId; //387

            JObject data = c.GetDetail(MajorTopicUrl);

            mresults = data["list"]["listMajorTopic"].Children().ToList();

            tresults = data["list"]["listTopic"].Children().ToList();

            if(mresults.Count!=0)
            {
                listMajorTopics.IsVisible = true;
                
                listTopics.IsVisible = false;
                foreach (JToken result in mresults)
                {
                    System.Diagnostics.Debug.WriteLine(result); //just to check my json data.
                    MajorTopicInfo searchResult = JsonConvert.DeserializeObject<MajorTopicInfo>(result.ToString()); //get exception on this line.          
                    majorTopicList.Add(searchResult);                    
                }
                if (majorTopicList.Count == 0)
                {
                    lbltxt.IsVisible = true;
                    stackList.IsVisible = false;
                }
                else
                {
                    lbltxt.IsVisible = false;
                    stackList.IsVisible = true;
                    listMajorTopics.ItemsSource = majorTopicList;
                }
                
                SetBusyIndicator(false);
            }

            else
            {
                listTopics.IsVisible = true;
                listMajorTopics.IsVisible = false;
                foreach (JToken result in tresults)
                {
                    System.Diagnostics.Debug.WriteLine(result); //just to check my json data.
                    TopicInfo searchResult = JsonConvert.DeserializeObject<TopicInfo>(result.ToString()); //get exception on this line.                                                                                                                          
                    TopicList.Add(searchResult);                    
                }

                if (TopicList.Count == 0)
                {
                    lbltxt.IsVisible = true;
                    stackList.IsVisible = false;
                }
                else
                {
                    lbltxt.IsVisible = false;
                    stackList.IsVisible = true;
                    listTopics.ItemsSource = TopicList;
                }
               
                SetBusyIndicator(false);
            }
            
        }

        protected override void OnAppearing()
        {
            if(mresults.Count != 0)
            {
                Title = "Major Topics";
            }
            else
            {
                Title = "Topic Lists";
            }
            SetBusyIndicator(false);
        }

        private void ListTopics_ItemSelected (object sender, SelectedItemChangedEventArgs e)
        {
            if (e?.SelectedItem == null)
            {
                return;
            }

            var topic = e.SelectedItem as TopicInfo;

            if (topic != null)
            {
                if (topic.TopicFormatId == "4")
                {
                    topic.FullPathPDF = "http://13.126.189.26:10008/" + topic.FilePath;
                    Device.OpenUri(new Uri(topic.FullPathPDF));
                    SetBusyIndicator(false);
                }
                else
                {
                    Navigation.PushAsync(new TopicDetailPage(topic));
                    SetBusyIndicator(false);
                }
            }

            listTopics.SelectedItem = null;
            SetBusyIndicator(false);
        }

        private void ListMajorTopics_ItemSelected (object sender, SelectedItemChangedEventArgs e)
        {
            if (e?.SelectedItem == null)
            {
                return;
            }

            var mtopic = e.SelectedItem as MajorTopicInfo;

            if (mtopic != null)
            {
                Navigation.PushAsync(new TopicPage(mtopic));
                SetBusyIndicator(false);
            }

            listMajorTopics.SelectedItem = null;

            SetBusyIndicator(false);
        }

        public void SetBusyIndicator(bool isBusyIndicatorIsVisible) => BusyIndicator.IsRunning = BusyIndicator.IsVisible = isBusyIndicatorIsVisible;
    }
}