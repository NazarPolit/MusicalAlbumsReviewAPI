using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IReviewRepository
	{
		//For GET Methods
		Task<ICollection<Review>> GetReviews();
		Task<Review> GetReview(int reviewId);
		Task<ICollection<Review>> GetReviewsOfAlbum(int albumId);
		bool ReviewExists(int reviewId);

		//For POST Methods
		Task<bool> CreateReview(Review review);
		Task<Review?> GetReviewByTitleAsync(string title);

		//For PUT Methods
		Task<bool> UpdateReview(Review review);

		//For DELETE Method
		Task<bool> DeleteReview(Review review);

		//For Album DELETE Method
		Task<bool> DeleteReviews(List<Review> reviews);

		Task<bool> Save();
	}
}
