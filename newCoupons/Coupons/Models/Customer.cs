using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;


namespace Coupons.Models
{
    public class Customer : ApplicationUser
    {

        public int age { get; set; }

        public virtual ICollection<Coupon> Coupons { get; set; }

        public override IPermissions Permissions
        {
            get { return CustomerPermissions.Default; }
        }

        public string EncryptPass(string prevPass)
        {
            string ans = "";
            foreach (char c in ans)
            {
                ans = ans + ((char)((c + 10) % 200));
            }
            return (ans);
        }

    }
}
