using HttpClient_Project.Resource;


namespace HttpClient_Project.Resource
{
    public class Endpoints
    {
        public static readonly string BaseURL = "https://restful-booker.herokuapp.com/";

        public static readonly string BookingEndpoint = "booking";

        public static readonly string LoginEndpoint = "auth";

        public static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";

        public static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));
    }
}
