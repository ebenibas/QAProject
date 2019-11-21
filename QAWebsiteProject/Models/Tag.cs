using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAWebsiteProject.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public virtual ICollection<QuestionTag> QuestionTags { get; set; }
    }
}