using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObject.TypologyDto;
using Helpers;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TypologyController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ITypologyLogic _logic;

        public TypologyController(IMapper mapper, ITypologyLogic logic)
        {
            _mapper = mapper;
            _logic = logic;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TypologyParams typologyParams)
        {
            var typologies = await _logic.GetTypologies(typologyParams);
            if (typologies == null)
                return NotFound("Tipologije nisu pronađene");

            Response.AddPagination(typologies.CurrentPage, typologies.PageSize,
                typologies.TotalCount, typologies.TotalPages);

            var typologiesToReturn = _mapper.Map<ICollection<TypologyDto>>(typologies);

            return Ok(typologiesToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var typologyFromRepo = await _logic.GetTypology(id);

            if (typologyFromRepo == null)
                return NotFound("Tipologija nije pronađena");

            var typology = _mapper.Map<TypologyDto>(typologyFromRepo);
            return Ok(typology);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var namesFromRepo = await _logic.GetTypologyNames(id);

            if (namesFromRepo == null)
                return NotFound();

            return Ok(namesFromRepo);
            
        }
    }
}
