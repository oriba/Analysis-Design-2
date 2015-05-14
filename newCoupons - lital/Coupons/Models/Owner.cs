using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coupons.Models
{
    public class Owner : ApplicationUser
    {
    
        public virtual ICollection<Business> Businesses { get; set; }
        
        public override IPermissions Permissions
        {
            get { return OwnerPermissions.Default; }
        }
    }
}
