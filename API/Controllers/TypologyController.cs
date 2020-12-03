using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObject.TypologyDtos;
using Helpers;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/typology")]
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
        public async Task<IActionResult> Get()
        {
            var typologies = await _logic.GetAll();
            if (typologies == null)
                return NotFound("Tipologije nisu pronađene");

            var typologiesToReturn = _mapper.Map<ICollection<TypologyDto>>(typologies);

            return Ok(typologiesToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var typologyFromRepo = await _logic.GetById(id);

            if (typologyFromRepo == null)
                return NotFound("Tipologija nije pronađena");

            var typology = _mapper.Map<TypologyDto>(typologyFromRepo);
            return Ok(typology);
        }

    }
}
