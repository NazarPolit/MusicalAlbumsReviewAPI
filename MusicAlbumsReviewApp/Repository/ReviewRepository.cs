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
	}
}
