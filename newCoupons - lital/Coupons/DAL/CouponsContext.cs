using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coupons.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Coupons.DAL
{
    public class CouponsContext : ApplicationDbContext
    {
        public CouponsContext() : base("CouponsContext")
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Owner> Owner { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<Coupon> Coupon { get; set; }
        public DbSet<CouponMaker> CouponMaker { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Status> Status { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}