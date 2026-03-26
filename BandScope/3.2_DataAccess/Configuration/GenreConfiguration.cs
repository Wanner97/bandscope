using BandScope.Common;
using BandScope.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BandScope.DataAccess.Configuration
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genre", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(Const.DefaultStringLength).IsUnicode().IsRequired();

            SeedMasterData(builder);
        }

        private void SeedMasterData(EntityTypeBuilder<Genre> builder)
        {
            var genresToAdd = new List<Genre>()
            {
                new Genre
                {
                    Id = Const.DefaultUnknownId,
                    Name = "Unknown"
                },

                new Genre
                {
                    Id = 2,
                    Name = "TestGenre"
                },

                new Genre
                {
                    Id = 3,
                    Name = "ExampleGenre"
                }
            };

            builder.HasData(genresToAdd);
        }
    }
}
