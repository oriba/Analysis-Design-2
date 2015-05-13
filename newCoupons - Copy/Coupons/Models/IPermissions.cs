using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coupons.Models
{
    public interface IPermissions
    {

        bool ListCustomers { get; }

        bool ListOwners { get; }

    }

    public class CustomerPermissions : IPermissions
    {
        public static readonly IPermissions Default = new CustomerPermissions();

        private CustomerPermissions()
        {

        }

        public bool ListCustomers
        {
            get { return false; }
        }

        public bool ListOwners
        {
            get { return false; }
        }
    }


    public class OwnerPermissions : IPermissions
    {
        public static readonly IPermissions Default = new OwnerPermissions();

        private OwnerPermissions()
        {

        }

        public bool ListCustomers
        {
            get { return false; }
        }

        public bool ListOwners
        {
            get { return false; }
        }
    }

    public class AdminPermissions : IPermissions
    {
        public static readonly IPermissions Default = new AdminPermissions();

        private AdminPermissions()
        {

        }

        public bool ListCustomers
        {
            get { return true; }
        }

        public bool ListOwners
        {
            get { return true; }
        }
    }

    public class GuestPermissions : IPermissions
    {
        public static readonly IPermissions Default = new GuestPermissions();

        private GuestPermissions()
        {

        }

        public bool ListCustomers
        {
            get { return false; }
        }

        public bool ListOwners
        {
            get { return false; }
        }
    }

}
