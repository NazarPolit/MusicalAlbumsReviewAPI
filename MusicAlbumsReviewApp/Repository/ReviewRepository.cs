using Microsoft.EntityFrameworkCore;
using MusicAlbumsReviewApp.Data;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Repository
{
	public class ReviewRepository:IReviewRepository
	{
		private readonly DataContext _context;

		public ReviewRepository(DataContext context)
        {
			_context = context;
		}

		//GET Methods
		public async Task<Review> GetReview(int reviewId)
		{
			return await _context.Reviews.FindAsync(reviewId);
		}

		public async Task<ICollection<Review>> GetReviews()
		{
			return await _context.Reviews.ToListAsync();
		}

		public async Task<ICollection<Review>> GetReviewsOfAlbum(int albumId)
		{
			return await _context.Reviews.Where(r => r.Album.Id == albumId).ToListAsync();
		}

		public bool ReviewExists(int reviewId)
		{
			return _context.Reviews.Any(r => r.Id == reviewId);
		}

		//POST Methods
		public async Task<bool> CreateReview(Review review)
		{
			await _context.AddAsync(review);
			return await Save();
		}

		public async Task<bool> Save()
		{
			var saved = await _context.SaveChangesAsync();
			return saved > 0 ? true : false;
		}

		public async Task<Review?> GetReviewByTitleAsync(string title)
		{
			return await _context.Reviews.
				Where(r => r.Title.Trim().ToUpper() == title.Trim().ToUpper())
				.FirstOrDefaultAsync();
		}

		//PUT Methods
		public async Task<bool> UpdateReview(Review review)
		{
			_context.Update(review);
			return await Save();
		}

		//DELETE Method
		public async Task<bool> DeleteReview(Review review)
		{
			_context.Remove(review);
			return await Save();
		}

		//For Album DELETE Method
		public async Task<bool> DeleteReviews(List<Review> reviews)
		{
			_context.RemoveRange(reviews);
			return await Save();
		}
	}
}
