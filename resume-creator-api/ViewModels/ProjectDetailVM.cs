using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resume_creator_api.ViewModels
{
    public class ProjectDetailVM
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string Client { get; set; }
        public string ProjectTitle { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public Nullable<int> ToMonth { get; set; }
        public Nullable<int> ToYear { get; set; }
        public bool IsCurrent { get; set; }
        public string ProjectLocation { get; set; }
        public bool IsOnsite { get; set; }
        public int EmploymentType { get; set; }
        public string ProjectDetails { get; set; }
        public int Role { get; set; }
        public string RoleDescription { get; set; }
        public int TeamSize { get; set; }
        public string SkillsUsed { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}