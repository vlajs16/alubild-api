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
    [Route("api/guide")]
    [ApiController]
    public class GuideController : ControllerBase
    {
        private readonly IGuideLogic _logic;
        private readonly IMapper _mapper;

        public GuideController(IGuideLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var guidesFromRepo = await _logic.GetAll();
            if (guidesFromRepo == null)
                return NotFound();

            var guides = _mapper.Map<ICollection<GuideDto>>(guidesFromRepo);

            return Ok(guides);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var guideFromRepo = await _logic.GetById(id);
            if (guideFromRepo == null)
                return NotFound("Željena vođica nije pronađena");

            var guide = _mapper.Map<GuideDto>(guideFromRepo);
            return Ok(guide);
        }
    }
}
