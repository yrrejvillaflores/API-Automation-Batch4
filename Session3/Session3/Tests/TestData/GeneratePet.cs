using Session3.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session3.Tests.TestData
{
    public class GeneratePet
    {
     public static PetJsonModel newPet()
        {
            return new PetJsonModel
            {
     
                Id = 1,
                Category = new Category()
                {
                    Name = "Japanese Belgian"
                },
                Name = "Pink",
                PhotoUrls = new string[]
                {
                    "photoUrls"
                },
                Tags = new Category[]
                {
                    new Category()
                    {
                        Name = "Dog Tag"
                    }
                },
                Status = "status"
            };

        }
    }
}
