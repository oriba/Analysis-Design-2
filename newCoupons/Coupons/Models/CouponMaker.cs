using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coupons.Models
{

    public class CouponMaker
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public int originalPrice { get; set; }
        public int couponPrice { get; set; }
        public int rating { get; set; }
        public int numOfRaters { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int quantity { get; set; }
        public int maxQuantity { get; set; }
        public string StatusID { get; set; }
        public int BusinessID { get; set; }
        
        public virtual Business Business { get; set; }
        public virtual Status Status { get; set; }
    }
}
