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
    [Route("api/quality")]
    [ApiController]
    public class QualityController : ControllerBase
    {
        private readonly IQualityLogic _logic;
        private readonly IMapper _mapper;

        public QualityController(IQualityLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var qualitiesFromRepo = await _logic.GetAll();
            if (qualitiesFromRepo == null)
                return NotFound();

            var qualities = _mapper.Map<ICollection<QualityDto>>(qualitiesFromRepo);

            return Ok(qualities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var qualityFromRepo = await _logic.GetById(id);
            if (qualityFromRepo == null)
                return NotFound("Željeni kvalitet nije pronađen");

            var quality = _mapper.Map<QualityDto>(qualityFromRepo);
            return Ok(quality);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var qualitiesFromRepo = await _logic.GetByCategory(categoryId);
            if (qualitiesFromRepo == null)
                return NotFound("Nisu pronađeni kvaliteti za ovaj tip");

            var qualities = _mapper.Map<ICollection<QualityDto>>(qualitiesFromRepo);

            return Ok(qualities);
        }
    }
}
