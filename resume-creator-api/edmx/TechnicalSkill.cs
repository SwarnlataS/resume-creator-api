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
    
    public partial class TechnicalSkill
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
    
        public virtual LoginProfile LoginProfile { get; set; }
    }
}
