using BandScope.Common.DTOs;
using BandScope.Common.Enums;
using BandScope.Common.Models;
using BandScope.Server.Helper;

namespace BandScope.Server.Mapper
{
    public class DtoToModelMapper
    {
        #region User

        public static User MapToNewUserModel(RegisterDto registerDto)
        {
            return new User
            {
                Nickname = registerDto.Nickname,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Email = registerDto.Email,
                RoleId = RoleEnum.User
            };
        }

        public static User MapToUpdatedUserModel(UserUpdateDto userUpdateDto)
        {
            var userModel = new User
            {
                Nickname = userUpdateDto.Nickname,
                Email = userUpdateDto.UserEmail,
                Id = userUpdateDto.Id
            };

            if (userUpdateDto.UserNewPassword != null)
            {
                userModel.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.UserNewPassword);
            }
            else
            {
                userModel.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userUpdateDto.UserPassword);
            }

            return userModel;
        }

        #endregion

        #region Style

        public static Style MapToStyleModel(StyleDto styleDto)
        {
            return new Style
            {
                Name = styleDto.Name,
            };
        }

        #endregion

        #region Genre

        public static Genre MapToGenreModel(GenreDto genreDto)
        {
            return new Genre
            {
                Name = genreDto.Name,
            };
        }

        #endregion

        #region Picture

        public static Picture MapToNewPictureModel(NewPictureDto newPictureDto)
        {
            return new Picture
            {
                Filename = newPictureDto.Filename,
                Data = PictureDataDecodingHelper.DecodePictureData(newPictureDto.PictureDataUrl)
            };
        }

        #endregion

        #region Artist

        public static Artist MapToNewArtistModel(NewArtistDto newArtistDto)
        {
            return new Artist
            {
                Name = newArtistDto.Name,
                StyleId = newArtistDto.StyleId,
                GenreId = newArtistDto.GenreId,
                Origin = newArtistDto.Origin,
                Biography = newArtistDto.Biography,
                LastFmUrl = newArtistDto.LastFmUrl,
                ThumbnailPictureId = newArtistDto.ThumbnailId,
                LogoPictureId = newArtistDto.LogoId
            };
        }

        public static Artist MapAudioDbArtistToArtistModel(ArtistDetailDto artistDetailDto)
        {
            return new Artist
            {
                Name = artistDetailDto.ArtistName,
                Biography = artistDetailDto.Biography,
                TheAudioDbId = artistDetailDto.TheAudioDbId,
                ThumbnailUrl = artistDetailDto.ThumbnailUrl,
                LogoUrl = artistDetailDto.LogoUrl,
                StyleId = artistDetailDto.StyleId,
                GenreId = artistDetailDto.GenreId,
                Origin = artistDetailDto.Origin,
                LastFmUrl = artistDetailDto.LastFmUrl,
            };
        }

        #endregion

        #region Album

        public static Album MapToNewAlbumModel(AlbumDto albumDto)
        {
            var model = new Album
            {
                Name = albumDto.Name,
                ReleaseYear = albumDto.ReleaseYear,
                ImageUrl = albumDto.ImageUrl,
            };

            if (albumDto.ArtistId != null)
            {
                model.ArtistId = (int)albumDto.ArtistId;
            }

            return model;
        }

        #endregion

        #region Review

        public static Review MapToNewReviewModel(FullReviewDto fullReviewDto)
        {
            return new Review
            {
                ArtistId = fullReviewDto.ArtistId,
                UserId = fullReviewDto.UserId,
                Comment = fullReviewDto.Comment,
                Rating = fullReviewDto.Rating,
                CreatedAt = DateTime.Now
            };
        }

        public static Review MapToUpdatedReviewModel(FullReviewDto fullReviewDto)
        {
            return new Review
            {
                Id = (int)fullReviewDto.Id,
                ArtistId = fullReviewDto.ArtistId,
                UserId = fullReviewDto.UserId,
                Comment = fullReviewDto.Comment,
                Rating = fullReviewDto.Rating,
                CreatedAt = DateTime.Now
            };
        }

        #endregion
    }
}
