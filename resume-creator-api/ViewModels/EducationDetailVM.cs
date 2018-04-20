using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resume_creator_api.ViewModels
{
    public class EducationDetailVM
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string Title { get; set; }
        public string Specialization { get; set; }
        public string University { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public Nullable<int> ToMonth { get; set; }
        public Nullable<int> ToYear { get; set; }
        public bool IsCurrent { get; set; }
        public Nullable<double> Percentage { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}