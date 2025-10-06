using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IAlbumRepository
	{
		// For GET Methods
		Task<ICollection<Album>> GetAlbums();
		Task<Album> GetAlbum(int id);
		Task<Album> GetAlbum(string name);
		Task<decimal> GetAlbumRating(int albmid);
		bool AlbumExists(int albmId);

		//For POST Methods
		Task<Album?> GetAlbumByTitleAsync(string title);
		Task<bool> CreateAlbum(int artistId, int genreId, Album album);

		//For PUT Methods
		Task<bool> UpdateAlbum(int artistId, int genreId, Album album);

		//For DELETE Method
		Task<bool> DeleteAlbum(Album album);
		Task<bool> Save();
	}
}
