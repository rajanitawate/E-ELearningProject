using System;
using System.Collections.Generic;
using System.Text;

namespace ELearningApp.Model
{
  public class EvaluationQuestionInfo
    {
        public string EvaluationName { get; set; }
        public int EvaluationId { get; set; }
        public string CourseName { get; set; }
        public string MajorTopicTitle { get; set; }
        public string Description { get; set; }
        public List<QuestionInfo> listQuestion { get; set; }      
    }

    public class QuestionInfo
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }
        public int ChoiceType { get; set; }
        public int EvaluationId { get; set; }
        public List<AnswerInfo> listAnswer { get; set;}
    }
    public class AnswerInfo
    {
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string Answer { get; set; }
        public bool IsSelected { get; set; }    
    }    
}
