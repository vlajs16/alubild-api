using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DataTransferObject.UserDto;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;

        public AuthController(IMapper mapper, UserManager<User> userManager, IConfiguration config,
            SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _config = config;
            _signInManager = signInManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Post(UserForRegisterDto registerUser) 
        {
            var user = _mapper.Map<User>(registerUser);
            user.RegistrationDate = DateTime.Now;
            user.Enabled = true;

            var result = _userManager.CreateAsync(user, registerUser.Password).Result;

            if (result.Succeeded)
            {
                var secondResult = await _userManager.AddToRoleAsync(user, "client");
                if (secondResult.Succeeded)
                {
                    var userToReturn = _mapper.Map<UserDetailDto>(user);
                    return Ok(new
                    {
                        token = await GenerateJwtToken(user),
                        user = userToReturn
                    });
                }
            }

            return BadRequest(result.Errors);
        }




        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Post(UserLoginDto userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.UserName);
            if (user == null)
                return Unauthorized($"Ne postoji korisnik sa korisničkim imenom {userLogin.UserName}");

            if (!user.Enabled)
                return (Unauthorized("Ovaj nalog je suspendovan"));

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);

            if (result.Succeeded)
            {
                var appUser = _mapper.Map<UserForListDto>(user);

                return Ok(new
                {
                    token = await GenerateJwtToken(user),
                    user = appUser
                });
            }


            return Unauthorized("Pogrešna korisnička šifra");
        }


        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config
                .GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
