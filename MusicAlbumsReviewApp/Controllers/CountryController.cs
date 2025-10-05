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

		//GET Methods
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

		//POST Method
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateCountry([FromBody] CountryDto countryCreate)
		{
			if (countryCreate == null)
				return BadRequest(ModelState);

			var countries = await _countryRepository.GetCountryByNameAsync(countryCreate.Name);

			if (countries != null)
			{
				ModelState.AddModelError("", "Country already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var countryMap = _mapper.Map<Country>(countryCreate);

			if (!await _countryRepository.CreateCountry(countryMap))
			{
				ModelState.AddModelError("", "Something went wrong while saving");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created");
		}
	}
}
