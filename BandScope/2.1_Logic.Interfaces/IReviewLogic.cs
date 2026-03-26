using BandScope.Common.Models;

namespace BandScope.Logic.Interfaces
{
    public interface IReviewLogic
    {
        Review CreateReview(Review review);
        Review GetReviewById(int reviewId);
        Review GetReviewByUserAndArtist(int userId, int artistId);
        List<Review> GetReviewsByUserId(int userId);
        bool UpdateReview(Review review);
        bool DeleteReview(int reviewId);
    }
}
