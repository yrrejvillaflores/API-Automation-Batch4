using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using HttpClient_Project.Helper;
using HttpClient_Project.DataModel;
using HttpClient_Project.Resource;
using HttpClient_Project.Test.TestData;
using System.Net;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace HttpClient_Project
{
    [TestClass]
    public class HttpClientProject
    {

        public BookingHelper bookingHelper;

        private readonly List<BookingListModel> cleanUpList = new List<BookingListModel>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            bookingHelper = new BookingHelper();
        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var httpDeleteResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Delete, $"{Endpoints.BookingEndpoint}/{data.Bookingid}");
            }
        }

        [TestMethod]
        public async Task CreateBooking()
        {
            // Create Data
            var createBookingData = GenerateBookingDetails.bookingDetails();

            // Post Request and Deserialize Content
            var httpPostResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Post, $"{Endpoints.BookingEndpoint}", createBookingData);
            var postResponseData = JsonConvert.DeserializeObject<BookingListModel>(httpPostResponse.Content.ReadAsStringAsync().Result);

            // Get Request and Deserialize Content
            var httpGetResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Get, $"{Endpoints.BookingEndpoint}/{postResponseData.Bookingid}");
            var getResponseData = JsonConvert.DeserializeObject<BookingModel>(httpGetResponse.Content.ReadAsStringAsync().Result);

            // Add Data to Cleanup List
            cleanUpList.Add(postResponseData);

            //Assertion
            Assert.AreEqual(HttpStatusCode.OK, httpPostResponse.StatusCode, "Status Code not equal to 200");
            Assert.AreEqual(createBookingData.Firstname, getResponseData.Firstname, "Firstname not matching");
            Assert.AreEqual(createBookingData.Lastname, getResponseData.Lastname, "Lastname not matching");
            Assert.AreEqual(createBookingData.Totalprice, getResponseData.Totalprice, "Totalprice not matching");
            Assert.AreEqual(createBookingData.Depositpaid, getResponseData.Depositpaid, "Depositpaid not matching");
            Assert.AreEqual(createBookingData.Bookingdates.Checkin, getResponseData.Bookingdates.Checkin, "Checkin Data not matching");
            Assert.AreEqual(createBookingData.Bookingdates.Checkout, getResponseData.Bookingdates.Checkout, "Checkout Data not matching");
            Assert.AreEqual(createBookingData.Additionalneeds, getResponseData.Additionalneeds, "Additional Needs not matching");

        }

        [TestMethod]
        public async Task UpdateBooking()
        {
            // Create Data
            var createBookingData = GenerateBookingDetails.bookingDetails();

            // Post Request and Deserialize Content
            var httpPostResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Post, $"{Endpoints.BookingEndpoint}", createBookingData);
            var postResponseData = JsonConvert.DeserializeObject<BookingListModel>(httpPostResponse.Content.ReadAsStringAsync().Result);

            // Get Request and Deserialize Content
            var httpGetResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Get, $"{Endpoints.BookingEndpoint}/{postResponseData.Bookingid}");
            var getCreatedDataResponse = JsonConvert.DeserializeObject<BookingModel>(httpGetResponse.Content.ReadAsStringAsync().Result);

            var updateBookingData = new BookingModel()
            {
                Firstname = "testFname.put.updated",
                Lastname = "testLname.put.updated",
                Totalprice = getCreatedDataResponse.Totalprice,
                Depositpaid = getCreatedDataResponse.Depositpaid,
                Bookingdates = new Bookingdates()
                {
                    Checkin = getCreatedDataResponse.Bookingdates.Checkin,
                    Checkout = getCreatedDataResponse.Bookingdates.Checkout
                },
                Additionalneeds = getCreatedDataResponse.Additionalneeds
            };

            // Put Request
            var httpPutResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Put, $"{Endpoints.BookingEndpoint}/{postResponseData.Bookingid}", updateBookingData);
       
            // Get Request and Deserialize Content
            httpGetResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Get, $"{Endpoints.BookingEndpoint}/{postResponseData.Bookingid}");
            var getUpdatedDataResponse = JsonConvert.DeserializeObject<BookingModel>(httpGetResponse.Content.ReadAsStringAsync().Result);

            // Add Data to Cleanup List
            cleanUpList.Add(postResponseData);

            //Assertion
            Assert.AreEqual(HttpStatusCode.OK, httpPutResponse.StatusCode, "Status Code not equal to 200");
            Assert.AreEqual(updateBookingData.Firstname, getUpdatedDataResponse.Firstname, "Firstname not matching");
            Assert.AreEqual(updateBookingData.Lastname, getUpdatedDataResponse.Lastname, "Lastname not matching");
            Assert.AreEqual(updateBookingData.Totalprice, getUpdatedDataResponse.Totalprice, "Totalprice not matching");
            Assert.AreEqual(updateBookingData.Depositpaid, getUpdatedDataResponse.Depositpaid, "Depositpaid not matching");
            Assert.AreEqual(updateBookingData.Bookingdates.Checkin, getUpdatedDataResponse.Bookingdates.Checkin, "Checkin Data not matching");
            Assert.AreEqual(updateBookingData.Bookingdates.Checkout, getUpdatedDataResponse.Bookingdates.Checkout, "Checkout Data not matching");
            Assert.AreEqual(updateBookingData.Additionalneeds, getUpdatedDataResponse.Additionalneeds, "Additional Needs not matching");

        }

        [TestMethod]
        public async Task DeleteBooking()
        {
            // Create Data
            var createBookingData = GenerateBookingDetails.bookingDetails();

            // Post Request and Deserialize Content
            var httpPostResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Post, $"{Endpoints.BookingEndpoint}", createBookingData);
            var postResponseData = JsonConvert.DeserializeObject<BookingListModel>(httpPostResponse.Content.ReadAsStringAsync().Result);

            // Delete Request
            var httpDeleteResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Delete, $"{Endpoints.BookingEndpoint}/{postResponseData.Bookingid}");

            // Add Data to Cleanup List
            cleanUpList.Add(postResponseData);

            //Assertion
            Assert.AreEqual(HttpStatusCode.Created, httpDeleteResponse.StatusCode, "Status Code not equal to 200");

        }

        [TestMethod]

        public async Task InvalidBookingId()
        {
            // Get Request
            var httpInvalidBookingIdResponse = await bookingHelper.SendAsyncFunction(HttpMethod.Get, $"{Endpoints.BookingEndpoint}/3532453656546");

            // Assertion
            Assert.AreEqual(HttpStatusCode.NotFound, httpInvalidBookingIdResponse.StatusCode);

        }
    }

}