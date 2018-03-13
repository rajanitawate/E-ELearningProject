using Android.Widget;
using Xamarin.Forms;
using ELearningApp.ToastFile;

[assembly: Dependency(typeof(ELearningApp.Droid.MessageAndroid))]
namespace ELearningApp.Droid
{
   public class MessageAndroid : IMessage
    {
        public void LongAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
        }
        public void ShortAlert(string message)
        {
            Toast.MakeText(Android.App.Application.Context, message, ToastLength.Short).Show();
        }
    }
}