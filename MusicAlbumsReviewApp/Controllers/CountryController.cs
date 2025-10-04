using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicAlbumsReviewApp.Dto;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;
using MusicAlbumsReviewApp.Repository;

namespace MusicAlbumsReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CountryController : Controller
	{
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

		public CountryController(ICountryRepository countryRepository, IMapper mapper) 
		{
			_countryRepository = countryRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]
		public async Task<IActionResult> GetCountries()
		{
			var countries = _mapper.Map<List<CountryDto>>(await _countryRepository.GetCountries());

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(countries);
		}

		[HttpGet("{countryId}")]
		[ProducesResponseType(200, Type = typeof(Country))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetCountry(int countryId)
		{
			if (!_countryRepository.CountriesExists(countryId))
				return NotFound(new { message = "Not Found" });

			var country = _mapper.Map<CountryDto>(await _countryRepository.GetCountry(countryId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(country);
		}

		[HttpGet("artist/{artistId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(Country))]
		public async Task<IActionResult> GetCountryOfAnArtist(int artistId)
		{
			var country = _mapper.Map<CountryDto>(
				await _countryRepository.GetCountryByArtist(artistId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(country);
		}
	}
}
