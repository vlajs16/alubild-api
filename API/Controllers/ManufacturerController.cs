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
    [Route("api/manufacturer")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerLogic _logic;
        private readonly IMapper _mapper;

        public ManufacturerController(IManufacturerLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var manufacturersFromRepo = await _logic.GetAll();
            if (manufacturersFromRepo == null)
                return NotFound();

            var manufacturers = _mapper.Map<ICollection<ManufacturerDto>>(manufacturersFromRepo);

            return Ok(manufacturers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var manufacturerFromRepo = await _logic.GetById(id);
            if (manufacturerFromRepo == null)
                return NotFound("Željeni proizvođač nije pronađen");

            var manufacturer = _mapper.Map<ManufacturerDto>(manufacturerFromRepo);
            return Ok(manufacturer);
        }

        [HttpGet("category/{categoryId}")]
        public async Task<IActionResult> GetByCategory(int categoryId)
        {
            var manufacturersFromRepo = await _logic.GetByCategory(categoryId);
            if (manufacturersFromRepo == null)
                return NotFound("Nisu pronađeni proizvođači za ovaj tip");

            var manufacturers = _mapper.Map<ICollection<ManufacturerDto>>(manufacturersFromRepo);
         
            return Ok(manufacturers);
        }



    }
}
