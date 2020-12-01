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
    [Route("api/tabakera")]
    [ApiController]
    public class TabakeraController : ControllerBase
    {
        private readonly ITabakeraLogic _logic;
        private readonly IMapper _mapper;

        public TabakeraController(ITabakeraLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var tabakereRepo = await _logic.GetAll();
            if (tabakereRepo == null)
                return NotFound();

            var tabakere = _mapper.Map<ICollection<TabakeraDto>>(tabakereRepo);

            return Ok(tabakere);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tabakeraRepo = await _logic.GetById(id);
            if (tabakeraRepo == null)
                return NotFound("Željena tabakera nije pronađena");

            var tabakera = _mapper.Map<TabakeraDto>(tabakeraRepo);
            return Ok(tabakera);
        }
    }
}
