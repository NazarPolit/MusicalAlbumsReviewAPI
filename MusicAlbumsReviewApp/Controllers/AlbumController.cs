using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicAlbumsReviewApp.Dto;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;
using MusicAlbumsReviewApp.Repository;

namespace MusicAlbumsReviewApp.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AlbumController : Controller
	{
		private readonly IAlbumRepository _albumRepository;
		private readonly IMapper _mapper;

		public AlbumController(IAlbumRepository albumRepository, IMapper mapper)
        {
			_albumRepository = albumRepository;
			_mapper = mapper;
		}

		//GET Methods
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
		public async Task<IActionResult> GetAlbums()
		{
			var albums = _mapper.Map<List<AlbumDto>>(await _albumRepository.GetAlbums());

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(albums);
		}

		[HttpGet("{albmId}")]
		[ProducesResponseType(200, Type = typeof(Album))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetAlbum(int albmId)
		{
			if(!_albumRepository.AlbumExists(albmId))
				return NotFound(new { message = "Not Found" });

			var album =  _mapper.Map<AlbumDto>(await _albumRepository.GetAlbum(albmId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(album);
		}

		[HttpGet("{albmId}/rating")]
		[ProducesResponseType(200, Type = typeof(decimal))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetAlbumRating(int albmId)
		{
			if (!_albumRepository.AlbumExists(albmId))
				return NotFound(new { message = "Not Found" });

			var rating = await _albumRepository.GetAlbumRating(albmId);

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(rating);
		}

		//POST Methods
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateAlbum([FromQuery] int artistId, [FromQuery] int genreId, [FromBody] AlbumDto albumCreate)
		{
			if (albumCreate == null)
				return BadRequest(ModelState);

			var albums = await _albumRepository.GetAlbumByTitleAsync(albumCreate.Title);

			if (albums != null)
			{
				ModelState.AddModelError("", "Album already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var albumMap = _mapper.Map<Album>(albumCreate);

			if (!await _albumRepository.CreateAlbum(artistId, genreId, albumMap))
			{
				ModelState.AddModelError("", "Something went wrong while saving");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created");
		}
	}
}
