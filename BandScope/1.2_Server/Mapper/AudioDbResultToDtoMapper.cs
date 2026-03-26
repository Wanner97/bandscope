using BandScope.Common.DTOs;
using BandScope.Common.DTOs.External;

namespace BandScope.Server.Mapper
{
    public class AudioDbResultToDtoMapper
    {
        public static ArtistDetailDto MapToArtistDetailDto(TheAudioDbArtist theAudioDbArtist)
        {
            return new ArtistDetailDto
            {
                ArtistName = theAudioDbArtist.Name,
                Biography = theAudioDbArtist.Biography,
                TheAudioDbId = Convert.ToInt32(theAudioDbArtist.AudioDbId),
                ThumbnailUrl = theAudioDbArtist.ThumbUrl,
                LogoUrl = theAudioDbArtist.LogoUrl,
                StyleName = theAudioDbArtist.StyleName,
                GenreName = theAudioDbArtist.GenreName,
                AverageRating = 0,
                FavorizedCount = 0,
                Origin = theAudioDbArtist.Origin,
                LastFmUrl = theAudioDbArtist.LastFmUrl,
                IsFromExternalApi = true
            };
        }

        public static ArtistListDto MapToArtistListDto(TheAudioDbArtist theAudioDbArtist)
        {
            return new ArtistListDto
            {
                ArtistName = theAudioDbArtist.Name,
                TheAudioDbId = Convert.ToInt32(theAudioDbArtist.AudioDbId),
                ThumbnailUrl = theAudioDbArtist.ThumbUrl,
                StyleName = theAudioDbArtist.StyleName,
                GenreName = theAudioDbArtist.GenreName,
                AverageRating = 0,
                FavorizedCount = 0,
                IsFromExternalApi = true
            };
        }

        public static AlbumDto MapToNewAlbumDto(TheAudioDbAlbum theAudioDbAlbum)
        {
            return new AlbumDto
            {
                Name = theAudioDbAlbum.Name,
                ReleaseYear = Convert.ToInt32(theAudioDbAlbum.ReleaseYear),
                ImageUrl = theAudioDbAlbum.ImageUrl,
            };
        }
    }
}
