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

		public ReviewController(IReviewRepository reviewRepository, IMapper mapper)
        {
			_reviewRepository = reviewRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Album>))]
		public async Task<IActionResult> GetReviews()
		{
			var reviews = _mapper.Map<List<ReviewDto>>(await _reviewRepository.GetReviews());

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(reviews);
		}

		[HttpGet("{reviewId}")]
		[ProducesResponseType(200, Type = typeof(Album))]
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

		[HttpGet("review/{albumId}")]
		[ProducesResponseType(200, Type = typeof(Album))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetReviewsOfAlbum(int albumId)
		{
			var review = _mapper.Map<ReviewDto>(
						 await _reviewRepository.GetReviewsOfAlbum(albumId));

			if(!ModelState.IsValid) 
				return BadRequest();

			return Ok(review);
		}
	}
}
