using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WallerAPI.Services.Interfaces;

namespace WallerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IPhotoServices _photoServices;
        private readonly IMapper _mapper;

        public PhotosController(IMapper mapper, IPhotoServices photoServices, 
            ILogger<UsersController> logger)
        {
            _mapper = mapper;
            _photoServices = photoServices;
            _logger = logger;
        }


        // GET: api/<PhotosController>
        [HttpGet]
        public IActionResult GetAllPhotos()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<PhotosController>/5
        [HttpGet("{id}")]
        public IActionResult GetPhoto(int id)
        {
            return "value";
        }

        // POST api/<PhotosController>
        [HttpPost]
        public void Addphoto([FromBody] string value)
        {
        }

        // PUT api/<PhotosController>/5
        [HttpPut("{id}")]
        public void UpdatePhoto(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PhotosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
