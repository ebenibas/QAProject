using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAWebsiteProject.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionID { get; set; }
 

        public string Content { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}