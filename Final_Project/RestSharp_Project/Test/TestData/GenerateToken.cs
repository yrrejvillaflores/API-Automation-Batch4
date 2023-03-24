using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp_Project.DataModel;
using RestSharp_Project.Test.TestData;

namespace RestSharp_Project.Test.TestData
{
    public class GenerateTokenDetails
    {
        public static LoginModel loginData()
        {
            return new LoginModel
            {
                Username = "admin",
                Password = "password123"
            };
        }
    }
}
