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
            var stores = lookup.PhoneAvailable(ModelCode.iPhone6Grey64GB);
            Assert.IsTrue(stores.Count > 0);
        
        }
        [TestMethod]
        public void AnyGreyIphones64GBInStockOracle()
        {
            Lookup lk = new Lookup();
            var stores = lk.PhoneAvailable(ModelCode.iPhone6Grey64GB);
            Assert.IsTrue( stores.Where(m=>m.ToLower().Contains("oracle")).Count() > 0);
        }

        [TestMethod]
        public void AnyGreyIphones64GBInStockFestivalPlace()
        {
            Lookup lk = new Lookup();
            var stores = lk.PhoneAvailable(ModelCode.iPhone6Grey64GB);
            Assert.IsTrue(stores.Where(m => m.ToLower().Contains("festival")).Count() > 0);
        }

        [TestMethod]
        public void DebuggingTest()
        {
            Lookup lookup = new Lookup(this.AvailabilityText,this.StoresText);

            var stores = lookup.PhoneAvailable(ModelCode.iPhone6Grey64GB);
            Assert.IsTrue(stores.Count > 0);

        }

        //Check all types of iPhone 6
        [TestMethod]
        public void AnyGreyIphones16GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(ModelCode.iPhone6Grey16GB);
            Assert.IsTrue(stores.Count > 0);

        }

        [TestMethod]
        public void AnyGreyIphones128GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable( ModelCode.iPhone6Grey128GB);
            Assert.IsTrue(stores.Count > 0);

        }

        [TestMethod]
        public void AnySilverIphones16GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(ModelCode.iPhone6Silver16GB);
            Assert.IsTrue(stores.Count > 0);

        }

        [TestMethod]
        public void AnySilverIphones64GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(ModelCode.iPhone6Silver64GB);
            Assert.IsTrue(stores.Count > 0);

        }

        [TestMethod]
        public void AnySilverIphones128GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(ModelCode.iPhone6Silver128GB);
            Assert.IsTrue(stores.Count > 0);

        }


        [TestMethod]
        public void AnyGoldIphones16GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable( ModelCode.iPhone6Gold16GB);
            Assert.IsTrue(stores.Count > 0);
        }

        [TestMethod]
        public void AnyGoldIphones64GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(ModelCode.iPhone6Gold64GB);
            Assert.IsTrue(stores.Count > 0);
        }

        [TestMethod]
        public void AnyGoldIphones128GB()
        {
            Lookup lookup = new Lookup();
            var stores = lookup.PhoneAvailable(ModelCode.iPhone6Gold128GB);
            Assert.IsTrue(stores.Count > 0);
        }
    }
}
