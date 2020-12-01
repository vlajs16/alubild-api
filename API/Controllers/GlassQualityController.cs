using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObject.SimpleDto;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/glassquality")]
    [ApiController]
    public class GlassQualityController : ControllerBase
    {
        private readonly IGlassQualityLogic _logic;
        private readonly IMapper _mapper;

        public GlassQualityController(IGlassQualityLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var glassQualitiesFromRepo = await _logic.GetAll();
            if (glassQualitiesFromRepo == null)
                return NotFound();

            var qualities = _mapper.Map<ICollection<GlassQualityDto>>(glassQualitiesFromRepo);

            return Ok(qualities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var glassQualityFromRepo = await _logic.GetById(id);
            if (glassQualityFromRepo == null)
                return NotFound("Željeni kvalitet stakla nije pronađen");

            var quality = _mapper.Map<GlassQualityDto>(glassQualityFromRepo);
            return Ok(quality);
        }


    }
}
