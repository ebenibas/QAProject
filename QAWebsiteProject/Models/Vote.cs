using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QAWebsiteProject.Models
{
    public class Vote
    {
        public int VoteId { get; set; }
        public int Value { get; set; }
        public virtual Question question { get; set; }
        public string UserId { get; set; }
    }
}