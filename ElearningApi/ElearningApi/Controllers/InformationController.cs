using ElearningApi.DTOs.Informations;
using ElearningApi.Helpers.Extensions;
using ElearningApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ElearningApi.Controllers
{
    public class InformationController : BaseController
    {
        private readonly IInformationService _informationService;

        public InformationController(IInformationService informationService)
        {
            _informationService = informationService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] InformationCreateDto request)
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

            await _informationService.CreateAsync(request);

            return CreatedAtAction(nameof(Create), request);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _informationService.GetAllAsync());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return Ok(await _informationService.GetById(id));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            if (id == null) return BadRequest();
            await _informationService.DeleteAsync(id);
            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync([FromRoute] int id, [FromForm] InformationEditDto request)
        {
            if (id == null) return BadRequest();
            await _informationService.EditAsync(id, request);
            return Ok();
        }
    }
}
