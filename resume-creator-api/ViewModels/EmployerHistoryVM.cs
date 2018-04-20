using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resume_creator_api.ViewModels
{
    public class EmployerHistoryVM
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string EmployerName { get; set; }
        public bool IsCurrent { get; set; }
        public int FromMonth { get; set; }
        public int FromYear { get; set; }
        public Nullable<int> ToMonth { get; set; }
        public Nullable<int> ToYear { get; set; }
        public string Designation { get; set; }
        public int TeamSize { get; set; }
        public string JobProfile { get; set; }
        public int NoticePeriod { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}