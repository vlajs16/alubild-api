using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObject.SimpleDto;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/series")]
    [ApiController]
    public class SeriesController : ControllerBase
    {
        private readonly ISeriesLogic _logic;
        private readonly IMapper _mapper;

        public SeriesController(ISeriesLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var seriesFromRepo = await _logic.GetAll();
            if (seriesFromRepo == null)
                return NotFound();

            var series = _mapper.Map<ICollection<SeriesDto>>(seriesFromRepo);

            return Ok(series);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var seriesFromRepo = await _logic.GetById(id);
            if (seriesFromRepo == null)
                return NotFound("Željena serija nije pronađena");

            var series = _mapper.Map<SeriesDto>(seriesFromRepo);
            return Ok(series);
        }

        [HttpGet("manufacturer/{manId}")]
        public async Task<IActionResult> GetByManufacturer(int manId)
        {
            var seriesFromRepo = await _logic.GetByManufacturer(manId);
            if (seriesFromRepo == null)
                return NotFound("Nisu pronađene serije za ovog proizvođača");

            var series = _mapper.Map<ICollection<SeriesDto>>(seriesFromRepo);

            return Ok(series);
        }
    }
}
