using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Dtos.Category
{
    //This is the DTO that we will use to return the data from the API.
    public class CategoryResultDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
