using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicAlbumsReviewApp.Data;
using MusicAlbumsReviewApp.Dto;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;
using MusicAlbumsReviewApp.Repository;

namespace MusicAlbumsReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArtistController : Controller
	{
		private readonly IArtistRepository _artistRepository;
		private readonly IAlbumRepository _albumRepository;
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

		public ArtistController(IArtistRepository artistRepository, IAlbumRepository albumRepository, ICountryRepository countryRepository, IMapper mapper)
        {
			_artistRepository = artistRepository;
			_mapper = mapper;
			_albumRepository = albumRepository;
			_countryRepository = countryRepository;
		}

		//GET Methods
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Artist>))]
		public async Task<IActionResult> GetArtists()
		{
			var artists = _mapper.Map<List<ArtistDto>>(await _artistRepository.GetArtists());

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(artists);
		}

		[HttpGet("{artistId}")]
		[ProducesResponseType(200, Type = typeof(Artist))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetArtist(int artistId)
		{
			if (!_artistRepository.ArtistExists(artistId))
				return NotFound(new { message = "Not Found" });

			var artist = _mapper.Map<ArtistDto>(await _artistRepository.GetArtist(artistId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(artist);
		}

		[HttpGet("{artistId}/album")]
		[ProducesResponseType(200, Type = typeof(Album))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetAlbumByArtist(int artistId)
		{
			if (!_artistRepository.ArtistExists(artistId))
				return NotFound(new { message = $"Not found" });

			var artist = _mapper.Map<List<AlbumDto>>(
						 await _artistRepository.GetAlbumByArtist(artistId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(artist);
		}

		[HttpGet("artist/{albumId}")]
		[ProducesResponseType(200, Type = typeof(Artist))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetArtistOfAlbum(int albumId)
		{
			if (!_albumRepository.AlbumExists(albumId))
				return NotFound(new { message = $"Not found" });

			var artist = _mapper.Map<List<ArtistDto>>(
						 await _artistRepository.GetArtistOfAlbum(albumId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(artist);
		}

		//POST Method
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateArtist([FromQuery] int countryId, [FromBody] ArtistDto artistCreate)
		{
			if (artistCreate == null)
				return BadRequest(ModelState);

			var artists = await _artistRepository.GetArtistByNameAsync(artistCreate.Name);

			if (artists != null)
			{
				ModelState.AddModelError("", "Artist already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var artistMap = _mapper.Map<Artist>(artistCreate);

			artistMap.Country = await _countryRepository.GetCountry(countryId);

			if (!await _artistRepository.CreateArtist(artistMap))
			{
				ModelState.AddModelError("", "Something went wrong while saving");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created");
		}
	}
}
