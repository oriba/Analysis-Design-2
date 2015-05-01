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
        public int categoryID { get; set; }
        public string description { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public int moneyEarned { get; set; }

        public virtual Owner Owner { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<CouponMaker> couponMakers { get; set; }
    }
}