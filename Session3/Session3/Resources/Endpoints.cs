using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session3.Resources
{
    public class Endpoints
    {
        //Base URL
        public const string baseURL = "https://petstore.swagger.io/v2";

        public static string GetPetById(long id) => $"{baseURL}/pet/{id}";

        public static string PostPet() => $"{baseURL}/pet";

        public static string DeletePetById(string id) => $"{baseURL}/pet/{id}";
    }

}
