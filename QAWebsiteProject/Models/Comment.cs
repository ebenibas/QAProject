using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAWebsiteProject.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }

        public string Name { get; set; }
        public int QuestionID { get; set; }
    }
}