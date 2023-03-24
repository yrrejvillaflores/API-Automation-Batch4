using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp_Project.DataModel;

namespace RestSharp_Project.Test.TestData
{
    public class GenerateBookingDetails
    {
        public static BookingModel bookingDetails()
        {

            return new BookingModel
            {
                Firstname = "testFname.post",
                Lastname = "testLname",
                Totalprice = 100,
                Depositpaid = true,
                Bookingdates = new Bookingdates()
                {
                    Checkin = DateTime.Parse("2023-03-23"),
                    Checkout = DateTime.Parse("2023-03-23")
                },
                Additionalneeds = "Lots of Money"
            };
        }
    }
}
