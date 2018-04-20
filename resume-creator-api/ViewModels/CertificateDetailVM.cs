using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resume_creator_api.ViewModels
{
    public class CertificateDetailVM
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string Title { get; set; }
        public string Institute { get; set; }
        public int FromYear { get; set; }
        public Nullable<int> ToYear { get; set; }
        public bool HasNoExpiry { get; set; }
        public int DisplayOrder { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    }
}