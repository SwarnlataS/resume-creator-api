//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace resume_creator_api.edmx
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProfile
    {
        public int ID { get; set; }
        public int LoginID { get; set; }
        public string Objective { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int State { get; set; }
        public int Country { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public System.DateTime UpdatedOn { get; set; }
    
        public virtual LoginProfile LoginProfile { get; set; }
    }
}
