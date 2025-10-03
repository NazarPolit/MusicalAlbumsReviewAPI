using Microsoft.EntityFrameworkCore;
using MusicAlbumsReviewApp.Data;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Repository
{
	public class AlbumRepository : IAlbumRepository
	{
		private readonly DataContext _context;


		public AlbumRepository(DataContext context)
        {
			_context = context;
		}

		public async Task<ICollection<Album>> GetAlbums()
		{
			return await _context.Albums.OrderBy(a => a.Id).ToListAsync();
		}

		public async Task<Album> GetAlbum(int id)
		{
			return await _context.Albums.FindAsync(id);
		}

		public async Task<Album> GetAlbum(string name)
		{
			return await _context.Albums.FindAsync(name);
		}

		public async Task<decimal> GetAlbumRating(int albmId)
		{
			var review = _context.Reviews.Where(a => a.Album.Id == albmId);

			if(!await review.AnyAsync())
				return 0;

			return await review.AverageAsync(r => (decimal)r.Rating);
		}

		public bool AlbumExists(int albmId)
		{
			return  _context.Albums.Any(a => a.Id == albmId);
		}


    }
}
