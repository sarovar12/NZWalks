using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using System.Collections.Generic;

namespace NZWalks.API.Controllers
{
    // https://localhost:portnumber/api/regions 
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase

    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        //GET ALL REGIONS
        //GET: https://localhost:portnumber/api/regions 

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get data from database - Domain Models
            var regionsDomain = await regionRepository.GetAllASync();

            // Map region models into DTOs
           var regionsDTO= mapper.Map<List<RegionDTO>>(regionsDomain);


            //return DTOs
            return Ok(regionsDTO);
        }

        //Get Single Region (Get region by id)
        //GET:https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")] //make sure this matches the argument in GetByID()
        public async  Task<IActionResult> GetByID([FromRoute] Guid id) {

            // var region = dbContext.Regions.Find(id); //find method only uses primary key

            //Get region domain model from DB
            var regionDomain = await regionRepository.GetByIDASync(id);

            if (regionDomain == null)
            {
                return NotFound();

            }

            //Map/Convert Region Domain models to Region DTO
            

            return Ok(mapper.Map<RegionDTO>(regionDomain));
        }


        //POST To Create a new region
        //POST: https://localhost:portnumber/api/regions 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO)
        {

            //Map or Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO); 


            //Use Domain Model to create region
          regionDomainModel =  await regionRepository.CreateASync(regionDomainModel);

            // Map Domain model back to DTO
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);


            //This is just here to display new thing getting created at json
            return CreatedAtAction(nameof(GetByID), new { id = regionDomainModel.Id }
                , regionDTO);
        }


        //Update Region
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDTO updateRegionRequestDTO)
        {
            //Map DTO to domain model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDTO);


            regionDomainModel =  await regionRepository.UpdateASync(id, regionDomainModel);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Convert Domain Model to DTO

            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDTO);

        }

        //Delete Region 
        //DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var regionDomainModel= await regionRepository.DeleteASync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            

            return Ok(mapper.Map<RegionDTO>(regionDomainModel));

        }
    }
}

        

