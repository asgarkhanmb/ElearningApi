using Swashbuckle.AspNetCore.Annotations;

namespace ElearningApi.DTOs.Informations
{
    public class InformationCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string? Icon { get; set; }
        public IFormFile UploadImage { get; set; }
    }
}
