using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {


        private readonly AppDbContext _db;
        public RegionsController(AppDbContext db)
        {
            _db = db;
        }


        [HttpGet]
        public IActionResult GetAll()
        {
            //Get Data From Database - Domain models
            List<Region> regionsDomain = _db.Regions.ToList();

            //Map Domain Models to Dtos
            List<RegionDto> regionsDto = new List<RegionDto>();
            foreach (Region region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            //Return DTOs 
            return Ok(regionsDto);
        }


        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //Region region = _db.Regions.Find(id);
            // Get Region Domain Model From DataBase
            Region regionDomain = _db.Regions.FirstOrDefault(x => x.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            //Map Domain Models to Dtos
            RegionDto regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };
            return Ok(regionDto);
        }


        //Post to Created New Region 
        [HttpPost]
        public IActionResult Create([FromBody] CreateRegionDto createRegionDto) 
        {

            //Map or Converter DTO to Region Model
            Region region = new Region
            {
                Code = createRegionDto.Code,
                Name = createRegionDto.Name,
                RegionImageUrl = createRegionDto.RegionImageUrl
            };

            //Use Domain Model to create Region 
            _db.Regions.Add(region);
           _db.SaveChanges();

            //Map Domain model back to DTO
            RegionDto regionDto = new RegionDto
            {
                Id = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new {id=regionDto.Id},regionDto);
        }


        //Update region 
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto )
        {
            //Check if region exists
           Region regionDomainModel = _db.Regions.FirstOrDefault(x => x.Id == id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Map DTO to Domain model
            regionDomainModel.Code = updateRegionDto.Code;  
            regionDomainModel.Name= updateRegionDto.Name;
            regionDomainModel.RegionImageUrl= updateRegionDto.RegionImageUrl;

            _db.SaveChanges();

            //Convert Domain Model to DTO
            RegionDto regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }


        //Delete Region
        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            Region regionDomainModel=_db.Regions.FirstOrDefault(x=>x.Id == id);
            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region 
            _db.Regions.Remove(regionDomainModel);
            _db.SaveChanges();
            return Ok();
        }












    }
}
