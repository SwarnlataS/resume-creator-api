using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resume_creator_api.ViewModels
{
    public class MasterDetailVM
    {
        public int ID { get; set; }
        public string DisplayText { get; set; }
        public int DisplayOrder { get; set; }
        public string MasterType { get; set; }
        public bool IsDeleted { get; set; }
    }
}