using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Xml.Linq;

namespace iPhoneChecker
{
    public class Lookup
    {
        private string _availabilityString { get; set; }
        private string _storesString { get; set; }

        public Lookup()
        {
            _availabilityString = null;
            _storesString = null;
        }

        public Lookup(string AvailabilityString, string StoresString)
        {
            _availabilityString = AvailabilityString;
            _storesString = StoresString;
        }

        public List<string> PhoneAvailable(bool Available, string ModelCode) {

            //Convert Model code to Model number using method below
            var modelNumber = this.GetModelNumber(ModelCode);

            //These are the web addresses.  Should really be parsing them in but hard coded for now
            var storesURL = "https://reserve.cdn-apple.com/GB/en_GB/reserve/iPhone/stores.json";
            var availabilityURL = "https://reserve.cdn-apple.com/GB/en_GB/reserve/iPhone/availability.json";

            WebClient wc = new WebClient();

            //If I've passing the json string in use that otherwise use the URL
            if (string.IsNullOrEmpty(_storesString)){
                _storesString =wc.DownloadString(storesURL);
            }
            StoreRootObject stores = JsonConvert.DeserializeObject<StoreRootObject>(_storesString);
            var storeDictionary = stores.stores.ToDictionary(m => m.storeNumber);
            

            //If I've passing the json string in use that otherwise use the URL
            if (string.IsNullOrEmpty(_availabilityString))
                _availabilityString = wc.DownloadString(availabilityURL);              

            _availabilityString = _availabilityString.Replace('/', 'Z');
            _availabilityString = "{\"availability\" : [" + _availabilityString + "]}";
            var test = JsonConvert.DeserializeXNode(_availabilityString);
            var searchString = string.Format("<{0}>{1}</{0}>", modelNumber, Available.ToString().ToLower());
            var children = test.DescendantNodes()
                .Where(m => m.ToString().Contains(searchString));
            //var children = test.DescendantNodes().Where(m => m.ToString().Contains("<MG4F2BZA>" + Available.ToString().ToLower() + "</MG4F2BZA>"));
            var childExEl = children.Cast<XElement>();
            var storeNumbers = childExEl.Select(m => m.Name);

            List<string> storesInStock = new List<string>();
            foreach (var s in storeNumbers)
            {
               
                if (storeDictionary.ContainsKey(s.LocalName))
                storesInStock.Add(storeDictionary[s.LocalName].storeName);
            }

            return storesInStock;

            //if (children.Count() > 0)
            //    return true;
            //return false;
            //"MGAH2B/A"

            //throw new NotImplementedException();

        }


        private Dictionary<string, string> ModelNumbers
        {
            get
            {
                var mnDictionary = new Dictionary<string, string>();
                mnDictionary.Add("16s", "MG482BZA");
                mnDictionary.Add("64s", "MG4H2BZA");
                mnDictionary.Add("128s", "MG4C2BZA");
                mnDictionary.Add("16g", "MG492BZA");
                mnDictionary.Add("64g", "MG4J2BZA");
                mnDictionary.Add("128g", "MG4E2BZA");
                mnDictionary.Add("16sg", "MG472BZA");
                mnDictionary.Add("64sg", "MG4F2BZA");
                mnDictionary.Add("128sg", "MG4A2BZA");

                return mnDictionary;
            }
        }

        private string GetModelNumber(string ModelCode)
        {
            if (this.ModelNumbers.ContainsKey(ModelCode))
                return this.ModelNumbers[ModelCode];
            else
                return null;
        }


        /*
         * 16s = MG482BZA,
            64s = MG4H2BZA
            128s = MG4C2BZA
            16g = MG492BZA
            64g = MG4J2BZA
            128g = MG4E2BZA
            16sg = MG472BZA
            64sg = MG4F2BZA
            128sg = MG4A2BZA
         */



    }
}