using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Coupons.DAL;
using Coupons.Models;

namespace CouponTestUnit
{
    [TestClass]
    public class UnitTest1
    {
        /* Customer Encryption */
        [TestMethod]
        public void TestMethod0()
        {
            Customer cust = new Customer();
            Assert.AreNotEqual("mypass123", cust.EncryptPass("mypass123"));
        }
        /* Customer Permission */
        [TestMethod]
        public void TestMethod1()
        {
            Customer cust = new Customer();
            Assert.AreEqual(false, cust.Permissions.EditDeleteBusiness);
        }
        [TestMethod]
        public void TestMethod2()
        {
            Customer cust = new Customer();
            Assert.AreEqual(false, cust.Permissions.EditDeleteCoupon);
        }
        [TestMethod]
        public void TestMethod3()
        {
            Customer cust = new Customer();
            Assert.AreEqual(false, cust.Permissions.EditDeleteCouponMaker);
        }
        [TestMethod]
        public void TestMethod4()
        {
            Customer cust = new Customer();
            Assert.AreEqual(false, cust.Permissions.CreateBusiness);
        }
        [TestMethod]
        public void TestMethod5()
        {
            Customer cust = new Customer();
            Assert.AreEqual(false, cust.Permissions.CreateCoupon);
        }
        [TestMethod]
        public void TestMethod6()
        {
            Customer cust = new Customer();
            Assert.AreEqual(false, cust.Permissions.DeleteAndApproveOrderCoupon);
        }

        /* Owner Permission */

        [TestMethod]
        public void TestMethod7()
        {
            Owner o = new Owner();
            Assert.AreEqual(true, o.Permissions.CreateCoupon);
        }
        [TestMethod]
        public void TestMethod8()
        {
            Owner o = new Owner();
            Assert.AreEqual(false, o.Permissions.DeleteAndApproveOrderCoupon);
        }
        [TestMethod]
        public void TestMethod9()
        {
            Owner o = new Owner();
            Assert.AreEqual(false, o.Permissions.EditDeleteCouponMaker);
        }
        [TestMethod]
        public void TestMethod10()
        {
            Owner o = new Owner();
            Assert.AreEqual(false, o.Permissions.CreateBusiness);
        }
        [TestMethod]
        public void TestMethod11()
        {
            Owner o = new Owner();
            Assert.AreEqual(false, o.Permissions.EditDeleteCoupon);
        }

        /*Admin Permissions*/

        [TestMethod]
        public void TestMethod12()
        {
            Admin ad = new Admin();
            Assert.AreEqual(true, ad.Permissions.EditDeleteCouponMaker);
        }
        [TestMethod]
        public void TestMethod13()
        {
            Admin ad = new Admin();
            Assert.AreEqual(true, ad.Permissions.CreateBusiness);
        }
        [TestMethod]
        public void TestMethod14()
        {
            Admin ad = new Admin();
            Assert.AreEqual(true, ad.Permissions.EditDeleteCoupon);
        }
        [TestMethod]
        public void TestMethod15()
        {
            Admin ad = new Admin();
            Assert.AreEqual(true, ad.Permissions.EditDeleteBusiness);
        }
        [TestMethod]
        public void TestMethod16()
        {
            Admin ad = new Admin();
            Assert.AreEqual(false, ad.Permissions.CreateCoupon);
        }
    }
}
