using ElearningApi.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ElearningApi.Helpers.EntityConfigurations
{
    public class AboutConfigurations
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(m => m.Title).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Description).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Image).IsRequired();
        }
    }
}
