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
    [Route("api/glass")]
    [ApiController]
    public class GlassPackageController : ControllerBase
    {
        private readonly IGlassPackageLogic _logic;
        private readonly IMapper _mapper;

        public GlassPackageController(IGlassPackageLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }



        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var glassesFromRepo = await _logic.GetAll();
            if (glassesFromRepo == null)
                return NotFound();

            var glasses = _mapper.Map<ICollection<GlassPackageDto>>(glassesFromRepo);

            return Ok(glasses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var glassFromRepo = await _logic.GetById(id);
            if (glassFromRepo == null)
                return NotFound("Željeni paket stakla nije pronađen");

            var glass = _mapper.Map<GlassPackageDto>(glassFromRepo);
            return Ok(glass);
        }
    }
}
