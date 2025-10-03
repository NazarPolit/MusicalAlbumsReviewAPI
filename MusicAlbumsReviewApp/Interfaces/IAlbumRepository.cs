using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IAlbumRepository
	{
		Task<ICollection<Album>> GetAlbums();
		Task<Album> GetAlbum(int id);
		Task<Album> GetAlbum(string name);
		Task<decimal> GetAlbumRating(int albmid);
		bool AlbumExists(int albmId);
	}
}
