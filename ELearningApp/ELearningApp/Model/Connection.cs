using ELearningApp.DataBase;
using ELearningApp.ToastFile;
using ELearningApp.View;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ELearningApp.Model
{
   public class Connection
    {
        
        public Connection()
        {
                             
        }

        public JObject GetDetail(string Url)
        {
            HttpWebRequest request = WebRequest.Create(Url) as HttpWebRequest;

            string content1;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                content1 = reader.ReadToEnd();
            }

            JObject data = JObject.Parse(content1);

            return data;
        }

        public async Task<UserInfo> SaveUser(UserInfo userInfo, string url)
        {
            var httpclient = new HttpClient();
            var json = JsonConvert.SerializeObject(userInfo);
            HttpContent httpContent = new StringContent(json);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            var result = await httpclient.PostAsync(url, httpContent);

            if (result.IsSuccessStatusCode)
            {
                UserInfo data = new UserInfo();
                
                dynamic a= JObject.Parse(result.Content.ReadAsStringAsync().Result).ToString();
                string msg = JObject.Parse(a)["Messages"].ToString();
                              
                if (msg != "Failed")
                {
                    data = JsonConvert.DeserializeObject<UserInfo>(result.Content.ReadAsStringAsync().Result);
                    User user = new User
                    {
                        Status = true
                    };
                    await SplashPage.Database.SaveUserAsync(user);
                }
                else
                {
                    data.Messages = "Failed";
                    data.UserId = "0";
                }
               
                return data;              
            }
            else
            {
                return null;
            }
         
        }

      
    }
}
