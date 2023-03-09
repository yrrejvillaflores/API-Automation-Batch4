using Newtonsoft.Json;
using System.Net;
using System.Text;


namespace Session2._1
{
    [TestClass]
    public class HttpClientTest
    {

        private static HttpClient httpClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        private static readonly string PetEndpoint = "pet";

        private static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<PetModel> cleanUpList = new List<PetModel>();


        [TestInitialize]
        public void TestInitialize()
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var httpResponse = await httpClient.DeleteAsync(GetURI($"{PetEndpoint}/{data.Id}"));
            }
        }
        [TestMethod]
        public async Task PutMethod()
        {
            #region create data 

            // Create JSON Object
            PetModel petData = new PetModel()
            {
                Id = 1,
                Category = new Category { Name = "Japanese Belgian" },
                Name = "Pink",
                PhotoUrls = new string[] {"photoUrls"},
                Tags = new Category[] {new Category { Name = "Dog Tag" } },
                Status = "status"
            };

            // Serialize Content 
            var request = JsonConvert.SerializeObject(petData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            await httpClient.PostAsync(GetURL(PetEndpoint), postRequest);

            #endregion

            #region get PetName of the created data
            // Get Request
            var getResponse = await httpClient.GetAsync(GetURI($"{PetEndpoint}/{petData.Id}"));

            // Desserialize Content
            var listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            // Filter Created Data
            var createdPetData = listPetData.Name;

            #endregion

            #region send put request to update data

            // Update Value of petData
            petData = new PetModel()
            {
                Id = listPetData.Id,
                Category = listPetData.Category,
                Name = "Cloud",
                PhotoUrls = listPetData.PhotoUrls,
                Tags = listPetData.Tags,
                Status = listPetData.Status

            };


            // Serialize Content 
            request = JsonConvert.SerializeObject(petData);
            postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Put Request
            var httpResponse = await httpClient.PutAsync(GetURL(PetEndpoint), postRequest);

            // Status Code
            var statusCode = httpResponse.StatusCode;

            #endregion

            #region get updated data

            getResponse = await httpClient.GetAsync(GetURI($"{PetEndpoint}/{petData.Id}"));

            // Desserialize Content
            listPetData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            // Filter Created Pet Data
            var updatedPetData = listPetData.Name;

            #endregion

            #region cleanup data

            // Add Data to Cleanup List
            cleanUpList.Add(listPetData);

            #endregion

            #region assertion

            //Assertion
            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status Code not equal to 200");
            Assert.AreEqual(petData.Name, updatedPetData, "Pet Name not matching");

            #endregion
        }
    }
}