using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using RestSharp_Project.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp_Project.Test
{
    public class ApiBaseTest
    {
        public RestClient restClient { get; set; }

        public BookingListModel bookingHelper { get; set; }

        [TestInitialize]
        public void Initilize()
        {
            restClient = new RestClient();
        }
    }
}
