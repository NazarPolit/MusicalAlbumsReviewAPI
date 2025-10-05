using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IArtistRepository
	{
		//For GET Mothods
		Task<ICollection<Artist>> GetArtists();
		Task<Artist> GetArtist(int id);
		Task<ICollection<Artist>> GetArtistOfAlbum(int albmId);
		Task<ICollection<Album>> GetAlbumByArtist(int artistId);
		bool ArtistExists(int artistId);

		//For POST Methods
		Task<Artist?> GetArtistByNameAsync(string name);
		Task<bool> CreateArtist(Artist artist);
		Task<bool> Save();
	}
}
