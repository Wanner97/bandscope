using BandScope.Common;
using BandScope.Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BandScope.DataAccess.Configuration
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Album", "dbo");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasColumnName("Name").HasMaxLength(Const.DefaultStringLength).IsUnicode().IsRequired();
            builder.Property(x => x.ReleaseYear).HasColumnName("ReleaseYear").IsRequired(false);
            builder.Property(x => x.ImageUrl).HasColumnName("ImageUrl").HasMaxLength(Const.UrlLength).IsUnicode().IsRequired(false);

            builder.HasOne(a => a.Artist)
                .WithMany(a => a.Albums)
                .HasForeignKey(a => a.ArtistId)
                .IsRequired();
        }
    }
}
