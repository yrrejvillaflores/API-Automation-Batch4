using RestSharp;
using Session3.DataModels;
using Session3.Resources;
using Session3.Tests.TestData;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net;

namespace Session3.Helpers
{

    public class PetHelper
    {

        public static async Task<PetJsonModel> AddNewPet(RestClient client)
        {
            var newPetData = GeneratePet.newPet();
            var postRequest = new RestRequest(Endpoints.PostPet());

            //Send Post Request to add new Pet
            postRequest.AddJsonBody(newPetData);
            var postResponse = await client.ExecutePostAsync<PetJsonModel>(postRequest);

            var createdPetData = newPetData;
            return createdPetData;
        }
    }
}
