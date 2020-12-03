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
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryLogic _logic;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categoriesFromRepo = await _logic.GetAll();
            if (categoriesFromRepo == null)
                return NotFound();

            var categories = _mapper.Map<ICollection<CategoryDto>>(categoriesFromRepo);

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var categoryFromRepo = await _logic.GetById(id);
            if (categoryFromRepo == null)
                return NotFound("Željena kategorija nije pronađena");

            var category = _mapper.Map<CategoryDto>(categoryFromRepo);
            return Ok(category);
        }

        [HttpGet("typology/{id}")]
        public async Task<IActionResult> GetByTypology(long id)
        {
            var categoriesFromRepo = await _logic.GetByTypology(id);
            if (categoriesFromRepo == null)
                return NotFound();

            var categories = _mapper.Map<ICollection<CategoryDto>>(categoriesFromRepo);

            return Ok(categories);
        }
    }
}
