using ElearningApi.DTOs.Abouts;
using ElearningApi.DTOs.Informations;

namespace ElearningApi.Services.Interfaces
{
    public interface IInformationService
    {
        Task CreateAsync(InformationCreateDto request);
        Task<List<InformationDto>> GetAllAsync();
        Task<InformationDto> GetById(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, InformationEditDto request);
    }
}
