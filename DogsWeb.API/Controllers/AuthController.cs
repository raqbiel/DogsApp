using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using SendGrid;
using SendGrid.Helpers.Mail;

namespace DogsWeb.API.Controllers
{

    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        private IEmailSender _emailsender;

        public AuthController(IConfiguration config, UserManager<ApplicationUser> userManager, IEmailSender emailsender)
        {

            _userManager = userManager;
            _emailsender = emailsender;
            _config = config;

        }
    

        [HttpPost("register")]
        public async Task<IActionResult> Register(/*[FromBody]*/UserForRegister userForRegister)
        { 
            List<string> errorList = new List<string>();
            userForRegister.Username = userForRegister.Username.ToLower();

            var userToCreate = new ApplicationUser
            {
                UserName = userForRegister.Username,
                Email = userForRegister.Email,
                SecurityStamp = Guid.NewGuid().ToString()

            };

            var result = await _userManager.CreateAsync(userToCreate, userForRegister.Password);
            if (result.Succeeded)
            {
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
        // Login Metoda
        [HttpPost("login")]
        public async Task<IActionResult> Login(/*[FromBody]*/ UserForLogin userForLogin)
        {
            // Get the User from Database
            var user = await _userManager.FindByNameAsync(userForLogin.Username);
            //var lockOut = await _userManager.IsLockedOutAsync(user);
            //var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value)); // utworzenie klucza zebezpieczen

            if (user != null && await _userManager.CheckPasswordAsync(user, userForLogin.Password))
            {

                // Sprawdz czy email został potwierdzony
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    ModelState.AddModelError(string.Empty, "Użytkownik nie potwierdził adresu e-mail.");

                    return Unauthorized("Wysłaliśmy Ci e-mail potwierdzający. Potwierdź swoją rejestrację w DogsWeb, aby się zalogować.");
                }

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
                // Generowanie tokenu
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new { token = tokenHandler.WriteToken(token), expiration = token.ValidTo, username = user.UserName });

            }
           
            ModelState.AddModelError("", "Nie znaleziono użytkownika/hasła");
            return Unauthorized("Sprawdź dane logowania - wprowadzono nieprawidłową nazwę użytkownika / hasło");
        
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

           if (user.EmailConfirmed)
            {
                return Redirect("http://localhost:4200");
            }

              var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {

                return RedirectToAction("EmailConfirmed", "Auth", new { userId, code });

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
         public IActionResult EmailConfirmed(string userId, string code) 
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return Redirect("http://localhost:4200");
            }
            return Redirect("http://localhost:4200/emailconfirm");
         }

        // [HttpGet("forgotpassword")]
        // [AllowAnonymous]
        // public IActionResult ForgotPassword()
        // {
        //     return View();
        // }


        [HttpPost("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(ApplicationUser model){
            
            if(string.IsNullOrWhiteSpace(model.Email) || model.Email == null){
                  
                    return Unauthorized("Podaj prawidłowy adres email");
            }

            if(ModelState.IsValid){
                var user = await _userManager.FindByEmailAsync(model.Email);
              
                if (user != null && await _userManager.IsEmailConfirmedAsync(user)){

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordLinkReset = Url.Action("ResetPassword", "Auth", new {Email = user.Email, Token = token},  protocol: HttpContext.Request.Scheme);

                    Console.Write(passwordLinkReset);
                    await _emailsender.SendEmailAsync(user.Email, "Dogs Meeting - Reset hasła", "Zresetuj swóje hasło, podając kod: " + token);

                    return Ok(new { Email = user.Email, Token = token});

                }else if(user == null ){
                        return Unauthorized("Brak adresu email w bazie");
                     }
                }
                 return Unauthorized("Podaj prawidłowy adres email");
             }

// [HttpGet]
// [AllowAnonymous]
// public IActionResult ResetPassword(string token, string email)
// {
//     // If password reset token or email is null, most likely the
//     // user tried to tamper the password reset link
//     if (token == null || email == null)
//     {
//         ModelState.AddModelError("", "Nieprawidłowy token");
//     }
//     return View();
// }
      [HttpPost("resetpassword")]
      [AllowAnonymous]
     public async Task<IActionResult> ResetPassword(string token, ResetPasswordModel model)
        {
    if (ModelState.IsValid)
    {
        // Znajdz uzytkownika po adresie email
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null)
        {
            // reset hasła
            var result = await _userManager.ResetPasswordAsync(user, token, model.Password);
            if (result.Succeeded)
            {
                return Ok(new { User = user, Token = token });
            }
            // Display validation errors. For example, password reset token already
            // used to change the password or password complexity rules not met
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
              return Ok(new { Email = user.Email, Token = token});
        }

        // To avoid account enumeration and brute force attacks, don't
        // reveal that the user does not exist
        return Redirect("http://localhost:4200/resetpasswordconfirm");
    }
    // Display validation errors if model state is not valid
    return BadRequest("Zle wprowadzone dane");
}
}
}