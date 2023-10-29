using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Repository.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {

        private readonly AppDbContext _db;
        private readonly IRegionRepository _regionRepository;
        private IMapper mapper;
        public RegionsController(AppDbContext db, IRegionRepository regionRepository, IMapper mapper)
        {
            _db = db;
            _regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain models
            List<Region> regionsDomain = await _regionRepository.GetAllAsync();
            //Map Domain Models to Dtos
            //Return DTOs 
            // return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
            return Ok(regionsDomain);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Region region = _db.Regions.Find(id);
            // Get Region Domain Model From DataBase
            Region regionDomain = await _regionRepository.GetByIdAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map Domain Models to Dtos
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        //Post to Created New Region 
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
            if (ModelState.IsValid)
            {
                //Map or Converter DTO to Region Model
                Region region = mapper.Map<Region>(createRegionDto);

                //Use Domain Model to create Region 
                region = await _regionRepository.CreateAsync(region);

                //Map Domain model back to DTO
                RegionDto regionDto = mapper.Map<RegionDto>(region);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //Update region 
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            //Check if region exists
            Region existingRegion = await _regionRepository.GetByIdAsync(id);
            if (existingRegion == null)
            {
                return NotFound();
            }
            existingRegion = await _regionRepository.UpdateAsync(updateRegionDto, existingRegion);

            //Convert Domain Model to DTO
            RegionDto regionDto = mapper.Map<RegionDto>(existingRegion);
            return Ok(regionDto);
        }


        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Region regionDomainModel = await _regionRepository.GetByIdAsync(id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }
            //Delete region 
            _regionRepository.DeleteAsync(regionDomainModel);
            return Ok();
        }












    }
}
