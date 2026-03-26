using BandScope.Common;
using BandScope.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BandScope.DataAccess.Configuration
{
    public class ArtistConfiguration : IEntityTypeConfiguration<Artist>
    {
        public void Configure(EntityTypeBuilder<Artist> builder)
        {
            builder.ToTable("Artist", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(Const.DefaultStringLength).IsUnicode().IsRequired();
            builder.Property(x => x.TheAudioDbId).HasColumnName("AudioDbId").IsRequired(false);
            builder.Property(x => x.Biography).HasColumnName("Biography").HasMaxLength(int.MaxValue).IsUnicode().IsRequired(false);
            builder.Property(x => x.Origin).HasColumnName("Origin").HasMaxLength(Const.DefaultStringLength).IsUnicode().IsRequired(false);
            builder.Property(x => x.LastFmUrl).HasColumnName("LastFmUrl").HasMaxLength(Const.UrlLength).IsUnicode().IsRequired(false);
            builder.Property(x => x.ThumbnailUrl).HasColumnName("ThumbUrl").HasMaxLength(Const.UrlLength).IsUnicode().IsRequired(false);
            builder.Property(x => x.LogoUrl).HasColumnName("LogoUrl").HasMaxLength(Const.UrlLength).IsUnicode().IsRequired(false);

            builder.HasOne(a => a.ThumbnailPicture)
                .WithMany()
                .HasForeignKey(a => a.ThumbnailPictureId)
                .IsRequired(false);

            builder.HasOne(a => a.LogoPicture)
                .WithMany()
                .HasForeignKey(a => a.LogoPictureId)
                .IsRequired(false);

            builder.HasOne(a => a.Style)
                .WithMany(s => s.Artists)
                .HasForeignKey(a => a.StyleId)
                .IsRequired(false);

            builder.HasOne(a => a.Genre)
                .WithMany(g => g.Artists)
                .HasForeignKey(a => a.GenreId)
                .IsRequired(false);

            builder.HasIndex(a => a.Name).HasDatabaseName("IX_Artist_Name");

            SeedMasterData(builder);
        }

        private void SeedMasterData(EntityTypeBuilder<Artist> builder)
        {
            var artistsToAdd = new List<Artist>()
            {
                new Artist
                {
                    Id = 1,
                    Name = "TestArtist",
                    GenreId = 2,
                    StyleId = 2
                },

                new Artist
                {
                    Id = 2,
                    Name = "TestArtist2",
                    GenreId = 2,
                    StyleId = 2
                },

                new Artist
                {
                    Id = 3,
                    Name = "ExampleArtist"
                }
            };

            builder.HasData(artistsToAdd);
        }
    }
}
