using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicAlbumsReviewApp.Data;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Repository
{
	public class CountryRepository : ICountryRepository
	{
		private readonly DataContext _context;
		private readonly IMapper _mapper;

		//GET Mothods
		public CountryRepository(DataContext context, IMapper mapper) 
		{
			_context = context;
			_mapper = mapper;
		}
		public bool CountriesExists(int coutryId)
		{
			return _context.Countries.Any(c => c.Id == coutryId);
		}

		public async Task<ICollection<Artist>> GetArtistsFromCountry(int countryId)
		{
			return await _context.Artists.Where(c => c.Country.Id == countryId).ToListAsync();
		}

		public async Task<ICollection<Country>> GetCountries()
		{
			return await _context.Countries.ToListAsync();
		}

		public async Task<Country> GetCountry(int coutryId)
		{
			return await _context.Countries.FindAsync(coutryId);
		}

		public async Task<Country> GetCountryByArtist(int artistId)
		{
			return await _context.Artists.Where(a =>  a.Id == artistId).Select(c => c.Country).FirstOrDefaultAsync();
		}

		//POST Methods
		public async Task<bool> CreateCountry(Country country)
		{
			await _context.AddAsync(country);
			return await Save();
		}

		public async Task<Country?> GetCountryByNameAsync(string name)
		{
			return await _context.Countries.
				Where(c => c.Name.Trim().ToUpper() == name.Trim().ToUpper())
				.FirstOrDefaultAsync();
		}

		public async Task<bool> Save()
		{
			var saved = await _context.SaveChangesAsync();
			return saved > 0 ? true : false; 
		}
	}
}
