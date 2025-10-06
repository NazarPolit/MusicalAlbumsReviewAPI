using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IGenreRepository
	{
		//For GET Methods
		Task<ICollection<Genre>> GetGenres();
		Task<Genre> GetGenre(int genreId);
		Task<ICollection<Album>> GetAlbumByGenre(int genreId);
		bool GenreExists(int id);
		
		//For POST Method
		Task<Genre?> GetGenreByNameAsync(string name);
		Task<bool> CreateGenre(Genre genre);

		//For PUT Method
		Task<bool> UpdateGenre(Genre genre);

		//For DELETE Method
		Task<bool> DeleteGenre(Genre genre); 

		Task<bool> Save();
	}
}
