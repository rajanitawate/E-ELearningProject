using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;

namespace ELearningApp.Model
{
   public class MajorTopicInfo
    {
        public string MajorTopicTitle { get; set; }
        public string MajorTopicId { get; set; }

        public List<TopicInfo> listTopic { get; set; }
        
    }

    
}
