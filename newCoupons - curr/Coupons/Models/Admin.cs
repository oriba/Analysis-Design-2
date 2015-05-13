using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace Coupons.Models
{
    public class Admin : ApplicationUser
    {

        public override IPermissions Permissions
        {
            get { return AdminPermissions.Default; }
        }

    }
}