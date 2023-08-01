using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/regions 
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        //GET ALL REGIONS
        //GET: https://localhost:portnumber/api/regions 

        [HttpGet]    
        public IActionResult GetAll()
        {
            var regions = new List<Region>
            {
                new Region
                {
                    Id = Guid.NewGuid(),
                    Name="Auckland Region",
                    Code="AKL",
                    RegionImageURL="https://images.pexels.com/photos/17319423/pexels-photo-17319423/free-photo-of-ornamented-walls-around-glass-ceiling-in-dome.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"
                },
                new Region {
                     Id = Guid.NewGuid(),
                    Name="Wellington Region",
                    Code="WLG",
                    RegionImageURL="https://images.pexels.com/photos/17319423/pexels-photo-17319423/free-photo-of-ornamented-walls-around-glass-ceiling-in-dome.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
                },
            };

            return Ok(regions);
        }
    }
}
