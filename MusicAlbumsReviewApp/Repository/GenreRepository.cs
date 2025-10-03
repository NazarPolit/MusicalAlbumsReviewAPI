using Microsoft.EntityFrameworkCore;
using MusicAlbumsReviewApp.Data;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Repository
{
	public class GenreRepository : IGenreRepository
	{
		private readonly DataContext _context;

		public GenreRepository(DataContext context)
        {
			_context = context;
		}
        public bool GenreExists(int id)
		{
			return  _context.Genres.Any(g => g.Id == id);
		}

		public async Task<ICollection<Album>> GetAlbumByGenre(int genreId)
		{
			return await _context.AlbumGenres.Where(ag => ag.GenreId == genreId).Select(g => g.Album).ToListAsync();
		}

		public async Task<Genre> GetGenre(int genreId)
		{
			return await _context.Genres.FindAsync(genreId);
		}

		public async Task<ICollection<Genre>> GetGenres()
		{
			return await _context.Genres.ToListAsync();
		}
	}
}
