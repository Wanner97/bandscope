using BandScope.Common.Models;

namespace BandScope.DataAccess.Interfaces
{
    public interface IReviewDataAccess
    {
        Review CreateReview(Review review);
        Review GetReviewByUserAndArtist(int userId, int artistId);
        Review GetReviewById(int reviewId);
        List<Review> GetReviewsByUserId(int userId);
        bool UpdateReview(Review review);
        bool DeleteReview(int reviewId);
    }
}
