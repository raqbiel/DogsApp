using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DogsWeb.API.Data;
using DogsWeb.API.Dtos;
using DogsWeb.API.Helpers;
using DogsWeb.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DogsWeb.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IDogsRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IDogsRepository repo, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repo = repo;

            Account account = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id){

            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturn>(photoFromRepo);

            return Ok(photo);
        }


        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(string userId, [FromForm]PhotoForCreation photoForCreation){

                 if(userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

                var userFromRepo = await _repo.GetUser(userId);

                var file = photoForCreation.File;
                var uploadResult = new ImageUploadResult();

                if(file.Length > 0){
                    using (var stream = file.OpenReadStream() ){
                        var uploadParams = new ImageUploadParams(){

                            File = new FileDescription(file.Name, stream),
                            Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                        };

                        uploadResult = _cloudinary.Upload(uploadParams);
                    }
                }

                photoForCreation.Url = uploadResult.Url.ToString();
                photoForCreation.PublicId = uploadResult.PublicId;

                var photo = _mapper.Map<Photo>(photoForCreation);
                if(!userFromRepo.Photos.Any(u => u.isMain))
                    photo.isMain = true;


                userFromRepo.Photos.Add(photo);

             

                if(await _repo.SaveAll()){

                    var photoToReturn = _mapper.Map<PhotoForReturn>(photo);
                    return CreatedAtRoute("GetPhoto", new {userId = userId, id = photo.Id}, photoToReturn);
                }

                return BadRequest("Nie można dodać zdjęcia");
        }


    }
}