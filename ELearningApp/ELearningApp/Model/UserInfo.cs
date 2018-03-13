using System;
using System.Collections.Generic;
using System.Text;

namespace ELearningApp.Model
{
   public class UserInfo
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool IsAgree { get; set; }
        public string UserType { get; set; }
        public string Messages { get; set; }
    }
}
