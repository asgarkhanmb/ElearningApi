﻿using ElearningApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ElearningApi.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Information> Informations { get; set; }
    }
}
