using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicAlbumsReviewApp.Data;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Repository
{
	public class ArtistRepository: IArtistRepository
	{
		private readonly DataContext _context;

		public ArtistRepository(DataContext context)
        {
			_context = context;
		}

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
	}
}
