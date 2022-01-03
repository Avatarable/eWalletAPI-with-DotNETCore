using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WallerAPI.Commons;
using WallerAPI.Models.Domain;
using WallerAPI.Models.DTOs;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly ILogger<PhotosController> _logger;
        private readonly IPhotoServices _photoServices;
        private readonly IMapper _mapper;

        public PhotosController(IMapper mapper, IPhotoServices photoServices, 
            ILogger<PhotosController> logger)
        {
            _mapper = mapper;
            _photoServices = photoServices;
            _logger = logger;
        }


        // GET: api/<PhotosController>
        [HttpGet]
        public IActionResult GetAllPhotos()
        {
            var photos = _photoServices.GetAllPhotos();
            if(photos == null)
            {
                ModelState.AddModelError("Not found", "No result found for photos");
                return NotFound(Utility.BuildResponse<PhotoToReturnDTO>(false, "Result is empty", ModelState, null));
            }

            var listOfPhotosToReturn = new List<PhotoToReturnDTO>();
            foreach(var photo in photos)
            {
                listOfPhotosToReturn.Add(_mapper.Map<PhotoToReturnDTO>(photo));
            }
            return Ok(Utility.BuildResponse<List<PhotoToReturnDTO>>(true, "List of photos", ModelState, listOfPhotosToReturn));
        }

        // GET api/<PhotosController>/5
        [HttpGet("{id}")]
        public IActionResult GetUserPhoto(string userId)
        {
            var photo = _photoServices.GetUserPhoto(userId);
            if (photo == null)
            {
                ModelState.AddModelError("Not found", "No photo found for user with specified Id");
                return NotFound(Utility.BuildResponse<PhotoToReturnDTO>(false, "Result is empty", ModelState, null));
            }

            var photoToReturn = _mapper.Map<PhotoToReturnDTO>(photo);
            return Ok(Utility.BuildResponse<PhotoToReturnDTO>(true, "User Photo", null, photoToReturn));
        }

        // POST api/<PhotosController>
        [HttpPost]
        public async Task<IActionResult> Addphoto([FromForm] PhotoUploadDTO model, string userId)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var file = model.Photo;
            if(file.Length > 0)
            {
                var uploadStatus = await _photoServices.UploadPhotoAsync(model, currentUserId);
                if (uploadStatus.Item1)
                {
                    var photo = _mapper.Map<Photo>(model);
                    var res = await _photoServices.AddPhoto(photo, currentUserId);
                    if (!res)
                    {
                        ModelState.AddModelError("Failed", "Could not add photo to database");
                        return BadRequest(Utility.BuildResponse<ImageUploadResult>(false, "Failed to add to database", ModelState, null));
                    }
                    var photoToReturn = _mapper.Map<PhotoToReturnDTO>(uploadStatus.Item2);
                    return Ok(Utility.BuildResponse<PhotoToReturnDTO>(true, "Uploaded successfully", null, photoToReturn));
                }
                ModelState.AddModelError("Failed", "File could not be uploaded to cloudinary");
                return BadRequest(Utility.BuildResponse<ImageUploadResult>(false, "Failed to upload", ModelState, null));
            }
            ModelState.AddModelError("Invalid", "File size must not be empty");
            return BadRequest(Utility.BuildResponse<ImageUploadResult>(false, "File is empty", ModelState, null));
        }

        [HttpDelete("delete-photo/{publicId}")]
        public async Task<IActionResult> Delete(string publicId)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var res = await _photoServices.DeleteUserPhoto(publicId, currentUserId);
            if (!res)
            {
                ModelState.AddModelError("Failed", "Could not delete photo");
                return BadRequest(Utility.BuildResponse<PhotoToReturnDTO>(false, "Delete failed!", ModelState, null));
            }

            return Ok(Utility.BuildResponse<string>(true, "Photo deleted sucessful!", null, ""));
        }
    }
}
