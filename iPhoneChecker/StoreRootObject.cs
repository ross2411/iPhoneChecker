using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPhoneChecker
{
    public class Store
    {
        public string storeNumber { get; set; }
        public string storeName { get; set; }
        public bool storeEnabled { get; set; }
    }

    public class StoreRootObject
    {
        public string updatedTime { get; set; }
        public List<Store> stores { get; set; }
        public string timezone { get; set; }
        public string updatedDate { get; set; }
        public string reservationURL { get; set; }
    }
}