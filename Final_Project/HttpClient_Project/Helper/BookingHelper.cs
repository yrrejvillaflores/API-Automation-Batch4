using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using HttpClient_Project.DataModel;
using HttpClient_Project.Resource;
using HttpClient_Project.Test.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HttpClient_Project.Helper
{
    public class BookingHelper
    {

        private HttpClient httpClient;

        public async Task<HttpResponseMessage> SendAsyncFunction(HttpMethod method, string url, BookingModel bookingData = null)
        {
            httpClient = new HttpClient();

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage();

            var token = await GetToken();

            httpRequestMessage.Method = method;
            httpRequestMessage.Headers.Add("Accept", "application/json");
            httpRequestMessage.Headers.Add("Cookie", $"token={token}");
            httpRequestMessage.RequestUri = Endpoints.GetURI(url);

            if (bookingData != null)
            {
                //Serialized Content
                var request = JsonConvert.SerializeObject(bookingData);
                httpRequestMessage.Content = new StringContent(request, Encoding.UTF8, "application/json");
            }

            var httpResponse = await httpClient.SendAsync(httpRequestMessage);

            return httpResponse;

        }

        [TestMethod]
        public async Task<string> GetToken()
        {
            // Serialize Content
            var request = JsonConvert.SerializeObject(GenerateTokenDetails.loginData());
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            var tokenResponse = await httpClient.PostAsync(Endpoints.GetURL(Endpoints.LoginEndpoint), postRequest);

            // Deserialize Content
            var tokenResponseData = JsonConvert.DeserializeObject<TokenModel>(tokenResponse.Content.ReadAsStringAsync().Result);

            // Return token
            return tokenResponseData.Token;

        }
    }
}
