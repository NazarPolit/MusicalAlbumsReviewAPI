using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicAlbumsReviewApp.Data;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;
using System.Diagnostics.Metrics;

namespace MusicAlbumsReviewApp.Repository
{
	public class ArtistRepository: IArtistRepository
	{
		private readonly DataContext _context;

		public ArtistRepository(DataContext context)
        {
			_context = context;
		}

		//GET Methods
		public bool ArtistExists(int artistId)
		{
			return _context.Artists.Any(a => a.Id == artistId);
		}

		public async Task<ICollection<Album>> GetAlbumByArtist(int artistId)
		{
			return await _context.AlbumArtists.Where(a => a.Artist.Id == artistId).Select(a => a.Album).ToListAsync();
		}

		public async Task<Artist> GetArtist(int id)
		{
			return await _context.Artists.FindAsync(id);
		}

		public async Task<ICollection<Artist>> GetArtistOfAlbum(int albmId)
		{
			return await _context.AlbumArtists.Where(a => a.Album.Id == albmId).Select(a => a.Artist).ToListAsync();
		}

		public async Task<ICollection<Artist>> GetArtists()
		{
			return await _context.Artists.ToListAsync();
		}

		//POST Methods
		public async Task<Artist?> GetArtistByNameAsync(string name)
		{
			return await _context.Artists.
				Where(a => a.Name.Trim().ToUpper() == name.Trim().ToUpper())
				.FirstOrDefaultAsync();
		}

		public async Task<bool> Save()
		{
			var saved = await _context.SaveChangesAsync();
			return saved > 0 ? true : false;
		}

		public async Task<bool> CreateArtist(Artist artist)
		{
			await _context.AddAsync(artist);
			return await Save();
		}
	}
}
