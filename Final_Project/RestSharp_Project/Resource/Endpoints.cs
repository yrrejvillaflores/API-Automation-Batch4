using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_Project.Resource
{
    public class Endpoints
    {
        public const string BaseURL = "https://restful-booker.herokuapp.com";

        public const string BookingEndpoint = "booking";

        public const string AuthEndpoint = "auth";

        public static string LoginEndpoint() => $"{BaseURL}/{AuthEndpoint}";
        public static string GetBookingById(long id) => $"{BaseURL}/{BookingEndpoint}/{id}";
        public static string PostBooking() => $"{BaseURL}/{BookingEndpoint}";
        public static string PutBookingById(long id) => $"{BaseURL}/{BookingEndpoint}/{id}";
        public static string DeleteBookingById(long id) => $"{BaseURL}/{BookingEndpoint}/{id}";







    }
}
