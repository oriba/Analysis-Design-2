using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coupons.Models
{

    public class Business
    {
        public int ID { get; set; }
        public string name { get; set; }
        public string ownerID { get; set; }
        public string categoryID { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int moneyEarned { get; set; }

        public virtual Owner owner { get; set; }
        public virtual Category category { get; set; }
    }
}