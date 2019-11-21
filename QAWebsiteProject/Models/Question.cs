using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAWebsiteProject.Models
{
    public class Question
    {
        public int QuestionID { get; set; }
        public string Title { get; set; }
        public string description { get; set; }
        public DateTime DatePosted { get; set; }

        public virtual ICollection<QuestionTag> QuestionTags { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}