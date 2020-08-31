using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DogsWeb.API.Data;
using DogsWeb.API.Dtos;
using DogsWeb.API.Email;
using DogsWeb.API.Helpers;
using DogsWeb.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace DogsWeb.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signManager;

        private IEmailSender _emailsender;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration config, IMapper mapper, UserManager<ApplicationUser> userManager,
        ApplicationDbContext context, IEmailSender emailsender)
        {

            _userManager = userManager;
            _context = context;
            _emailsender = emailsender;
            _config = config;
            _mapper = mapper;

            // var conf = new MapperConfiguration(cfg =>
            // {
            //     cfg.CreateMap<UserForLogin, User>();
            // });

            // _mapper = conf.CreateMapper();


        }
    

        [HttpPost("register")]
        public async Task<IActionResult> Register(/*[FromBody]*/UserForRegister userForRegister)
        { //[FromBody] zamiast [ApiController]

            List<string> errorList = new List<string>();
            //uzytkownik logowanie z malej litery aby uniknac duplikatu J i j . User bedzie mogl sie logowac zarowno z J jak i j.
           
            
            userForRegister.Username = userForRegister.Username.ToLower();

            var userToCreate = new ApplicationUser
            {

                Email = userForRegister.Email,
                UserName = userForRegister.Username,
                SecurityStamp = Guid.NewGuid().ToString()

            };

            //var createdUser = await _repo.Register(userToCreate, userForRegister.Password);
            var result = await _userManager.CreateAsync(userToCreate, userForRegister.Password);
            if (result.Succeeded)
            {

                //  await _userManager.AddToRoleAsync(userToCreate, "Customer");

                var code = await _userManager.GenerateEmailConfirmationTokenAsync(userToCreate);

                string callbackUrl = Url.Action("ConfirmEmail", "Auth", new { UserId = userToCreate.Id, Code = code }, protocol: HttpContext.Request.Scheme);
                await _emailsender.SendEmailAsync(userToCreate.Email, "Dogs Meeting - Potwierdź adres email", "Potwierdź swój adres e-mail, klikając ten link:  <a href=\"" + callbackUrl + "\">LINK</a>");
                return Ok(new { username = userToCreate.UserName, email = userToCreate.Email, status = 1, message = "Rejestracja zakończona sukcesem" });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                    errorList.Add(error.Description);
                }
            }

            return BadRequest(new JsonResult(errorList));

        }
        // Login Method
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLogin userForLogin)
        {
           
            // Get the User from Database
            var user = await _userManager.FindByNameAsync(userForLogin.Username);

            //var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)); // utworzenie klucza zebezpieczen

            if (user != null && await _userManager.CheckPasswordAsync(user, userForLogin.Password))
            {

                // THen Check If Email Is confirmed
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty, "Użytkownik nie potwierdził adresu e-mail.");

                    return Unauthorized(new { LoginError = "Wysłaliśmy Ci e-mail potwierdzający. Potwierdź swoją rejestrację w DogsWeb, aby się zalogować." });
                }

                // get user Role
                //var roles = await _userManager.GetRolesAsync(user);

                var tokenHandler = new JwtSecurityTokenHandler();

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, userForLogin.Username),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id),
                        new Claim("LoggedOn", DateTime.Now.ToString()),

                     }),

                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature),
                    Expires = DateTime.Now.AddDays(1)
                };

                // Generate Token

                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token), expiration = token.ValidTo, username = user.UserName });

            }

            // return error
            ModelState.AddModelError("", "Nie znaleziono użytkownika/hasła");
            return Unauthorized(new { LoginError = "Sprawdź dane logowania - wprowadzono nieprawidłową nazwę użytkownika / hasło" });

        }

        [HttpGet("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "Wymagany userId oraz kod");
                return BadRequest(ModelState);

            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return new JsonResult("ERROR");
            }


            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {

                return Redirect("http://localhost:4200");

            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var error in result.Errors)
                {
                    errors.Add(error.ToString());
                }
                return new JsonResult(errors);
            }


        }


    }
}