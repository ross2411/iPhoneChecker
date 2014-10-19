using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iPhoneChecker;
using System.IO;
using System.Linq;
using System.Collections.Generic;
namespace iPhoneTester
{
    [TestClass]
    public class UnitTest1
    {

        public string AvailabilityText { get; set; }
        public string StoresText { get; set; }



        [TestInitialize]
        public void LoadFiles()
        {
            //Open test file
            var f1 = File.OpenText(@"C:\Users\Ross\Documents\Visual Studio 2013\Projects\iPhoneChecker\iPhoneTester\TestData\availability.json");
            AvailabilityText = f1.ReadToEnd();
            f1.Close();

            var f2 = File.OpenText(@"C:\Users\Ross\Documents\Visual Studio 2013\Projects\iPhoneChecker\iPhoneTester\TestData\stores.json");
            StoresText = f2.ReadToEnd();
            f2.Close();
        }

        
        [TestMethod]
        public void AnyGreyIphones64GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(true, "64sg");
            Assert.IsTrue(stores.Count > 0);
        
        }
        [TestMethod]
        public void AnyGreyIphones64GBInStockOracle()
        {
            Lookup lk = new Lookup();
            var stores = lk.PhoneAvailable(true, "64sg");
            Assert.IsTrue( stores.Where(m=>m.ToLower().Contains("oracle")).Count() > 0);
        }

        [TestMethod]
        public void AnyGreyIphones64GBInStockFestivalPlace()
        {
            Lookup lk = new Lookup();
            var stores = lk.PhoneAvailable(true, "64sg");
            Assert.IsTrue(stores.Where(m => m.ToLower().Contains("festival")).Count() > 0);
        }

        [TestMethod]
        public void DebuggingTest()
        {
            Lookup lookup = new Lookup(this.AvailabilityText,this.StoresText);

            var stores = lookup.PhoneAvailable(false, "64sg");
            Assert.IsTrue(stores.Count > 0);

        }

        //Check all types of iPhone 6
        [TestMethod]
        public void AnyGreyIphones16GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(true, "16sg");
            Assert.IsTrue(stores.Count > 0);

        }

        [TestMethod]
        public void AnyGreyIphones128GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(true, "128sg");
            Assert.IsTrue(stores.Count > 0);

        }

        [TestMethod]
        public void AnySilverIphones16GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(true, "16s");
            Assert.IsTrue(stores.Count > 0);

        }

        [TestMethod]
        public void AnySilverIphones64GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(true, "64s");
            Assert.IsTrue(stores.Count > 0);

        }

        [TestMethod]
        public void AnySilverIphones128GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(true, "128s");
            Assert.IsTrue(stores.Count > 0);

        }


        [TestMethod]
        public void AnyGoldIphones16GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(true, "16g");
            Assert.IsTrue(stores.Count > 0);
        }

        [TestMethod]
        public void AnyGoldIphones64GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(true, "64g");
            Assert.IsTrue(stores.Count > 0);
        }

        [TestMethod]
        public void AnyGoldIphones128GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(true, "128g");
            Assert.IsTrue(stores.Count > 0);
        }
    }
}
