using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DogsWeb.API.Data;
using DogsWeb.API.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DogsWeb.API.Controllers
{

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDogsRepository _repository;
        private readonly IMapper _mapper;
        public UsersController(IDogsRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {

            var users = await _repository.GetUsers();
            var usersToReturn = _mapper.Map<IEnumerable<UserForList>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(string id)
        {

            var user = await _repository.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailed>(user);

            return Ok(userToReturn);

        }
    }
}