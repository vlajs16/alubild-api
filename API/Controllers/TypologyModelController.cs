using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObject.TypologyModelDtos;
using Helpers;
using Logic.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/typologymodel")]
    [ApiController]
    public class TypologyModelController : ControllerBase
    {
        private readonly ITypologyModelLogic _logic;
        private readonly IMapper _mapper;

        public TypologyModelController(ITypologyModelLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] TypologyModelParams modelParams)
        {
            var modelsFromRepo = await _logic.Get(modelParams);

            if (modelsFromRepo == null)
                return NotFound();

            Response.AddPagination(modelsFromRepo.CurrentPage, modelsFromRepo.PageSize,
                modelsFromRepo.TotalCount, modelsFromRepo.TotalPages);

            var models = _mapper.Map<ICollection<TypologyModelDto>>(modelsFromRepo);
            return Ok(models);
        }

        [HttpGet("{id}/typology/{typId}")]
        public async Task<IActionResult> Get(long id, long typId)
        {
            var modelFromRepo = await _logic.GetById(id, typId);
            if (modelFromRepo == null)
                return NotFound("Ovaj model tipologije nije pronađen");

            var model = _mapper.Map<TypologyModelDto>(modelFromRepo);

            return Ok(model);
        }
    }
}
