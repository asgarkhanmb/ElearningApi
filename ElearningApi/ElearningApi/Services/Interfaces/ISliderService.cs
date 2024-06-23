using ElearningApi.DTOs.Sliders;

namespace ElearningApi.Services.Interfaces
{
    public interface ISliderService
    {
        Task CreateAsync(SliderCreateDto request);
        Task<List<SliderDto>> GetAllAsync();
        Task<SliderDto> GetById(int id);
        Task DeleteAsync(int id);
        Task EditAsync(int id, SliderEditDto request);
    }
}
