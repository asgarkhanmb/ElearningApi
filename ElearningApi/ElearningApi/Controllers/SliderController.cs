using ElearningApi.DTOs.Sliders;
using ElearningApi.Helpers.Extensions;
using ElearningApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace ElearningApi.Controllers
{
    public class SliderController :BaseController
    {
        private readonly ISliderService _slidlerService;

        public SliderController(ISliderService sliderService)
        {
            _slidlerService = sliderService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] SliderCreateDto request)
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

            await _slidlerService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), request);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _slidlerService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (id == null) return BadRequest();
            return Ok(await _slidlerService.GetById(id));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            await _slidlerService.DeleteAsync(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync([FromRoute] int id, [FromForm] SliderEditDto request)
        {
            if (id == null) return BadRequest();
            await _slidlerService.EditAsync(id, request);
            return Ok();
        }
    }
}
