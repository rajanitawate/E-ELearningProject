using System;
using System.Collections.Generic;
using System.Text;

namespace ELearningApp.Model
{
    public class TopicInfo
    {
        public int TopicId { get; set; }
      //public int MajorTopicId { get; set; }

        public string TopicName { get; set; }

        public string TopicFormatId { get; set; }

        public string TopicOverview { get; set; }
        public string FilePath { get; set; }
        public string ImagePath { get; set; }
        public string TopicTranscript { get; set; }

        public string FullPathPDF { get; set; }


        //public TopicInfo(string topicName,int tId,string formateId)
        //{
        //    this.TopicName = topicName;
        //    this.TopicId = tId;
        //    this.TopicFormatId = formateId;
        //}
    }
}
