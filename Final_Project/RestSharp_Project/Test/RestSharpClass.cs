using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using RestSharp_Project.DataModel;
using RestSharp_Project.Test.TestData;
using RestSharp_Project.Helper;
using RestSharp_Project.Test;
using RestSharp_Project.Resource;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace RestSharp_Project
{
    [TestClass]
    public class RestSharpClass : ApiBaseTest
    {
        private readonly List<BookingListModel> cleanUpList = new List<BookingListModel>();


        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanUpList)
            {
                var deleteRestResponse = await BookingHelper.DeleteBooking(restClient, bookingHelper.Bookingid);
            }
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            bookingHelper = await BookingHelper.CreateBooking(restClient);
        }

        [TestMethod]
        public async Task CreateBooking()
        {
            // Get Create Booking Data by ID
            var getRestRequest = new RestRequest(Endpoints.GetBookingById(bookingHelper.Bookingid));
            var getRestResponse = await restClient.ExecuteGetAsync<BookingModel>(getRestRequest);

            //Cleanup
            cleanUpList.Add(bookingHelper);

            //Assertion
            Assert.AreEqual(HttpStatusCode.OK, getRestResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(bookingHelper.Booking.Firstname, getRestResponse.Data.Firstname, "Firstname not matching");
            Assert.AreEqual(bookingHelper.Booking.Lastname, getRestResponse.Data.Lastname, "Lastname not matching");
            Assert.AreEqual(bookingHelper.Booking.Totalprice, getRestResponse.Data.Totalprice, "Totalprice not matching");
            Assert.AreEqual(bookingHelper.Booking.Depositpaid, getRestResponse.Data.Depositpaid, "Depositpaid not matching");
            Assert.AreEqual(bookingHelper.Booking.Bookingdates.Checkin, getRestResponse.Data.Bookingdates.Checkin, "Checkin Data not matching");
            Assert.AreEqual(bookingHelper.Booking.Bookingdates.Checkout, getRestResponse.Data.Bookingdates.Checkout, "Checkout Data not matching");
            Assert.AreEqual(bookingHelper.Booking.Additionalneeds, getRestResponse.Data.Additionalneeds, "Additional Needs not matching");

        }

        [TestMethod]
        public async Task UpdateBooking()
        {
            // Get Created Booking Data by ID
            var getRestRequest = new RestRequest(Endpoints.GetBookingById(bookingHelper.Bookingid));
            var getRestResponseCreatedBooking = await restClient.ExecuteGetAsync<BookingModel>(getRestRequest);

            //Cleanup
            cleanUpList.Add(bookingHelper);

            // Update Booking Data
            var updateBookingData = new BookingModel()
            {
                Firstname = "testFname.put.updated",
                Lastname = "testLname.put.updated",
                Totalprice = getRestResponseCreatedBooking.Data.Totalprice,
                Depositpaid = getRestResponseCreatedBooking.Data.Depositpaid,
                Bookingdates = new Bookingdates()
                {
                    Checkin = getRestResponseCreatedBooking.Data.Bookingdates.Checkin,
                    Checkout = getRestResponseCreatedBooking.Data.Bookingdates.Checkout
                },
                Additionalneeds = getRestResponseCreatedBooking.Data.Additionalneeds
            };

            // Get Put Response
            var putRestResponse = await BookingHelper.UpdateBooking(restClient, updateBookingData, bookingHelper.Bookingid);

            // Get Updated Booking Data by ID
            getRestRequest = new RestRequest(Endpoints.GetBookingById(bookingHelper.Bookingid));
            var getRestResponseUpdatedBooking = await restClient.ExecuteGetAsync<BookingModel>(getRestRequest);

            //Assertion
            Assert.AreEqual(HttpStatusCode.OK, putRestResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(updateBookingData.Firstname, getRestResponseUpdatedBooking.Data.Firstname, "Firstname not matching");
            Assert.AreEqual(updateBookingData.Lastname, getRestResponseUpdatedBooking.Data.Lastname, "Lastname not matching");
            Assert.AreEqual(updateBookingData.Totalprice, getRestResponseUpdatedBooking.Data.Totalprice, "Totalprice not matching");
            Assert.AreEqual(updateBookingData.Depositpaid, getRestResponseUpdatedBooking.Data.Depositpaid, "Depositpaid not matching");
            Assert.AreEqual(updateBookingData.Bookingdates.Checkin, getRestResponseUpdatedBooking.Data.Bookingdates.Checkin, "Checkin Data not matching");
            Assert.AreEqual(updateBookingData.Bookingdates.Checkout, getRestResponseUpdatedBooking.Data.Bookingdates.Checkout, "Checkout Data not matching");
            Assert.AreEqual(updateBookingData.Additionalneeds, getRestResponseUpdatedBooking.Data.Additionalneeds, "Additional Needs not matching");

        }

        [TestMethod]
        public async Task DeleteBooking()
        {    
            // Get Delete Response
            var deleteRestResponse = await BookingHelper.DeleteBooking(restClient, bookingHelper.Bookingid);

            //Assertion
            Assert.AreEqual(HttpStatusCode.Created, deleteRestResponse.StatusCode, "Status code is not equal to 200");

        }

        [TestMethod]
        public async Task InvalidBooking()
        {
            var getRestRequest = new RestRequest(Endpoints.GetBookingById(122124325));
            var getRestResponseInvalidBookingId = await restClient.ExecuteGetAsync<BookingModel>(getRestRequest);

            Assert.AreEqual(HttpStatusCode.NotFound, getRestResponseInvalidBookingId.StatusCode, "Status code is not equal to 200");

        }
    }
}

