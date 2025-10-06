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
	public class ReviewController: Controller
	{
		private readonly IReviewRepository _reviewRepository;
		private readonly IMapper _mapper;
		private readonly IAlbumRepository _albumRepository;
		private readonly IListenerRepository _listenerRepository;

		public ReviewController(IReviewRepository reviewRepository, IMapper mapper, IAlbumRepository albumRepository, IListenerRepository listenerRepository)
        {
			_reviewRepository = reviewRepository;
			_mapper = mapper;
			_albumRepository = albumRepository;
			_listenerRepository = listenerRepository;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Review>))]
		public async Task<IActionResult> GetReviews()
		{
			var reviews = _mapper.Map<List<ReviewDto>>(await _reviewRepository.GetReviews());

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(reviews);
		}

		[HttpGet("{reviewId}")]
		[ProducesResponseType(200, Type = typeof(Review))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetReview(int reviewId)
		{
			if (!_reviewRepository.ReviewExists(reviewId))
				return NotFound(new { message = "Not Found" });

			var review = _mapper.Map<ReviewDto>(await _reviewRepository.GetReview(reviewId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(review);
		}

		[HttpGet("album/{albumId}")]
		[ProducesResponseType(200, Type = typeof(Album))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetReviewsOfAlbum(int albumId)
		{
			var review = _mapper.Map<List<ReviewDto>>(
						 await _reviewRepository.GetReviewsOfAlbum(albumId));

			if(!ModelState.IsValid) 
				return BadRequest();

			return Ok(review);
		}

		//POST Methods
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateReview([FromQuery] int listenerId, [FromQuery] int albumId, [FromBody] ReviewDto reviewCreate)
		{
			if (reviewCreate == null)
				return BadRequest(ModelState);

			var reviews = await _reviewRepository.GetReviewByTitleAsync(reviewCreate.Title);

			if (reviews != null)
			{
				ModelState.AddModelError("", "Review already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var reviewMap = _mapper.Map<Review>(reviewCreate);

			reviewMap.Album = await _albumRepository.GetAlbum(albumId);
			reviewMap.Listener = await _listenerRepository.GetListener(listenerId);

			if (!await _reviewRepository.CreateReview(reviewMap))
			{
				ModelState.AddModelError("", "Something went wrong while saving");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created");
		}

		//PUT Method
		[HttpPut("{reviewId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> UpdateListener(int reviewId, [FromBody] ReviewDto updatedReview)
		{
			if (updatedReview == null)
				return BadRequest(ModelState);

			if (!_reviewRepository.ReviewExists(reviewId))
				return NotFound(new { message = "Not Found" });

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var reviewMap = _mapper.Map<Review>(updatedReview);
			reviewMap.Id = reviewId;

			if (!await _reviewRepository.UpdateReview(reviewMap))
			{
				ModelState.AddModelError("", "Something went wrong updating review");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}

		//DELETE Method
		[HttpDelete("{reviewId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteReview(int reviewId)
		{
			if (!_reviewRepository.ReviewExists(reviewId))
				return NotFound();

			var reviewToDelete = await _reviewRepository.GetReview(reviewId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _reviewRepository.DeleteReview(reviewToDelete))
				ModelState.AddModelError("", "Something wrong with deleting review");

			return NoContent();
		}

	}
}
