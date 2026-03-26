using BandScope.Common;
using BandScope.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BandScope.DataAccess.Configuration
{
    public class StyleConfiguration : IEntityTypeConfiguration<Style>
    {
        public void Configure(EntityTypeBuilder<Style> builder)
        {
            builder.ToTable("Style", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(Const.DefaultStringLength).IsUnicode().IsRequired();

            SeedMasterData(builder);
        }

        private void SeedMasterData(EntityTypeBuilder<Style> builder)
        {
            var stylesToAdd = new List<Style>()
            {
                new Style
                {
                    Id = Const.DefaultUnknownId,
                    Name = "Unknown"
                },

                new Style
                {
                    Id = 2,
                    Name = "TestStyle"
                },

                new Style
                {
                    Id = 3,
                    Name = "ExampleStyle"
                }
            };

            builder.HasData(stylesToAdd);
        }
    }
}
