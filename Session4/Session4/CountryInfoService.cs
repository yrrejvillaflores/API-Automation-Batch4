using ServiceReference1;
using System.Net.WebSockets;
using RestSharp;

namespace Session4
{
    [TestClass]
    public class CountryInfoService
    {
        //Global Variable
        private readonly CountryInfoServiceSoapTypeClient countryTest =
             new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);

        [TestMethod]
        public void AscendCountryNamesbyCode()
        {
            // Verify Ascending Order of Country Names by Code
            var listOfCountryNameByCode = countryTest.ListOfCountryNamesByCode();
            Assert.IsTrue(Enumerable.SequenceEqual(listOfCountryNameByCode, listOfCountryNameByCode.OrderBy(countryCode => countryCode.sISOCode)), "Country Code Not Ascending Order");

        }

        [TestMethod]
        public void PassCountryCode()
        {
            // Verify method returns a country after passing correct code
            var validCountryCode = countryTest.CountryName("HK");
            Assert.AreEqual(validCountryCode, "Hong Kong", "Country not found in the database");


            // Verify passing of incorrect code does not return a country
            var invalidCountryCode = countryTest.CountryName("codeRed");
            Assert.AreEqual("Country not found in the database", invalidCountryCode, "Country was found in the database");


        }

        [TestMethod]
        public void ValidateCountryName()
        {
            // Get Last Country Name
            var getLastCountryNamebyCode = countryTest.ListOfCountryNamesByCode()[countryTest.ListOfCountryNamesByCode().Count -1];

            // Convert Last Country Name to Code then pass it as a Parameter on CountryName()
            var getCountryName = countryTest.CountryName(getLastCountryNamebyCode.sISOCode);

            Assert.AreEqual(getLastCountryNamebyCode.sName, getCountryName, "Country Name not match");



        }



    }
}