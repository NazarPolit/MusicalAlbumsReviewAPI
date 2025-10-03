using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface ICountryRepository
	{
		Task<ICollection<Country>> GetCountries();
		Task<Country> GetCountry(int coutryId);
		Task<Country> GetCountryByArtist(int artistId);
		Task<ICollection<Artist>> GetArtistsFromCountry(int countryId);
		bool CountriesExists(int coutryId);
	}
}
