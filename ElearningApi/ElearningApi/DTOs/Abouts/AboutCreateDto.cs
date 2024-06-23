using Swashbuckle.AspNetCore.Annotations;

namespace ElearningApi.DTOs.Abouts
{
    public class AboutCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string? Image { get; set; }
        public IFormFile UploadImage { get; set; }
    }
}
