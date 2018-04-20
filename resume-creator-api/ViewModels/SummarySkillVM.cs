using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resume_creator_api.ViewModels
{
    public class SummarySkillVM
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string DisplayText { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}