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
	public class GenreController : Controller
	{
		private readonly IGenreRepository _genreRepository;
		private readonly IMapper _mapper;

		public GenreController(IGenreRepository genreRepository, IMapper mapper)
		{
			_genreRepository = genreRepository;
			_mapper = mapper;
		}

		// GET Methods
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Genre>))]
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

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(albums);
		}

		//POST Method
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateGenre([FromBody] GenreDto genreCreate)
		{
			if (genreCreate == null)
				return BadRequest(ModelState);

			var geners = await _genreRepository.GetGenreByNameAsync(genreCreate.Name);

			if (geners != null)
			{
				ModelState.AddModelError("", "Genre already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var genreMap = _mapper.Map<Genre>(genreCreate);

			if (!await _genreRepository.CreateGenre(genreMap))
			{
				ModelState.AddModelError("", "Something went wrong while saving");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created");
		}

		//PUT Method
		[HttpPut("{genreId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> UpdateGenre(int genreId, [FromBody] GenreDto updatedGenre)
		{
			if (updatedGenre == null)
				return BadRequest(ModelState);

			if (!_genreRepository.GenreExists(genreId))
				return NotFound(new { message = "Not Found" });

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var genreMap = _mapper.Map<Genre>(updatedGenre);
			genreMap.Id = genreId;

			if (!await _genreRepository.UpdateGenre(genreMap))
			{
				ModelState.AddModelError("", "Something went wrong updating genre");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}

		[HttpDelete("{genreId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteGenre(int genreId)
		{
			if (!_genreRepository.GenreExists(genreId))
				return NotFound();

			var genreToDelete = await _genreRepository.GetGenre(genreId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _genreRepository.DeleteGenre(genreToDelete))
				ModelState.AddModelError("", "Something wrong with deleting genre");

			return NoContent();
		}
	} 

}
