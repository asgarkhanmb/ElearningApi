using ElearningApi.DTOs.Abouts;

namespace ElearningApi.Services.Interfaces
{
    public interface IAboutService
    {
        Task CreateAsync(AboutCreateDto request);
        Task<List<AboutDto>> GetAllAsync();
        Task<AboutDto> GetById(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, AboutEditDto request);
    }
}
