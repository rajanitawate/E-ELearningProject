using ELearningApp.Model;
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
	public partial class TopicPage : ContentPage
	{
		public TopicPage (MajorTopicInfo majorTopicInfo)
		{
			InitializeComponent ();

            SetBusyIndicator(true);
            if (majorTopicInfo.listTopic.Count == 0)
            {
                lbltxt.IsVisible = true;
                stackList.IsVisible = false;
            }
            else
            {
                lbltxt.IsVisible = false;
                stackList.IsVisible = true;
                listTopics.ItemsSource = majorTopicInfo.listTopic;
            }
            

            SetBusyIndicator(false);

        }

        private void ListTopics_ItemSelected(object sender, SelectedItemChangedEventArgs e)
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

        protected override void OnAppearing()
        {
            SetBusyIndicator(false);
        }

        public void SetBusyIndicator(bool isBusyIndicatorIsVisible) => BusyIndicator.IsRunning = BusyIndicator.IsVisible = isBusyIndicatorIsVisible;
    }
}