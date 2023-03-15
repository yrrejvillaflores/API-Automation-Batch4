using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Net.Http;
using Session3.DataModels;
using Session3.Resources;
using Session3.Helpers;
using Session3.Tests.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

[assembly: Parallelize(Workers = 10, Scope = ExecutionScope.MethodLevel)]
namespace Session3.Tests
{
    [TestClass]
    public class Session3Tests : ApiBaseTest
    {
        private static List<PetJsonModel> petCleanUpList = new List<PetJsonModel>();

        [TestInitialize]
        public async Task TestInitialize()
        {
            petDetails = await PetHelper.AddNewPet(restClient);
        }

        [TestMethod]
        public async Task GetPet()
        {
            //Arrange
            var getPetRequest = new RestRequest(Endpoints.GetPetById(petDetails.Id));
            petCleanUpList.Add(petDetails);

            //Act
            var getPetResponse = await restClient.ExecuteGetAsync<PetJsonModel>(getPetRequest);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, getPetResponse.StatusCode, "Failed due to wrong status code.");
            Assert.AreEqual(petDetails.Category.Name, getPetResponse.Data.Category.Name);
            Assert.AreEqual(petDetails.Name, getPetResponse.Data.Name);
            Assert.AreEqual(petDetails.PhotoUrls[0], getPetResponse.Data.PhotoUrls[0]);
            Assert.AreEqual(petDetails.Tags[0].Name, getPetResponse.Data.Tags[0].Name);
            Assert.AreEqual(petDetails.Status, getPetResponse.Data.Status);
        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var data in petCleanUpList)
            {
                var deletePetRequest = new RestRequest(Endpoints.GetPetById(petDetails.Id));
                var deletePetResponse = await restClient.DeleteAsync(deletePetRequest);
            }
        }
    }
}
