using ElearningApi.DTOs.Abouts;
using ElearningApi.Helpers.Extensions;
using ElearningApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElearningApi.Controllers
{
    public class AboutController : BaseController
    {
        private readonly IAboutService _aboutService;
        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AboutCreateDto request)
        {

            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (!request.UploadImage.CheckFileType("image"))
            {
                ModelState.AddModelError("Image", "Input can accept only image format");
                return BadRequest();
            }

            if (!request.UploadImage.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB");
                return BadRequest();
            }

            await _aboutService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), request);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _aboutService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            return Ok(await _aboutService.GetById(id));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();
            await _aboutService.DeleteAsync(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync([FromRoute] int id, [FromForm] AboutEditDto request)
        {

            if (id == null) return BadRequest();
            await _aboutService.EditAsync(id, request);
            return Ok();
        }

    }
}
