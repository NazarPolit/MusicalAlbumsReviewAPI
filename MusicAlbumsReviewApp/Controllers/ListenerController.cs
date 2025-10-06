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

		//POST Method
		[HttpPost]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		public async Task<IActionResult> CreateListener([FromBody] ListenerDto listenerCreate)
		{
			if (listenerCreate == null)
				return BadRequest(ModelState);

			var listeners = await _listenerRepository.GetListenerByLastNameAsync(listenerCreate.LastName);

			if (listeners != null)
			{
				ModelState.AddModelError("", "Listener already exists");
				return StatusCode(422, ModelState);
			}

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var listenerMap = _mapper.Map<Listener>(listenerCreate);

			if (!await _listenerRepository.CreateListener(listenerMap))
			{
				ModelState.AddModelError("", "Something went wrong while saving");
				return StatusCode(500, ModelState);
			}

			return Ok("Successfully created");
		}

		//PUT Method
		[HttpPut("{listenerId}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> UpdateListener(int listenerId, [FromBody] ListenerDto updatedListener)
		{
			if (updatedListener == null)
				return BadRequest(ModelState);

			if (!_listenerRepository.ListenerExists(listenerId))
				return NotFound(new { message = "Not Found" });

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var genreMap = _mapper.Map<Listener>(updatedListener);
			genreMap.Id = listenerId;

			if (!await _listenerRepository.UpdateListener(genreMap))
			{
				ModelState.AddModelError("", "Something went wrong updating review");
				return StatusCode(500, ModelState);
			}

			return NoContent();
		}

		//DELETE Method
		[HttpDelete("{listenerId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<IActionResult> DeleteListener(int listenerId)
		{
			if (!_listenerRepository.ListenerExists(listenerId))
				return NotFound();

			var reviewToDelete = await _listenerRepository.GetListener(listenerId);

			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			if (!await _listenerRepository.DeleteListener(reviewToDelete))
				ModelState.AddModelError("", "Something wrong with deleting listener");

			return NoContent();
		}
	}
}
