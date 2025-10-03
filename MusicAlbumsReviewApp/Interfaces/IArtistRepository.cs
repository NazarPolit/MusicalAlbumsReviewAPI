using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IArtistRepository
	{
		Task<ICollection<Artist>> GetArtists();
		Task<Artist> GetArtist(int id);
		Task<ICollection<Artist>> GetArtistOfAlbum(int albmId);
		Task<ICollection<Album>> GetAlbumByArtist(int artistId);
		bool ArtistExists(int artistId);
	}
}
