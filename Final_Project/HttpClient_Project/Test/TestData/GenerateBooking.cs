using HttpClient_Project.DataModel;

namespace HttpClient_Project.Test.TestData
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
                Additionalneeds = "None"
            };
        }
    }


}