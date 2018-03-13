using ELearningApp.Model;
using ELearningApp.ToastFile;

using Plugin.MediaManager;
using Plugin.MediaManager.Abstractions;
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
	public partial class TopicDetailPage : ContentPage
	{
        private IPlaybackController PlaybackController => CrossMediaManager.Current.PlaybackController;
         public bool flag = false;
        public TopicInfo topicInfoval = new TopicInfo();
        public string TFormatId;

        public TopicDetailPage (TopicInfo topicInfo)
		{
			InitializeComponent ();
            
            SetBusyIndicator(true);

            topicInfoval = topicInfo;
            CrossMediaManager.Current.PlayingChanged += (sender, e) =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    ProgressBar.Progress = e.Progress;
                   var a= e.Position.Seconds;
                    var t = e.Duration.Seconds;
                   
                });
            };


            if (topicInfo.TopicFormatId == "1")
            {
                stkContent.IsVisible = true;
                stkVideo.IsVisible = false;
                stkImage.IsVisible = false;
                stkPDF.IsVisible = false;

                TopicContent.Text = topicInfo.TopicTranscript;
                SetBusyIndicator(false);
            }
            else if(topicInfo.TopicFormatId == "2")
            {
                
                stkContent.IsVisible = false;
                stkVideo.IsVisible = true;
                stkImage.IsVisible = false;
                stkPDF.IsVisible = false;

                videoView.Source = "http://13.126.189.26:10008/" + topicInfo.FilePath;
                SetBusyIndicator(false);
            }
            else if (topicInfo.TopicFormatId == "3")
            {
                stkContent.IsVisible = false;
                stkVideo.IsVisible = false;
                stkImage.IsVisible = true;
                stkPDF.IsVisible = false;

                TopicImg.Source = "http://13.126.189.26:10008/" + topicInfo.FilePath;
                SetBusyIndicator(false);
            }
            else
            {
                //stkContent.IsVisible = false;
                //stkVideo.IsVisible = false;
                //stkImage.IsVisible = false;
                //stkPDF.IsVisible = true;

                ////TFormatId = topicInfo.TopicFormatId;
                
                // topicInfo.FullPathPDF = "http://13.126.189.26:10008/" + topicInfo.FilePath;

                ////Device.OpenUri(new Uri(topicInfo.FullPathPDF));

                //////UrlWebViewSource urlWebViewSource = new UrlWebViewSource()
                //////{
                //////    Url = path
                //////};
                //////TopicPdf.Source = urlWebViewSource;
                //TopicPdf.Source = topicInfo.FullPathPDF;
                //SetBusyIndicator(false);
                //OnBackButtonPressed();
                
            }



        }


       

        protected override void OnAppearing()
        {
            if (topicInfoval.TopicFormatId == "2")
            {
                stkContent.IsVisible = false;
                stkVideo.IsVisible = true;
                stkImage.IsVisible = false;
                stkPDF.IsVisible = false;

                videoView.Source = "http://13.126.189.26:10008/" + topicInfoval.FilePath;
                PlaybackController.Play();
                SetBusyIndicator(false);
            }
            //videoView.Source = "http://clips.vorwaerts-gmbh.de/big_buck_bunny.mp4";
           // SetBusyIndicator(false);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            PlaybackController.Stop();
           
        }
       

        void PlayPauseClicked(object sender, System.EventArgs e)
        {
            if (flag == false)
            {
                flag = true;
                imgControl.Source = "play.png";
                PlaybackController.Pause();
            }
            else if (flag == true)
            {
                flag = false;
                imgControl.Source = "pause.png";
                PlaybackController.Play();
            }
           
        }
        

        public void SetBusyIndicator(bool isBusyIndicatorIsVisible) => BusyIndicator.IsRunning = BusyIndicator.IsVisible = isBusyIndicatorIsVisible;
           
    }
}