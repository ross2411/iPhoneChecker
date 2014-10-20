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

        /// <summary>
        /// Find out if a particular model of iPhone 6 is in stock anywhere and returns the list of stores it's available in
        /// </summary>
        /// <param name="ModelCode">Model Code</param>
        /// <returns>List of stores where iPhone is currently available</returns>
        public List<string> PhoneAvailable(ModelCode ModelCode) {

            WebClient wc = new WebClient();

            //If I've passing the json string in use that otherwise use the URL
            if (string.IsNullOrEmpty(_storesString)){
                _storesString = wc.DownloadString(_storesLookupURL);
            }
            Stores stores = JsonConvert.DeserializeObject<Stores>(_storesString);
            var storeDictionary = stores.stores.ToDictionary(m => m.storeNumber);
            

            //If I've passing the json string in use that otherwise use the URL
            if (string.IsNullOrEmpty(_availabilityString))
                _availabilityString = wc.DownloadString(_availabilityLookupURL);

      
            var json = JsonConvert.DeserializeObject<dynamic>(_availabilityString);
            foreach (var x in json)
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
                        .Select(m=>m.Key)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Silver64GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_64_Silver.HasValue && m.Value.iPhone6_64_Silver.Value)
                        .Select(m => m.Key)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Silver128GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_128_Silver.HasValue && m.Value.iPhone6_128_Silver.Value)
                        .Select(m => m.Key)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Grey16GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_16_Grey.HasValue && m.Value.iPhone6_16_Grey.Value)
                        .Select(m => m.Key)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Grey64GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_64_Grey.HasValue && m.Value.iPhone6_64_Grey.Value)
                        .Select(m => m.Key)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Grey128GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_128_Grey.HasValue && m.Value.iPhone6_128_Grey.Value)
                        .Select(m => m.Key)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Gold16GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_16_Gold.HasValue && m.Value.iPhone6_16_Gold.Value)
                        .Select(m => m.Key)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Gold64GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_64_Gold.HasValue && m.Value.iPhone6_64_Gold.Value)
                        .Select(m => m.Key)
                        .ToList();

                case iPhoneChecker.ModelCode.iPhone6Gold128GB:
                    return storeDictionary
                        .Where(m => m.Value.iPhone6_128_Gold.HasValue && m.Value.iPhone6_128_Gold.Value)
                        .Select(m => m.Key)
                        .ToList();
            }

            throw new InvalidOperationException("Model number couldn't be found");

            //_availabilityString = _availabilityString.Replace('/', 'Z');
            //_availabilityString = "{\"availability\" : [" + _availabilityString + "]}";
            //var test = JsonConvert.DeserializeXNode(_availabilityString);
            //var searchString = string.Format("<{0}>{1}</{0}>", modelNumber, Available.ToString().ToLower());
            //var children = test.DescendantNodes()
            //    .Where(m => m.ToString().Contains(searchString));
            ////var children = test.DescendantNodes().Where(m => m.ToString().Contains("<MG4F2BZA>" + Available.ToString().ToLower() + "</MG4F2BZA>"));
            //var childExEl = children.Cast<XElement>();
            //var storeNumbers = childExEl.Select(m => m.Name);

            //List<string> storesInStock = new List<string>();
            //foreach (var s in storeNumbers)
            //{
               
            //    if (storeDictionary.ContainsKey(s.LocalName))
            //    storesInStock.Add(storeDictionary[s.LocalName].storeName);
            //}

            //return storesInStock;

            //if (children.Count() > 0)
            //    return true;
            //return false;
            //"MGAH2B/A"

            //throw new NotImplementedException();

        }


        //private Dictionary<string, string> ModelNumbers
        //{
        //    get
        //    {
        //        var mnDictionary = new Dictionary<string, string>();
        //        mnDictionary.Add("16s", "MG482BZA");
        //        mnDictionary.Add("64s", "MG4H2BZA");
        //        mnDictionary.Add("128s", "MG4C2BZA");
        //        mnDictionary.Add("16g", "MG492BZA");
        //        mnDictionary.Add("64g", "MG4J2BZA");
        //        mnDictionary.Add("128g", "MG4E2BZA");
        //        mnDictionary.Add("16sg", "MG472BZA");
        //        mnDictionary.Add("64sg", "MG4F2BZA");
        //        mnDictionary.Add("128sg", "MG4A2BZA");

        //        return mnDictionary;
        //    }
        //}

        //private string GetModelNumber(string ModelCode)
        //{
        //    if (this.ModelNumbers.ContainsKey(ModelCode))
        //        return this.ModelNumbers[ModelCode];
        //    else
        //        return null;
        //}


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