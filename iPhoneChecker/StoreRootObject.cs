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
        public bool? iPhone6_16_Silver {get;set;}
        public bool? iPhone6_64_Silver {get;set;}
        public bool? iPhone6_128_Silver {get;set;}
        public bool? iPhone6_16_Grey {get;set;}
        public bool? iPhone6_64_Grey {get;set;}
        public bool? iPhone6_128_Grey {get;set;}
        public bool? iPhone6_16_Gold {get;set;}
        public bool? iPhone6_64_Gold {get;set;}
        public bool? iPhone6_128_Gold {get;set;}
    }

    public class Stores
    {
        public string updatedTime { get; set; }
        public List<Store> stores { get; set; }
        public string timezone { get; set; }
        public string updatedDate { get; set; }
        public string reservationURL { get; set; }
    }
}