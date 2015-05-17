using Coupons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Coupons.DAL
{
    public class CouponsInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CouponsContext>
    {
        protected override void Seed(CouponsContext context)
        {

            var Category = new List<Category>
            {
            new Category{ID=1, category="food"},
            new Category{ID=2,category="Entertainment"},
            new Category{ID=3, category="HealthAndBeauty"},
            new Category{ID=4, category="Apparel"},
            new Category{ID=5, category="Electronics"}                        
            };
            Category.ForEach(s => context.Category.Add(s));
            context.SaveChanges();

            var Status = new List<Status>
            {
            new Status{ID=1, status="AwaitsApproval"},
            new Status{ID=2, status="Approved"},
            new Status{ID=3, status="Active"},
            new Status{ID=4, status="Inactive"}                       
            };

            Status.ForEach(s => context.Status.Add(s));
            context.SaveChanges();


            var applicationUserManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var customer = new Customer() { UserName = "israel@gmail.com", firstName = "Israel", lastName = "Israeli", Email = "israel@gmail.com"};
            var result = applicationUserManager.Create(customer, "Pp123456!");
            var result2 = applicationUserManager.Create(new Admin() { UserName ="litalmor5@gmail.com", firstName = "Lital", lastName = "Israeli", Email = "litalmor5@gmail.com" }, "Pp123456!");

            var owner = new Owner() { UserName="shani.elha@gmail.com", firstName = "Shani", lastName = "Israeli", Email = "shani.elha@gmail.com"};

            var result3 = applicationUserManager.Create(owner, "Pp123456!");

            //var Customer = new List<Customer>
            //{
            //new Customer{Id="111111111",firstName="Israel",lastName="Israeli",Email="israel@gmail.com",PhoneNumber="050-1111111",PasswordHash="1111",age=25,},
            ////new Customer{ID="222222222",firstName="Dan",lastName="Daniel",email="dan@gmail.com",phone="050-2222222",password="2222",age=26,}

            //};

            //Customer.ForEach(s => context.Customer.Add(s));
            //context.SaveChanges();

            //var Owner = new List<Owner>
            //{
            ////new Owner{ID="333333333",firstName="Tal",lastName="Tali",email="tal@gmail.com",phone="050-3333333",password="3333",},
            ////new Owner{ID="444444444",firstName="Avi",lastName="Aviv",email="avi@gmail.com",phone="050-4444444",password="4444",},
            
            //};
            //Owner.ForEach(s => context.Owner.Add(s));
            //context.SaveChanges();

            //var Admin = new List<Admin>
            //{
            ////new Admin{ID="000000000",firstName="Dana",lastName="Daniel",email="dana@gmail.com",phone="050-0000000",password="0000",}
            //};
            //Admin.ForEach(s => context.Admin.Add(s));
            //context.SaveChanges();
            
            var Business = new List<Business>
            {
            new Business{ID=1,name="mcDonalds",Owner=owner,categoryID=1,description="hamburgers", address="rager 20 Beer-sheva",city="Beer-Sheva",moneyEarned=100}
            };
            Business.ForEach(s => context.Business.Add(s));
            context.SaveChanges();

            var CouponMaker = new List<CouponMaker>
            {
            new CouponMaker{ID=1,Name="mcCoupon",description="best price", originalPrice=50,couponPrice=25,rating=2,numOfRaters=1,startDate=DateTime.Parse("2015-04-04"),endDate=DateTime.Parse("2015-05-05"),quantity=0,maxQuantity=100,StatusID="Active",BusinessID=1},
            new CouponMaker{ID=2,Name="scCoupon",description="best price", originalPrice=40,couponPrice=10,rating=2,numOfRaters=1,startDate=DateTime.Parse("2015-04-05"),endDate=DateTime.Parse("2015-05-06"),quantity=0,maxQuantity=70,StatusID="Active",BusinessID=1,}

            };
            CouponMaker.ForEach(s => context.CouponMaker.Add(s));
            context.SaveChanges();

            var Coupon = new List<Coupon>
            {
            new Coupon{ID=2,isActive=true,CouponMakerID=1,CustomerID=customer.Id,}
            };
            Coupon.ForEach(s => context.Coupon.Add(s));
            context.SaveChanges();

        }
    }
}