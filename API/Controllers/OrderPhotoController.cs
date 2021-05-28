using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using DataTransferObject.OrderPhotoDto;
using System.IO;
using Logic.Interfaces;
using Domain;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderPhotoController : ControllerBase
    {
        private readonly IHostingEnvironment _environment;
        private readonly IUserLogic _userLogic;
        private readonly IOrderLogic _orderLogic;

        public OrderPhotoController(IHostingEnvironment environment, IUserLogic userLogic, IOrderLogic orderLogic)
        {
            _environment = environment;
            _userLogic = userLogic;
            _orderLogic = orderLogic;
        }

        // GET: api/<OrderPhotoController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderPhotoController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderPhotoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] InsertPhotoDto photoToInsert)
        {
            if (photoToInsert.File.Length > 0)
            {
                var user = await _userLogic.Get(photoToInsert.UserId);

                var fileName = $"IMG_{user.Name}_{user.Surname}_{photoToInsert.File.FileName}";
                try
                {
                    if (!Directory.Exists( Environment.CurrentDirectory + "\\Uploaded_Documents\\Users\\"))
                    {
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\Uploaded_Documents\\Users\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(Environment.CurrentDirectory + "\\Uploaded_Documents\\Users\\" + fileName))
                    {
                        await photoToInsert.File.CopyToAsync(fileStream);
                        fileStream.Flush();

                        var result =  await _orderLogic.InsertPhotoUrl(new OrderPhoto
                        {
                            Important = true,
                            OrderId = photoToInsert.OrderId,
                            OrderUserId = photoToInsert.UserId,
                            Url = $"http://localhost:5000/Uploaded_Documents/Users/{fileName}",
                        });
                        if (result)
                            return Ok();
                        return BadRequest();
                    }
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest("Neuspesno");
            }
        }
    }
}
