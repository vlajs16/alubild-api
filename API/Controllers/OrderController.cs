﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Interfaces;
using Helpers;
using DataTransferObject.OrderDto;
using Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderLogic _logic;
        public OrderController(IMapper mapper, IOrderLogic logic)
        {
            _mapper = mapper;
            _logic = logic;
        }

        // GET: api/order/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var order = await _logic.GetById(id);
            if (order == null)
                return NotFound("Nije pronadjen ovaj nalog");
            return Ok(order);
        }

        // GET: api/order/user/1
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetByUser(long id, [FromQuery]OrderParams orderParams)
        {
            orderParams.UserId = id;
            var orders = await _logic.GetByUser(orderParams);
            Response.AddPagination(orders.CurrentPage, orders.PageSize, orders.TotalCount, orders.TotalPages);
            var odredsToReturn = _mapper.Map<List<OrderForList>>(orders);
            return Ok(odredsToReturn);
        }

        // POST api/order
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderToInsertDto order)
        {
            var orderToInsert = _mapper.Map<Order>(order);

            if (!await _logic.Insert(orderToInsert))
                return BadRequest("Nije uspelo cuvanje naloga");
            return Ok();
        }

        // PUT api/order/price/5
        [HttpPut("price/{id}")]
        public async Task<IActionResult> PutPrice(long id, [FromForm] double price)
        {
            if (!await _logic.UpdatePrice(id, price))
                return BadRequest("Postavljanje cene nije uspelo.");
            return Ok();
        }

        // PUT api/order/date/5
        [HttpPut("date/{id}")]
        public async Task<IActionResult> PutDate(long id, [FromForm] DateTime date)
        {
            if (!await _logic.UpdateScheduledDate(id, date))
                return BadRequest("Zakazivanje datuma izrade posla nije uspelo.");
            return Ok();
        }

        // PUT api/order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _logic.GetById(id);
            if (order == null)
                return NotFound("Ne moze se obrisati nepostojeci order");
            if (await _logic.Delete(order))
                return Ok("Uspesno obrisan");
            return BadRequest("Greska prilikom brisanja");
        }
    }
}
