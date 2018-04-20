using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resume_creator_api.ViewModels
{
    public class TechnicalSkillVM
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string Title { get; set; }
        public string Version { get; set; }
        public int LastUsed { get; set; }
        public int ExperienceYear { get; set; }
        public int ExperienceMonth { get; set; }
        public int ProfileID { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}