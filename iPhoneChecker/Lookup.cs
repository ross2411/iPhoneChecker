using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace iPhoneChecker
{
    public enum ModelCode
    {
        iPhone6Grey16GB,
        iPhone6Grey64GB,
        iPhone6Grey128GB,
        iPhone6Gold16GB,
        iPhone6Gold64GB,
        iPhone6Gold128GB,
        iPhone6Silver16GB,
        iPhone6Silver64GB,
        iPhone6Silver128GB,
    }

    public class Lookup
    {
        private string _availabilityString { get; set; }
        private string _storesString { get; set; }
        private Uri _storesLookupURL { get; set; }
        private Uri _availabilityLookupURL { get; set; }

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

        public Lookup(Uri StoresLookupURL, Uri AvailabilityLookupURL)
        {
            _storesLookupURL = StoresLookupURL;
            _availabilityLookupURL = AvailabilityLookupURL;
        }

        private IDictionary<string, Store> getStoresDictionary()
        {
            WebClient wc = new WebClient();

            if (string.IsNullOrEmpty(_storesString))
            {
                _storesString = wc.DownloadString(_storesLookupURL);
            }

            var stores = JsonConvert.DeserializeObject<dynamic>(_storesString);
            if (((JObject)stores).Count == 0)
                throw new InvalidOperationException("Unable to query stores Lookup URL");
            else
                return (((Stores)stores).stores.ToDictionary(m => m.storeNumber));
            
        }

        private dynamic getAvailability()
        {
            WebClient wc = new WebClient();
            //If I've passing the json string in use that otherwise use the URL
            if (string.IsNullOrEmpty(_availabilityString))
                _availabilityString = wc.DownloadString(_availabilityLookupURL);


            var returnedString = JsonConvert.DeserializeObject<dynamic>(_availabilityString);
            if (((JObject)returnedString).Count == 0)
                throw new InvalidOperationException("Unable to query Availability URL");
            else
                return returnedString;

        }


        /// <summary>
        /// Find out if a particular model of iPhone 6 is in stock anywhere and returns the list of stores it's available in
        /// </summary>
        /// <param name="ModelCode">Model Code</param>
        /// <returns>List of stores where iPhone is currently available</returns>
        public List<Store> PhoneAvailable(ModelCode ModelCode) {


            var storeDictionary = getStoresDictionary();
            var availabilityObject = getAvailability();

            foreach (var x in availabilityObject)
            {
                string storeNumber = x.Name;
                if (!string.Equals(storeNumber, "updated" )){
                JToken value = x.Value;

                storeDictionary[storeNumber].iPhone6_16_Silver = bool.Parse(value["MG482B/A"].ToString());
                storeDictionary[storeNumber].iPhone6_64_Silver = bool.Parse(value["MG4H2B/A"].ToString());
                storeDictionary[storeNumber].iPhone6_128_Silver = bool.Parse(value["MG4C2B/A"].ToString());
                storeDictionary[storeNumber].iPhone6_16_Grey = bool.Parse(value["MG472B/A"].ToString());
                storeDictionary[storeNumber].iPhone6_64_Grey = bool.Parse(value["MG4F2B/A"].ToString());
                storeDictionary[storeNumber].iPhone6_128_Grey = bool.Parse(value["MG4A2B/A"].ToString());
                storeDictionary[storeNumber].iPhone6_16_Gold = bool.Parse(value["MG492B/A"].ToString());
                storeDictionary[storeNumber].iPhone6_64_Gold = bool.Parse(value["MG4J2B/A"].ToString());
                storeDictionary[storeNumber].iPhone6_128_Gold = bool.Parse(value["MG4E2B/A"].ToString());
                }
            }

            switch(ModelCode){
                case iPhoneChecker.ModelCode.iPhone6Silver16GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_16_Silver.HasValue && m.Value.iPhone6_16_Silver.Value)
                        .Select(m=>m.Value)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Silver64GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_64_Silver.HasValue && m.Value.iPhone6_64_Silver.Value)
                        .Select(m => m.Value)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Silver128GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_128_Silver.HasValue && m.Value.iPhone6_128_Silver.Value)
                        .Select(m => m.Value)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Grey16GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_16_Grey.HasValue && m.Value.iPhone6_16_Grey.Value)
                        .Select(m => m.Value)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Grey64GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_64_Grey.HasValue && m.Value.iPhone6_64_Grey.Value)
                        .Select(m => m.Value)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Grey128GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_128_Grey.HasValue && m.Value.iPhone6_128_Grey.Value)
                        .Select(m => m.Value)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Gold16GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_16_Gold.HasValue && m.Value.iPhone6_16_Gold.Value)
                        .Select(m => m.Value)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Gold64GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_64_Gold.HasValue && m.Value.iPhone6_64_Gold.Value)
                        .Select(m => m.Value)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Gold128GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_128_Gold.HasValue && m.Value.iPhone6_128_Gold.Value)
                        .Select(m => m.Value)
                        .ToList();
            }

            throw new InvalidOperationException("Model number couldn't be found");
        }
    }
}