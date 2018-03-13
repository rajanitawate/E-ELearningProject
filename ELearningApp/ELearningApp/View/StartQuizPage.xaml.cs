using ELearningApp.Model;
using ELearningApp.ToastFile;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ELearningApp.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartQuizPage : ContentPage
    {
        public string url;
        public int quesNo = 1, lstCnt = 1, Qcount = 0;
       
        Connection c = new Connection();

        EvaluationQuestionInfo questionInfo = new EvaluationQuestionInfo();

        List<AnswerInfo> answerList = new List<AnswerInfo>();

        List<EvaluationQuestionInfo> list = new List<EvaluationQuestionInfo>();      

        public StartQuizPage(int courseid)
        {
            InitializeComponent();

            SetBusyIndicator(true);

            url = "http://13.126.189.26:10008/api/getEvaluation-by-majorTopic/0/" + courseid;

            GetData();

            if (list[0].listQuestion.Count == 0)
            {
                lbltxt.IsVisible = true;
                stackQuestion.IsVisible = false;
            }
            else
            {
                stackQuestion.IsVisible = true;
                lbltxt.IsVisible = false;
                for (int i = 0; i < list.Count; i++)
                {
                    Qcount = Qcount + list[i].listQuestion.Count;
                }

                lblQuestion.Text = list[0].listQuestion[0].Question;
                lblquesNo.Text = "Q." + quesNo;

                listAns.ItemsSource = list[0].listQuestion[0].listAnswer;

                answerList = list[0].listQuestion[0].listAnswer;
                var multiPage = new SelectMultipleBasePage<AnswerInfo>(answerList);
            }
            

        }

        protected override void OnAppearing()
        {
            SetBusyIndicator(false);
        }

        private void ListItem_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            listAns.SelectedItem = null;
            //var item = (AnswerInfo)e.SelectedItem;
            //item.IsSelected = true;
            //Switch mainSwitch = new Switch();
            //mainSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));
        }
          
        public void GetData()
        {
            JObject data = c.GetDetail(url);

            IList<JToken> results = data["list"].Children().ToList();

            foreach (JToken result in results)
            {
                System.Diagnostics.Debug.WriteLine(result); //just to check my json data.
                EvaluationQuestionInfo searchResult = JsonConvert.DeserializeObject<EvaluationQuestionInfo>(result.ToString()); //get exception on this line.
                list.Add(searchResult);
            }
            SetBusyIndicator(false);
        }

        async void UpBlue_Tapped(object sender, System.EventArgs e)
        {
            //show answer.

            PageDown.IsVisible = false;
            await PageDown.TranslateTo(0, 0, 500, Easing.SinIn);
            var a = answerList.Where(item => item.IsSelected == item.IsCorrect).Select(wrappedItem => new { wrappedItem.Answer, wrappedItem.IsCorrect, wrappedItem.IsSelected }).ToList();
            var b = answerList.Where(item => item.IsSelected == true).Select(wrappedItem => new { wrappedItem.Answer, wrappedItem.IsCorrect, wrappedItem.IsSelected }).ToList();

            if (b.Count != 0)
            {
                if (a.Count == answerList.Count)
                {
                    rightAns.IsVisible = true;
                    wrongAns.IsVisible = false;
                    PageDown.IsVisible = true;
                    stackMain.IsEnabled = false;
                    txtrightAns.Text = "Congrats Your Answer Is Right";

                }
                else
                {
                    var correctAns = answerList.Where(item => item.IsCorrect == true).Select(wrappedItem => wrappedItem.Answer).ToList();

                    string answer = "";
                    for (int x = 0; x < correctAns.Count(); x++)
                    {
                        answer = answer + correctAns[x] + ", ";
                    }

                    rightAns.IsVisible = false;
                    wrongAns.IsVisible = true;
                    PageDown.IsVisible = true;
                    stackMain.IsEnabled = false;
                    txtwrongAns.Text = "Your Right Answer Is :" + answer;
                }
            }

            else
            {
                PageDown.IsVisible = false;
                if (Device.OS == TargetPlatform.iOS)
                {
                    await DisplayAlert("Message", "Select any answer", "Ok");
                }
                else
                {
                    XFToast.ShortMessage("Select any answer");
                }              
                
            }
        }

        async void DownWhite_Tapped(object sender, System.EventArgs e)
        {
            //change question
            stackMain.IsEnabled = true;
            await PageDown.TranslateTo(0, Page.Height, 500, Easing.SinIn);

            var a = answerList.Where(item => item.IsSelected == item.IsCorrect).Select(wrappedItem => new { wrappedItem.Answer, wrappedItem.IsCorrect, wrappedItem.IsSelected }).ToList();
            var b = answerList.Where(item => item.IsSelected == true).Select(wrappedItem => new { wrappedItem.Answer, wrappedItem.IsCorrect, wrappedItem.IsSelected }).ToList();

            if (b.Count != 0)
            {
                quesNo++;
                if (a.Count == answerList.Count)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        for (int j = 0; j < list[i].listQuestion.Count; j++)
                        {
                            if (i == 0 && j == 0)
                            {
                            }
                            else
                            {
                                lstCnt++;
                                if (quesNo == lstCnt)
                                {
                                    lblQuestion.Text = list[i].listQuestion[j].Question;
                                    lblquesNo.Text = "Q." + quesNo;
                                    listAns.ItemsSource = list[i].listQuestion[j].listAnswer;
                                    answerList = list[i].listQuestion[j].listAnswer;
                                    var multiPage = new SelectMultipleBasePage<AnswerInfo>(answerList);
                                    SetBusyIndicator(false);
                                }
                            }
                        }
                    }
                    if (Qcount == quesNo)
                    {
                        var result = await DisplayAlert("Message", "Quiz Completed", "ok", "");
                        if (result == true)
                        {
                            await Navigation.PushAsync(new MasterPage());
                            SetBusyIndicator(false);
                        }
                        else
                        {
                            return;
                        }
                    }
                    lstCnt = 1;
                    SetBusyIndicator(false);
                }

                else
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        for (int j = 0; j < list[i].listQuestion.Count; j++)
                        {
                            if (i == 0 && j == 0)
                            {
                            }
                            else
                            {
                                lstCnt++;
                                if (quesNo == lstCnt)
                                {
                                    lblQuestion.Text = list[i].listQuestion[j].Question;
                                    lblquesNo.Text = "Q." + quesNo;
                                    listAns.ItemsSource = list[i].listQuestion[j].listAnswer;
                                    answerList = list[i].listQuestion[j].listAnswer;
                                    var multiPage = new SelectMultipleBasePage<AnswerInfo>(answerList);
                                    SetBusyIndicator(false);
                                }
                            }
                        }
                    }

                    if (Qcount == quesNo)
                    {
                        var result = await DisplayAlert("Message", "Quiz Completed", "ok", "cancel");
                        if (result == true)
                        {
                            await Navigation.PushAsync(new MasterPage());
                            SetBusyIndicator(false);
                        }
                        else
                        {
                            return;
                        }
                    }
                    lstCnt = 1;
                    SetBusyIndicator(false);
                }

            }
            else
            {
                if (Device.OS == TargetPlatform.iOS)
                {
                    await DisplayAlert("Message", "Select any answer", "Ok");
                }
                else
                {
                    XFToast.ShortMessage("Select any answer");
                }
                               
            }

        }

        public void SetBusyIndicator(bool isBusyIndicatorIsVisible) => BusyIndicator.IsRunning = BusyIndicator.IsVisible = isBusyIndicatorIsVisible;
    }
}

public class SelectMultipleBasePage<T> : ContentPage
{
    public class WrappedSelection<T> : INotifyPropertyChanged
    {
        public T Item { get; set; }
        bool isSelected = false;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }

    public class WrappedItemSelectionTemplate : ViewCell
    {
        public WrappedItemSelectionTemplate() : base()
        {
            Label name = new Label();
            name.SetBinding(Label.TextProperty, new Binding("Item.Name"));
            Switch mainSwitch = new Switch();
            mainSwitch.SetBinding(Switch.IsToggledProperty, new Binding("IsSelected"));
            RelativeLayout layout = new RelativeLayout();
            layout.Children.Add(name,
                Constraint.Constant(5),
                Constraint.Constant(5),
                Constraint.RelativeToParent(p => p.Width - 60),
                Constraint.RelativeToParent(p => p.Height - 10)
            );
            layout.Children.Add(mainSwitch,
                Constraint.RelativeToParent(p => p.Width - 55),
                Constraint.Constant(5),
                Constraint.Constant(50),
                Constraint.RelativeToParent(p => p.Height - 10)
            );
            View = layout;
        }
    }

    public List<WrappedSelection<T>> WrappedItems = new List<WrappedSelection<T>>();

    public SelectMultipleBasePage(List<T> items)
    {
        WrappedItems = items.Select(item => new WrappedSelection<T>() { Item = item, IsSelected = false }).ToList();

        ListView mainList = new ListView()
        {
            ItemsSource = WrappedItems,
            ItemTemplate = new DataTemplate(typeof(WrappedItemSelectionTemplate)),
        };
        mainList.ItemSelected += (sender, e) =>
        {
            if (e.SelectedItem == null) return;
            var o = (WrappedSelection<T>)e.SelectedItem;
            o.IsSelected = !o.IsSelected;
            ((ListView)sender).SelectedItem = null; //de-select
        };
        Content = mainList;
        // ToolbarItems.Add(new ToolbarItem("All", null, SelectAll, ToolbarItemOrder.Primary));
        // ToolbarItems.Add(new ToolbarItem("None", null, SelectNone, ToolbarItemOrder.Primary));
    }

    public List<T> GetSelection()
    {
        return WrappedItems.Where(item => item.IsSelected).Select(wrappedItem => wrappedItem.Item).ToList();
    }
}
