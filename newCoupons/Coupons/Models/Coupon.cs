using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Coupons.Models
{
   
    public class Coupon
    {
        public int ID { get; set; }
        public bool isActive { get; set; }
        public int CouponMakerID { get; set; }
        public string CustomerID { get; set; }

        public virtual CouponMaker CouponMaker { get; set; }
        public virtual Customer Customer { get; set; }
    }
}