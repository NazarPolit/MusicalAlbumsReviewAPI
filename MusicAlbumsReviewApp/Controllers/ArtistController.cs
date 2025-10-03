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
		private readonly IMapper _mapper;

		public ArtistController(IArtistRepository artistRepository, IAlbumRepository albumRepository, IMapper mapper)
        {
			_artistRepository = artistRepository;
			_mapper = mapper;
			_albumRepository = albumRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
		public async Task<IActionResult> GetArtists()
		{
			var artists = _mapper.Map<List<ArtistDto>>(await _artistRepository.GetArtists());

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(artists);
		}

		[HttpGet("{artistId}")]
		[ProducesResponseType(200, Type = typeof(Album))]
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

		[HttpGet("{albumId}/artist")]
		[ProducesResponseType(200, Type = typeof(Album))]
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
	}
}
