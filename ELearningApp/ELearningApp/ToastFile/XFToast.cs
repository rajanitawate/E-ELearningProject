using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ELearningApp.ToastFile
{
   public static class XFToast
    {
        public static void ShortMessage(string message)
        {
            DependencyService.Get<IMessage>().ShortAlert(message);
        }

        public static void LongMessage(string message)
        {
            DependencyService.Get<IMessage>().LongAlert(message);
        }

    }
}
