using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

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
            //Get data from database - Domain Models
            var regionsDomain = dbContext.Regions.ToList();

            //Map domain models to DTOs
            var regionsDTO = new List<RegionDTO>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDTO.Add(new RegionDTO()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageURL = regionDomain.RegionImageURL,
                });
            }
    

            //return DTOs
           return Ok(regionsDTO);
        }

        //Get Single Region (Get region by id)
        //GET:https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")] //make sure this matches the argument in GetByID()
        public IActionResult GetByID([FromRoute]Guid id) {

           // var region = dbContext.Regions.Find(id); //find method only uses primary key
            
            //Get region domain model from DB
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomain == null)
            {
                return NotFound();

            }

            //Map/Convert Region Domain models to Region DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageURL = regionDomain.RegionImageURL
            };
            
            return Ok(regionDTO);
        }


        //POST To Create a new region
        //POST: https://localhost:portnumber/api/regions 
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageURL = addRegionRequestDTO.RegionImageURL
            };


            //Use Domain Model to create region

            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            // Map Domain model back to DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };


           //This is just here to display new thing getting created at json
            return CreatedAtAction(nameof(GetByID), new { id = regionDomainModel.Id }
                , regionDTO);
        }


        //Update Region
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO )
        {
            //check if it exists or not
           var regionDomainModel = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null) {
                return NotFound(); 
            }
            //Map DTO to Domain Model
            regionDomainModel.Code = updateRegionRequestDTO.Code;
            regionDomainModel.Name = updateRegionRequestDTO.Name;
            regionDomainModel.RegionImageURL = updateRegionRequestDTO.RegionImageURL;

            dbContext.SaveChanges();

            //Convert Domain Model to DTO
            var regionDTO = new RegionDTO
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageURL = regionDomainModel.RegionImageURL
            };
            return Ok(regionDTO);

        }

    }
}
