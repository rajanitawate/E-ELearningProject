using System.IO;
using Xamarin.Forms;
using ELearningApp.Droid;

[assembly: Dependency(typeof(LocalFileHelper_User))]
namespace ELearningApp.Droid
{
   public class LocalFileHelper_User : ILocalFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string docFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder);
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, fileName);
        }
    }
}