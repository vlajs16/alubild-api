using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObject.UserDto;
using Domain;
using Helpers;
using Logic.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserLogic _logic;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UserController(IUserLogic logic, UserManager<User> userManager,
            IMapper mapper)
        {
            _logic = logic;
            _userManager = userManager;
            _mapper = mapper;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var userFormRepo = await _logic.Get(id);

            if (userFormRepo == null)
                return NotFound("Korisnik nije pronadjen");

            var user = _mapper.Map<UserDetailDto>(userFormRepo);

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] UserParams userParams)
        {
            var usersFromRepo = await _logic.Get(userParams);
            if (usersFromRepo == null)
                return NotFound("Korisnici nisu pronadjeni prema ovoj kategoriji");

            Response.AddPagination(usersFromRepo.CurrentPage, usersFromRepo.PageSize, 
                usersFromRepo.TotalCount, usersFromRepo.TotalPages);
            
            var users = _mapper.Map<ICollection<UserForListDto>>(usersFromRepo);
            return Ok(users);
        }

        [HttpPut("password/{id}")]
        public async Task<IActionResult> ChangePassword(long id, [FromBody] UserChangePasswordDto userChange)
        {
            userChange.Id = id;
            var user = await _userManager.FindByIdAsync(userChange.Id.ToString());
            if (user == null)
                return NotFound("Korisnik nije pronadjen");

            if (!user.Enabled)
                return (Unauthorized("Ovaj nalog je suspendovan"));

            if (!await _userManager.CheckPasswordAsync(user, userChange.OldPassword))
            {
                return BadRequest("Pogrešan unos trenutne šifre šifru");
            }
            
            var result = await _userManager.ChangePasswordAsync(user, userChange.OldPassword, userChange.NewPassword);

            if (result.Succeeded)
            {
                return Ok("Uspešno ste izmenili korisničku šifru");
            }

            return BadRequest("Neuspešna izmena šifre, pokušajte ponovo");
        }

        [HttpPut("email/{id}")]
        public async Task<IActionResult> ChangeEmail(long id, [FromBody] UserChangeEmailPhone userChange)
        {
            if (string.IsNullOrWhiteSpace(userChange.Email))
                return BadRequest("Email nije unet");

            userChange.Id = id;

            var user = await _userManager.FindByIdAsync(userChange.Id.ToString());
            if (user == null)
                return NotFound("Korisnik nije pronadjen");

            if (!user.Enabled)
                return (Unauthorized("Ovaj nalog je suspendovan"));

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, userChange.Email);
            var result = await _userManager.ChangeEmailAsync(user, userChange.Email, token);

            if (result.Succeeded)
            {
                return Ok("Uspešno ste izmenili svoj email");
            }

            return BadRequest("Nije moguće izmeniti email");
        }

        [HttpPut("phone/{id}")]
        public async Task<IActionResult> ChangePhoneNumber(long id, [FromBody] UserChangeEmailPhone userChange)
        {
            if (string.IsNullOrWhiteSpace(userChange.PhoneNumber))
                return BadRequest("Telefon nije unet");

            userChange.Id = id;

            var user = await _userManager.FindByIdAsync(userChange.Id.ToString());
            if (user == null)
                return NotFound("Korisnik nije pronadjen");

            if (!user.Enabled)
                return (Unauthorized("Ovaj nalog je suspendovan"));

            var token = await _userManager.GenerateChangePhoneNumberTokenAsync(user, userChange.PhoneNumber);
            var result = await _userManager.ChangePhoneNumberAsync(user, userChange.PhoneNumber, token);

            if (result.Succeeded)
            {
                return Ok("Uspešno ste izmenili svoj broj telefona");
            }

            return BadRequest("Nije moguće izmeniti broj telefona");
        }

        [HttpPut("disable/{id}")]
        public async Task<IActionResult> DisableUser(long id)
        {
            if (await _logic.DisableUser(id))
                return Ok("Korisnik je banovan");

            return BadRequest("Trenutno nije moguće onesposobiti korisnika");
        }
    }
}
