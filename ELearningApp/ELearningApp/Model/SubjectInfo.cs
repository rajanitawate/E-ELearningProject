using System;
using System.Collections.Generic;
using System.Text;

namespace ELearningApp.Model
{
   public class SubjectInfo
    {
        public int SubjectID { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectOverview { get; set; }
        public string SubjectName { get; set; }    
        public bool IsActive { get; set; }
    }
        
}
