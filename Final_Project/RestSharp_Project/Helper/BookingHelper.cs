using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RestSharp_Project.Test.TestData;
using RestSharp_Project.DataModel;
using RestSharp_Project.Resource;
using RestSharp_Project.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using Newtonsoft.Json.Linq;

namespace RestSharp_Project.Helper
{
    public class BookingHelper
    {
        private static async Task<string> GetToken(RestClient restClient)
        {

            // Serialize Content
            var postRestRequest = new RestRequest(Endpoints.LoginEndpoint()).AddJsonBody(GenerateTokenDetails.loginData());

            // Send Post Request
            var tokenRestResponse = await restClient.ExecutePostAsync<TokenModel>(postRestRequest);

            // Return token
            return tokenRestResponse.Data.Token;

        }
        public static async Task<BookingListModel> CreateBooking(RestClient restClient)
        {
            var token = await GetToken(restClient);
            restClient.AddDefaultHeader("Accept", "application/json");
            restClient.AddDefaultHeader("Cookie", $"token={token}");

            // Create Booking Data
            var createBookingData = GenerateBookingDetails.bookingDetails();

            //Send Post Request 
            var postRestRequest = new RestRequest(Endpoints.PostBooking()).AddJsonBody(createBookingData);
            var postRestResponse = await restClient.ExecutePostAsync<BookingListModel>(postRestRequest);

            return postRestResponse.Data;
        }

            public static async Task<RestResponse> UpdateBooking(RestClient restClient, BookingModel updateBookingData, long bookingId)
        {

            //Send Put Request
            var putRestRequest = new RestRequest(Endpoints.PutBookingById(bookingId)).AddJsonBody(updateBookingData); 
            var putRestResponse = await restClient.ExecutePutAsync<BookingModel>(putRestRequest);

            return putRestResponse;
        }
        public static async Task<RestResponse> DeleteBooking(RestClient restClient, long bookingId)
        {

            //Send Put Request
            var deleteRestRequest = new RestRequest(Endpoints.DeleteBookingById(bookingId));
            var deleteRestResponse = await restClient.DeleteAsync(deleteRestRequest);

            return deleteRestResponse;
        }

    }
}
