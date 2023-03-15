using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using Session3.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session3.Tests
{
    public class ApiBaseTest
    {
        public RestClient restClient { get; set; }

        public PetJsonModel petDetails { get; set; }

        [TestInitialize]
        public void Initilize()
        {
            restClient = new RestClient();
        }
    }
}
