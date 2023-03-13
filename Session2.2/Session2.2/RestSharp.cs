using Session2._2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace Session2._2
{
    [TestClass]
    public class RestSharp
    {
        private static RestClient restClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2";

        private static readonly string PetEndpoint = "/pet";

        private static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            restClient = new RestClient();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanUpList)
            {
                var deleteRestRequest = new RestRequest(GetURI($"{PetEndpoint}/{data.Id}"));
                var deleteRestResponse = await restClient.DeleteAsync(deleteRestRequest);
            }
        }

        [TestMethod]
        public async Task PostMethod()
        {
            #region CreatePet
            //Create Pet
            PetModel newPet = new PetModel()
            {
                Id = 1,
                Category = new Category()
                { 
                    Name = "Japanese Belgian" 
                },
                Name = "Pink",
                PhotoUrls = new string[] 
                { 
                    "photoUrls" 
                },
                Tags = new Category[] 
                { 
                    new Category()
                    { 
                        Name = "Dog Tag" 
                    } 
                },
                Status = "status"
            };


            // Send Post Request

            var postRestRequest = new RestRequest(GetURI(PetEndpoint)).AddJsonBody(newPet);
            var postRestResponse = await restClient.ExecutePostAsync<PetModel>(postRestRequest);



            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status code is not equal to 200");
            #endregion

            #region Assertions
            //Assertions
            Assert.AreEqual(newPet.Category.Name, postRestResponse.Data.Category.Name, "Category Name did not match.");
            Assert.AreEqual(newPet.Name, postRestResponse.Data.Name, "Name did not match.");
            Assert.AreEqual(newPet.PhotoUrls[0], postRestResponse.Data.PhotoUrls[0], "Photo URLs did not match.");
            Assert.AreEqual(newPet.Tags[0].Name, postRestResponse.Data.Tags[0].Name, "Email did not match.");
            Assert.AreEqual(newPet.Status, postRestResponse.Data.Status, "Status did not match");
            #endregion

            #region GetUser
            var getRestRequest = new RestRequest(GetURI($"{PetEndpoint}/{newPet.Id}"));
            var getRestResponse = await restClient.ExecuteGetAsync<PetModel>(getRestRequest);
            #endregion

            #region CleanUp
            // Add Data to Cleanup List
            cleanUpList.Add(newPet);
            #endregion

            #region Assertions
            //Assertions
            Assert.AreEqual(HttpStatusCode.OK, getRestResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(newPet.Category.Name, getRestResponse.Data.Category.Name, "Category Name did not match.");
            Assert.AreEqual(newPet.Name, getRestResponse.Data.Name, "Name did not match.");
            Assert.AreEqual(newPet.PhotoUrls[0], getRestResponse.Data.PhotoUrls[0], "Photo URLs did not match.");
            Assert.AreEqual(newPet.Tags[0].Name, getRestResponse.Data.Tags[0].Name, "Email did not match.");
            Assert.AreEqual(newPet.Status, getRestResponse.Data.Status, "Status did not match");
            #endregion

        }


    }
}
