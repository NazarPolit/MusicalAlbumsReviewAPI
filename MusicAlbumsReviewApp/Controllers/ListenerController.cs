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
	public class ListenerController : Controller
	{
		private readonly IListenerRepository _listenerRepository;
		private readonly IMapper _mapper;

		public ListenerController(IListenerRepository listenerRepository, IMapper mapper)
		{
			_listenerRepository = listenerRepository;
			_mapper = mapper;
		}

		[HttpGet]
		[ProducesResponseType(200, Type = typeof(IEnumerable<Listener>))]
		public async Task<IActionResult> GetListeners()
		{
			var listeners = _mapper.Map<List<ListenerDto>>(await _listenerRepository.GetListeners());

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(listeners);
		}

		[HttpGet("{listenerId}")]
		[ProducesResponseType(200, Type = typeof(Listener))]
		[ProducesResponseType(400)]
		public async Task<IActionResult> GetListener(int listenerId)
		{
			if (!_listenerRepository.ListenerExists(listenerId))
				return NotFound(new { message = "Not Found" });

			var listener = _mapper.Map<ListenerDto>(await _listenerRepository.GetListener(listenerId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(listener);
		}

		[HttpGet("{listenerId}/reviews")]
		public async Task<IActionResult> GetReviewsByListener(int listenerId)
		{
			if (!_listenerRepository.ListenerExists(listenerId))
				return NotFound(new { message = "Not Found" });

			var reviews = _mapper.Map<List<ReviewDto>>(
						  await _listenerRepository.GetReviewsByListener(listenerId));

			if (!ModelState.IsValid)
				return BadRequest();

			return Ok(reviews);
		}
	}
}
