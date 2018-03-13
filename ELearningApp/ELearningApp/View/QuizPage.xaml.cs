using ELearningApp.Model;
using ELearningApp.ToastFile;
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
    public partial class QuizPage : ContentPage
    {
        List<SubjectInfo> SubjectList = new List<SubjectInfo>();
        List<CourseInfo> CourseList;
        List<string> list = new List<string>();
        Connection c = new Connection();
        public int subjectId, courseId;
        public string selectedSubjectValue, selectedCourseValue;

        public QuizPage()
        {
            InitializeComponent();

            SetBusyIndicator(true);
            DataImage();

            GetData();

            subjectPick.SelectedIndexChanged += (sender, args) =>
            {
                foreach (var a in SubjectList)
                {
                    if(subjectPick.SelectedIndex == -1)
                    {
                        subjectPick.Title = "Select Subject";
                    }
                    else
                    {
                        selectedSubjectValue = subjectPick.Items[subjectPick.SelectedIndex];
                        if (selectedSubjectValue == a.SubjectName)
                        {
                            subjectId = a.SubjectID;
                            GetDataCourse();                            
                        }
                    }                   
                }
            };

            if (subjectPick.SelectedIndex == -1)
            {
                coursePick.Title = "Select Course";
                coursePick.IsEnabled = false;
                coursePick.Items.Add("Please select subject First ):");
            }
            else
            {
                coursePick.SelectedIndexChanged += (sender, args) =>
                {
                    foreach (var a in CourseList)
                    {
                        if (subjectPick.SelectedIndex == -1)
                        {
                            subjectPick.Title = "Select Subject";
                        }
                        else
                        {
                            selectedCourseValue = coursePick.Items[coursePick.SelectedIndex];
                            if (selectedCourseValue == a.CourseName)
                            {
                                courseId = a.CourseId;
                            }
                        }                       
                    }
                };
            }
        }

        public void SetBusyIndicator(bool isBusyIndicatorIsVisible) => BusyIndicator.IsRunning = BusyIndicator.IsVisible = isBusyIndicatorIsVisible;

        public void DataImage()
        {
            List<string> namesList = new List<string>();

            namesList.Add("thinkimagefor.jpg");
            namesList.Add("imagenewsave.jpg");
            namesList.Add("thinkImage.jpg");
            namesList.Add("imagenew.jpg");
            namesList.Add("imagenewsave.jpg");
            namesList.Add("quizbaner.jpg");
            namesList.Add("imagenewsave.jpg");

            ChangingImage.Source = namesList[0];
            int i = 1;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (i < namesList.Count)
                {
                    ChangingImage.Source = namesList[i];
                }
                else
                {
                    i = 0;
                    ChangingImage.Source = namesList[i];
                }
                i++;
                return true;
            });
        }

        protected override void OnAppearing()
        {
            subjectPick.SelectedIndex = -1;
            coursePick.SelectedIndex = -1;
            coursePick.IsEnabled = false;
            subjectId = 0; courseId = 0;
            SetBusyIndicator(false);
        }

        public void GetData()
        {
            string url = "http://13.126.189.26:10008/Api/get-subject/" + 0;

            JObject data = c.GetDetail(url);

            IList<JToken> results = data["SubjectList"].Children().ToList();

            //IList<SubjectInfo> searchRes = response["SubjectList"].Select(r => JsonConvert.DeserializeObject<SubjectInfo>(r.ToString())).ToList();

            foreach (JToken result in results)
            {
                System.Diagnostics.Debug.WriteLine(result); //just to check my json data.
                SubjectInfo searchResult = JsonConvert.DeserializeObject<SubjectInfo>(result.ToString()); //get exception on this line.
                subjectPick.Items.Add(searchResult.SubjectName);                                                                        // SubjectList.Add(searchResult.SubjectName.ToString());
                SubjectList.Add(searchResult);
            }
            SetBusyIndicator(false);

        }

        private async void Btnstart_Clicked(object sender, EventArgs e)
        {
            if (subjectPick.SelectedIndex != -1)
            {
                if (coursePick.SelectedIndex != -1 )
                {
                    await Navigation.PushAsync(new StartQuizPage(courseId));                    
                }
                else
                {
                    if (Device.OS == TargetPlatform.iOS)
                    {
                        await DisplayAlert("Message", "Please select any course", "Ok");
                    }
                    else
                    {
                        XFToast.ShortMessage("Please select any course");
                    }
                }
            }
            else
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    await DisplayAlert("Message", "Please select any subject", "Ok");
                }
                else
                {
                    XFToast.ShortMessage("Please select any subject");
                }
            }


            SetBusyIndicator(false);
        }

        public async void GetDataCourse()
        {
            SetBusyIndicator(true);

            CourseList = new List<CourseInfo>();
            CourseList.Clear();
            coursePick.Items.Clear();
            string url = "http://13.126.189.26:10008/api/Get_CourseList/" + subjectId;

            JObject data = c.GetDetail(url);

            IList<JToken> results = data["list"].Children().ToList();

            if (results.Count != 0)
            {
                foreach (JToken result in results)
                {
                    System.Diagnostics.Debug.WriteLine(result); //just to check my json data.
                    CourseInfo searchResult = JsonConvert.DeserializeObject<CourseInfo>(result.ToString()); //get exception on this line.
                    coursePick.Items.Add(searchResult.CourseName);
                    coursePick.IsEnabled = true;
                    CourseList.Add(searchResult);
                }

            }
            else
            {
                
                if (Device.OS == TargetPlatform.iOS)
                {
                    coursePick.IsEnabled = false;
                    await DisplayAlert("Message", "No Courses in this subject", "Ok");
                }
                else
                {
                    coursePick.IsEnabled = false;
                    XFToast.ShortMessage("No Courses in this subject");
                }
                //coursePick.Items.Add("No Courses in this subject ):");
                //coursePick.SelectedIndex = -1;
            }
            SetBusyIndicator(false);
        }
    }
}
