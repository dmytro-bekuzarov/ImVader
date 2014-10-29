using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImVaderWebsite.Models
{
    public class ContactForm
    {
        public String Name { get; set; }
        public String Email { get; set; }
        public String Phone { get; set; }
        public String Title { get; set; }
        public String Message { get; set; }
    }
}