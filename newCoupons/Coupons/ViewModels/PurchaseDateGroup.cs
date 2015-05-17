using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Coupons.ViewModels
{
    public class PurchaseDateGroup
    {
        [DataType(DataType.Date)]
        public int? Age { get; set; }

        public int CustomerCount { get; set; }
    }
}