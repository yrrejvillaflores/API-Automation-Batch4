using HttpClient_Project.DataModel;

namespace HttpClient_Project.Test.TestData
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
