using BandScope.Common.Models;
using BandScope.DataAccess.Interfaces;
using BandScope.Logic.Interfaces;
using BandScope.Logic.Validators;
using FluentValidation;

namespace BandScope.Logic
{
    public class ReviewLogic : IReviewLogic
    {
        private readonly IReviewDataAccess _reviewDataAccess;

        public ReviewLogic(IReviewDataAccess reviewDataAccess)
        {
            _reviewDataAccess = reviewDataAccess;
        }

        public Review CreateReview(Review review)
        {
            new ReviewValidator(false).ValidateAndThrow(review);

            return _reviewDataAccess.CreateReview(review);
        }

        public Review GetReviewById(int reviewId)
        {
            if (reviewId <= 0)
            {
                throw new ValidationException("reviewId must be greater than 0.");
            }

            return _reviewDataAccess.GetReviewById(reviewId);
        }

        public Review GetReviewByUserAndArtist(int userId, int artistId)
        {
            if (userId <= 0 || artistId <= 0)
            {
                throw new ValidationException("userId and artistId must be greater than 0.");
            }

            return _reviewDataAccess.GetReviewByUserAndArtist(userId, artistId);
        }

        public List<Review> GetReviewsByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ValidationException("userId must be greater than 0.");
            }

            return _reviewDataAccess.GetReviewsByUserId(userId);
        }

        public bool UpdateReview(Review review)
        {
            new ReviewValidator(true).ValidateAndThrow(review);

            return _reviewDataAccess.UpdateReview(review);
        }

        public bool DeleteReview(int reviewId)
        {
            if (reviewId <= 0)
            {
                throw new ValidationException("reviewId must be greater than 0.");
            }

            return _reviewDataAccess.DeleteReview(reviewId);
        }
    }
}
