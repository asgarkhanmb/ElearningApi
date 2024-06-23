using AutoMapper;
using ElearningApi.Data;
using ElearningApi.DTOs.Sliders;
using ElearningApi.Models;
using ElearningApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ElearningApi.Services
{
    public class SliderService : ISliderService
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public SliderService(IWebHostEnvironment env,
                             AppDbContext context,
                             IMapper mapper)
        {
            _env = env;
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateAsync(SliderCreateDto request)
        {

            string fileName = Guid.NewGuid().ToString() + "-" + request.UploadImage.FileName;

            string path = Path.Combine(_env.WebRootPath, "images", fileName);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await request.UploadImage.CopyToAsync(stream);
            }

            request.Image = fileName;

            await _context.Sliders.AddAsync(_mapper.Map<Slider>(request));

            await _context.SaveChangesAsync();


        }

        public async Task DeleteAsync(int id)
        {
            var existSlider = await _context.Sliders.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, "images", existSlider.Image);

            if (File.Exists(path))
                File.Delete(path);

            _context.Sliders.Remove(existSlider);
            await _context.SaveChangesAsync();

        }

        public async Task EditAsync(int id, SliderEditDto request)
        {
            var existSlider = await _context.Sliders.FindAsync(id);

            if (request.UploadImage is not null)
            {
                string oldPath = Path.Combine(_env.WebRootPath, "images", existSlider.Image);

                if (File.Exists(oldPath))
                    File.Delete(oldPath);


                string fileName = Guid.NewGuid().ToString() + "-" + request.UploadImage.FileName;

                string newPath = Path.Combine(_env.WebRootPath, "images", fileName);

                using (FileStream stream = new(newPath, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }

                request.Image = fileName;
            }
            _mapper.Map(request, existSlider);

            await _context.SaveChangesAsync();
        }

        public async Task<List<SliderDto>> GetAllAsync()
        {
            return _mapper.Map<List<SliderDto>>(await _context.Sliders.AsNoTracking().ToListAsync());
        }

        public async Task<SliderDto> GetById(int id)
        {
            return _mapper.Map<SliderDto>(await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id));
        }
    }
}
