using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DogsWeb.API.Data;
using DogsWeb.API.Dtos;
using DogsWeb.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DogsWeb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
       
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _config = config;
            _mapper = mapper;
            _repo = repo;
           
          var conf = new MapperConfiguration(cfg => {
             cfg.CreateMap<UserForLogin, User>();
                });

            _mapper = conf.CreateMapper();
          

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(/*[FromBody]*/UserForRegister userForRegister)
        { //[FromBody] zamiast [ApiController]

            // if(!ModelState.IsValid)
            // return BadRequest(ModelState);

            //uzytkownik logowanie z malej litery aby uniknac duplikatu J i j . User bedzie mogl sie logowac zarowno z J jak i j.
            userForRegister.Username = userForRegister.Username.ToLower();

            if (await _repo.UserExist(userForRegister.Username))
                return BadRequest("Użytkownik już istnieje");

            var userToCreate = new User
            {
                Username = userForRegister.Username
            };

            var createdUser = await _repo.Register(userToCreate, userForRegister.Password);

            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UserForRegister userForLogin)
        {

            var userFromRepo = await _repo.Login(userForLogin.Username.ToLower(), userForLogin.Password);

            if (userFromRepo == null) // sprawdzamy czy dane zgadzaja sie z danymi z db
                return Unauthorized();

            var claims = new[] // budowa token, 2 roszczenia
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)); // utworzenie klucza zebezpieczen
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); // nastepnie go szyfrujemy z uzyciem SHA512
            
            var tokenDesc = new SecurityTokenDescriptor{
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1), // wygasnie w ciagu dnia liczac od teraz
                    SigningCredentials = creds
            };

            var tokenToHandle = new JwtSecurityTokenHandler(); // modul obslugi tokena JWT
            var token = tokenToHandle.CreateToken(tokenDesc); // utworzenie tokena bazujach na danych z tokenDesc

            return Ok(new {
                token = tokenToHandle.WriteToken(token) // odpowiedz do klienta
            });

        
        }
        [HttpPost("resetpassword/{id}")]
        public async Task<IActionResult> ResetPassword(UserForLogin userForLogin){

           
            var userToReset = await _repo.GetUserName(userForLogin.Username);
            if(userToReset == null)
                return Unauthorized();

            userToReset = _mapper.Map(userForLogin, userToReset);
            var resetedUser = await _repo.ResetPassword(userToReset, userForLogin.Password);
           
        
         return StatusCode(201);
      
    }


    }
}