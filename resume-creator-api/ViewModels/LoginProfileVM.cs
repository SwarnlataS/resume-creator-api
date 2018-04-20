using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace resume_creator_api.ViewModels
{
    public class LoginProfileVM
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public System.DateTime CreatedOn { get; set; }
    }
}