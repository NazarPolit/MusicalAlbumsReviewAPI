using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IGenreRepository
	{
		Task<ICollection<Genre>> GetGenres();
		Task<Genre> GetGenre(int genreId);
		Task<ICollection<Album>> GetAlbumByGenre(int genreId);
		bool GenreExists(int id);
	}
}
