using BandScope.Common.Models;
using BandScope.DataAccess.Context;
using BandScope.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BandScope.DataAccess
{
    public class ReviewDataAccess : IReviewDataAccess
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public ReviewDataAccess(IDbContextFactory<AppDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public Review CreateReview(Review review)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Reviews.Add(review);
                context.SaveChanges();

                return review;
            }
        }

        public Review GetReviewById(int reviewId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Reviews
                    .Where(r => r.Id == reviewId)
                    .Include(r => r.User)
                    .FirstOrDefault();
            }
        }

        public Review GetReviewByUserAndArtist(int userId, int artistId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var review = context.Reviews
                    .FirstOrDefault(r => r.UserId == userId && r.ArtistId == artistId);

                if (review == null)
                {
                    return new Review();
                }

                return review;
            }
        }

        public List<Review> GetReviewsByUserId(int userId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return context.Reviews
                    .Where(r => r.UserId == userId)
                    .Include(r => r.Artist)
                    .ToList();
            }
        }

        public bool UpdateReview(Review review)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var existingReview = context.Reviews.Find(review.Id);

                if (existingReview == null)
                {
                    return false;
                }

                existingReview.Rating = review.Rating;
                existingReview.Comment = review.Comment;
                existingReview.CreatedAt = review.CreatedAt;

                context.SaveChanges();

                return true;
            }
        }

        public bool DeleteReview(int reviewId)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var reviewToDelete = context.Reviews.Find(reviewId);

                if (reviewToDelete == null)
                {
                    return false;
                }

                context.Reviews.Remove(reviewToDelete);
                context.SaveChanges();

                return true;
            }
        }
    }
}
