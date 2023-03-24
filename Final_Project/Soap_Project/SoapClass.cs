using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceReference1;

namespace Soap_Project
{
    [TestClass]
    public class SoapClass
    {
        //Global Variable
        private readonly CountryInfoServiceSoapTypeClient countryTest =
             new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        private List<tCountryCodeAndName> CountryListbyCode()
        {
            var countryListByCode = countryTest.ListOfCountryNamesByCode();

            return countryListByCode;

        }

        private tCountryCodeAndName RandomRecordFromList(List<tCountryCodeAndName> countryListByCode)
        {
            Random random = new Random();

            var getRandomList = random.Next(countryListByCode.Count);

            var randomCountryListByCode = countryListByCode[getRandomList]; 

            return randomCountryListByCode;
        }


        [TestMethod]
        public void FullCountryInfo()
        {
            var countryList = CountryListbyCode();
            var randomCountryList = RandomRecordFromList(countryList);

            var fullCountryInfo = countryTest.FullCountryInfo(randomCountryList.sISOCode);

            Assert.AreEqual(fullCountryInfo.sISOCode, randomCountryList.sISOCode, "Country Code not match");
            Assert.AreEqual(fullCountryInfo.sName, randomCountryList.sName, "Country Code not match");

        }

        [TestMethod]
        public void FiveCountryRecords()
        {
            var countryList = CountryListbyCode();
            List<tCountryCodeAndName> randomCountryList = new List<tCountryCodeAndName>();

            for (int country = 0; country < 5; country++)
            {
                randomCountryList.Add(RandomRecordFromList(countryList));              
            }

            foreach (var fiveCountries in randomCountryList)
            {
                var countryCode = countryTest.CountryISOCode(fiveCountries.sName);
                Assert.AreEqual(fiveCountries.sISOCode, countryCode, "Country code doesn't match.");
            }

        }


    }
}