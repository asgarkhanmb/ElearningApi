using ElearningApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElearningApi.Helpers.EntityConfigurations
{
    public class SliderConfigurations
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(m => m.Title).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Description).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Image).IsRequired();
        }
    }
}
    