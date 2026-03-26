using BandScope.Common.DTOs;
using BandScope.Common.Models;
using BandScope.Server.Helper;

namespace BandScope.Server.Mapper
{
    public class ModelToDtoMapper
    {
        #region User

        public static UserUpdateDto MapToUserUpdateDto(User user)
        {
            return new UserUpdateDto
            {
                Id = user.Id,
                Nickname = user.Nickname,
                UserEmail = user.Email,
                UserPassword = ""
            };
        }

        public static UserProfileDto MapToUserProfileDto(User user)
        {
            return new UserProfileDto
            {
                Id = user.Id,
                Nickname = user.Nickname,
            };
        }

        #endregion

        #region Artist

        public static ArtistListDto MapToArtistListDto(Artist artist)
        {
            var reviewList = artist.Reviews.ToList();
            var ratingSum = 0;

            if (reviewList.Any())
            {
                foreach (var review in reviewList)
                {
                    ratingSum += review.Rating;
                }
            }

            var artistListDto = new ArtistListDto
            {
                ArtistName = artist.Name,
                Id = artist.Id,
                IsFromExternalApi = false,
                TheAudioDbId = artist.TheAudioDbId
            };

            if (artist.Style != null)
            {
                artistListDto.StyleName = artist.Style.Name;
            }

            if (artist.Genre != null)
            {
                artistListDto.GenreName = artist.Genre.Name;
            }

            if (artist.ThumbnailPicture != null)
            {
                artistListDto.ThumbnailUrl = PictureDataUrlHelper.GeneratePictureDataUrl(artist.ThumbnailPicture);
            }
            else if (artist.ThumbnailUrl != null)
            {
                artistListDto.ThumbnailUrl = artist.ThumbnailUrl;
            }

            if (!reviewList.Any())
            {
                artistListDto.AverageRating = 0;
            }
            else
            {
                double average = (double)ratingSum / (double)reviewList.Count;

                artistListDto.AverageRating = (int)Math.Round(average, MidpointRounding.AwayFromZero);
            }

            if (artist.FavorizedByUsers.Any())
            {
                artistListDto.FavorizedCount = artist.FavorizedByUsers.Count;
            }
            else
            {
                artistListDto.FavorizedCount = 0;
            }

            return artistListDto;
        }

        public static ArtistDetailDto MapToArtistDetailDto(Artist artist)
        {
            var reviewList = new List<Review>();
            var reviewDtos = new List<ReviewOnArtistDetailDto>();

            var ratingSum = 0;

            if (artist.Reviews != null)
            {
                reviewList = artist.Reviews.ToList();

                if (reviewList.Any())
                {
                    foreach (var review in reviewList)
                    {
                        ratingSum += review.Rating;
                        reviewDtos.Add(MapToReviewOnArtistDetailDto(review));
                    }
                }
            }

            var artistDetailDto = new ArtistDetailDto
            {
                Id = artist.Id,
                ArtistName = artist.Name,
                IsFromExternalApi = false,
                Origin = artist.Origin,
                LastFmUrl = artist.LastFmUrl,
                Biography = artist.Biography,
                TheAudioDbId = artist.TheAudioDbId,
                Reviews = reviewDtos
            };

            if (artist.ThumbnailPicture != null)
            {
                artistDetailDto.ThumbnailUrl = PictureDataUrlHelper.GeneratePictureDataUrl(artist.ThumbnailPicture);
            }
            else if (artist.ThumbnailUrl != null)
            {
                artistDetailDto.ThumbnailUrl = artist.ThumbnailUrl;
            }

            if (artist.LogoPicture != null)
            {
                artistDetailDto.LogoUrl = PictureDataUrlHelper.GeneratePictureDataUrl(artist.LogoPicture);
            }
            else if (artist.LogoUrl != null)
            {
                artistDetailDto.LogoUrl = artist.LogoUrl;
            }

            if (artist.Style != null)
            {
                artistDetailDto.StyleName = artist.Style.Name;
            }

            if (artist.Genre != null)
            {
                artistDetailDto.GenreName = artist.Genre.Name;
            }

            if (artist.FavorizedByUsers != null)
            {
                if (artist.FavorizedByUsers.Any())
                {
                    artistDetailDto.FavorizedCount = artist.FavorizedByUsers.Count;
                }
            }
            else
            {
                artistDetailDto.FavorizedCount = 0;
            }

            if (!reviewList.Any())
            {
                artistDetailDto.AverageRating = 0;
            }
            else
            {
                double average = (double)ratingSum / (double)reviewList.Count;

                artistDetailDto.AverageRating = (int)Math.Round(average, MidpointRounding.AwayFromZero);
            }

            return artistDetailDto;
        }

        public static ArtistOnUserProfileDto MapToArtistOnUserProfileDto(Artist artist)
        {
            return new ArtistOnUserProfileDto
            {
                Id = artist.Id,
                Name = artist.Name,
                TheAudioDbId = artist.TheAudioDbId,
            };
        }

        #endregion

        #region Review

        public static ReviewOnArtistDetailDto MapToReviewOnArtistDetailDto(Review review)
        {
            return new ReviewOnArtistDetailDto
            {
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                Nickname = review.User.Nickname
            };
        }

        public static FullReviewDto MapToFullReviewDto(Review review, int? userId)
        {
            var dto = new FullReviewDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                CreatedAt = review.CreatedAt,
                ArtistId = review.ArtistId,
            };

            if (review.UserId == null)
            {
                dto.UserId = (int)userId;
            }
            else
            {
                dto.UserId = review.UserId;
            }

            return dto;
        }

        public static ReviewOnUserProfileDto MapToReviewOnUserProfileDto(Review review)
        {
            return new ReviewOnUserProfileDto
            {
                Id = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                ArtistName = review.Artist.Name,
            };
        }

        #endregion

        #region Genre

        public static GenreDto MapToGenreDto(Genre genre)
        {
            return new GenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
            };
        }

        #endregion

        #region Style

        public static StyleDto MapToStyleDto(Style style)
        {
            return new StyleDto
            {
                Id = style.Id,
                Name = style.Name,
            };
        }

        #endregion

        #region Picture

        public static PictureIdDto MapToPictureIdDto(Picture picture)
        {
            return new PictureIdDto
            {
                Id = picture.Id,
            };
        }

        #endregion

        #region Album

        public static AlbumDto MapToAlbumDto(Album album)
        {
            return new AlbumDto
            {
                Name = album.Name,
                ReleaseYear = album.ReleaseYear,
                ImageUrl = album.ImageUrl,
                ArtistId = album.ArtistId,
            };
        }

        #endregion
    }
}
