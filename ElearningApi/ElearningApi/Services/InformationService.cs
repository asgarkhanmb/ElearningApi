using AutoMapper;
using ElearningApi.Data;
using ElearningApi.DTOs.Abouts;
using ElearningApi.DTOs.Informations;
using ElearningApi.Models;
using ElearningApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ElearningApi.Services
{
    public class InformationService :IInformationService
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InformationService(IWebHostEnvironment env,
                            AppDbContext context,
                            IMapper mapper)
        {
            _env = env;
            _context = context;
            _mapper = mapper;

        }
        public async Task CreateAsync(InformationCreateDto request)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + request.UploadImage.FileName;

            string path = Path.Combine(_env.WebRootPath, "images", fileName);
            using (FileStream stream = new(path, FileMode.Create))
            {
                await request.UploadImage.CopyToAsync(stream);
            }
            request.Icon = fileName;

            await _context.Informations.AddAsync(_mapper.Map<Information>(request));

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existInform = await _context.Informations.FindAsync(id);
            string path = Path.Combine(_env.WebRootPath, "images", existInform.Icon);

            if (File.Exists(path))
                File.Delete(path);

            _context.Informations.Remove(existInform);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id,InformationEditDto request)
        {
            var existAbout = await _context.Informations.FindAsync(id);

            if (request.UploadImage is not null)
            {
                string oldPath = Path.Combine(_env.WebRootPath, "images", existAbout.Icon);

                if (File.Exists(oldPath))
                    File.Delete(oldPath);


                string fileName = Guid.NewGuid().ToString() + "-" + request.UploadImage.FileName;

                string newPath = Path.Combine(_env.WebRootPath, "images", fileName);

                using (FileStream stream = new(newPath, FileMode.Create))
                {
                    await request.UploadImage.CopyToAsync(stream);
                }

                request.Icon = fileName;
            }
            _mapper.Map(request, existAbout);

            await _context.SaveChangesAsync();
        }

        public async Task<List<InformationDto>> GetAllAsync()
        {
            return _mapper.Map<List<InformationDto>>(await _context.Informations.AsNoTracking().ToListAsync());
        }

        public async Task<InformationDto> GetById(int id)
        {
            return _mapper.Map<InformationDto>(await _context.Informations.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id));
        }
    }
}
