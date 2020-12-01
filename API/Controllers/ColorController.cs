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
    [Route("api/color")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorLogic _logic;
        private readonly IMapper _mapper;

        public ColorController(IColorLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var colorsFromRepo = await _logic.GetAll();
            if (colorsFromRepo == null)
                return NotFound();

            var colors = _mapper.Map<ICollection<ColorDto>>(colorsFromRepo);

            return Ok(colors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var colorFromRepo = await _logic.GetById(id);
            if (colorFromRepo == null)
                return NotFound("Željena boja nije pronađena");

            var color = _mapper.Map<ColorDto>(colorFromRepo);
            return Ok(color);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var colorsFromRepo = await _logic.GetByCategory(categoryId);
            if (colorsFromRepo == null)
                return NotFound("Nisu pronađene boje za ovaj tip");

            var colors = _mapper.Map<ICollection<ColorDto>>(colorsFromRepo);

            return Ok(colors);
        }
    }
}
