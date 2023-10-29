using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksController(IMapper mapper,IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;             
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalkDto createWalkDto)
        {
            //Map DTO to Domain Model
            Walk walkDomainModel=mapper.Map<Walk>(createWalkDto);

           await walkRepository.CreateAsync(walkDomainModel) ;

            //Map Domain model to DTO
            return Ok(mapper.Map<WalkDto>(walkDomainModel));
        }

        //Get Walks 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Walk> walks = await walkRepository.GetAllAsync();

            //Map Domain Model to DTO
           return Ok(mapper.Map<List<WalkDto>>(walks));         
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            Walk walk = await walkRepository.GetByIdAsync(id);
            if (walk == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDto>(walk));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,UpdateWalkDto updateWalkDto)
        {
            //Map DTO to Domain Model
            Walk walkDomain= mapper.Map<Walk>(updateWalkDto);

            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            if(walkDomain == null)
            {
                return NotFound();
            }
            //Map Domain Model to Dto
            return Ok(mapper.Map<WalkDto>(walkDomain));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            Walk deleteWalkDomainModel= await walkRepository.DeleteAsync(id);
            if(deleteWalkDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<WalkDto>(deleteWalkDomainModel));
        }









    }
}
