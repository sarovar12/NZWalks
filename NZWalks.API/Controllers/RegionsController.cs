using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/regions 
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase

    {
        private readonly NZWalksDbContext dbContext;

        public RegionsController(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET ALL REGIONS
        //GET: https://localhost:portnumber/api/regions 

        [HttpGet]    
        public IActionResult GetAll()
        {
            var regions = dbContext.Regions.ToList();
           return Ok(regions);
        }

        //Get Single Region (Get region by id)
        //GET:https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")] //make sure this matches the argument in GetByID()
        public IActionResult GetByID([FromRoute]Guid id) {

           // var region = dbContext.Regions.Find(id); //find method only uses primary key
            
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(region == null)
            {
                return NotFound();

            }
            return Ok(region);
        }
    }
}
