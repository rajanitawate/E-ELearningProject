using System;
using System.Collections.Generic;
using System.Text;

namespace ELearningApp.Model
{
   public class CourseInfo
    {
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string Image { get; set; }
        public string Imagepath { get; set; }
    }
}
