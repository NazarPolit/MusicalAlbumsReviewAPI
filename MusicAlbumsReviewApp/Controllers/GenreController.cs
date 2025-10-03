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
	public class GenreController: Controller
	{
		private readonly IGenreRepository _genreRepository;
		private readonly IMapper _mapper;

		public GenreController(IGenreRepository genreRepository, IMapper mapper)
        {
			_genreRepository = genreRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
		public async Task<IActionResult> GetGenres()
		{
			var genres = _mapper.Map<List<GenreDto>>(await _genreRepository.GetGenres());

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(genres);
		}

		[HttpGet("{genreId}")]
		[ProducesResponseType(200, Type = typeof(Genre))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetGenre(int genreId)
		{
			if (!_genreRepository.GenreExists(genreId))
				return NotFound(new { message = "Not Found" });

			var genre = _mapper.Map<GenreDto>(await _genreRepository.GetGenre(genreId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(genre);
		}

		[HttpGet("album/{genreId}")]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetAlbumByGenre(int genreId)
		{
			var albums = _mapper.Map<List<AlbumDto>>(
						 await _genreRepository.GetAlbumByGenre(genreId));

			if(!ModelState.IsValid)
				return BadRequest();

			return  Ok(albums);
		}
	}

}
