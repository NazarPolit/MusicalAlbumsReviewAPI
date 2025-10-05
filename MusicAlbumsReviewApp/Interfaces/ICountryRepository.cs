using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface ICountryRepository
	{
		//For GET Mothods
		Task<ICollection<Country>> GetCountries();
		Task<Country> GetCountry(int coutryId);
		Task<Country> GetCountryByArtist(int artistId);
		Task<ICollection<Artist>> GetArtistsFromCountry(int countryId);
		bool CountriesExists(int coutryId);

		//For POST Methods
		Task<Country?> GetCountryByNameAsync(string name);
		Task<bool> CreateCountry(Country country);
		Task<bool> Save();
	}
}
