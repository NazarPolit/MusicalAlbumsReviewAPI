using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IReviewRepository
	{
		Task<ICollection<Review>> GetReviews();
		Task<Review> GetReview(int reviewId);
		Task<ICollection<Review>> GetReviewsOfAlbum(int albumId);
		bool ReviewExists(int reviewId);
	}
}
