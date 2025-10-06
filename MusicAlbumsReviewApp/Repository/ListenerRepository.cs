using Microsoft.EntityFrameworkCore;
using MusicAlbumsReviewApp.Data;
using MusicAlbumsReviewApp.Interfaces;
using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Repository
{
	public class ListenerRepository : IListenerRepository
	{
		private readonly DataContext _context;

		public ListenerRepository(DataContext context)
        {
			_context = context;
		}

		//GET Methods
		public async Task<Listener> GetListener(int listenerId)
		{
			return await _context.Listeners.Where(l => l.Id == listenerId).Include(e => e.Reviews).FirstOrDefaultAsync();
		}

		public async Task<ICollection<Listener>> GetListeners()
		{
			return await _context.Listeners.ToListAsync();
		}

		public async Task<ICollection<Review>> GetReviewsByListener(int listenerId)
		{
			return await _context.Reviews.Where(l => l.Listener.Id == listenerId).ToListAsync();
		}

		public bool ListenerExists(int listenerId)
		{
			return _context.Listeners.Any(l => l.Id == listenerId);
		}

		//POST Mehods
		public async Task<bool> Save()
		{
			var saved = await _context.SaveChangesAsync();
			return saved > 0 ? true : false;
		}
		public async Task<bool> CreateListener(Listener listener)
		{
			await _context.AddAsync(listener);
			return await Save();
		}

		public async Task<Listener?> GetListenerByLastNameAsync(string lastName)
		{
			return await _context.Listeners.
				Where(r => r.LastName.Trim().ToUpper() == lastName.Trim().ToUpper())
				.FirstOrDefaultAsync();
		}

		//PUT Method
		public async Task<bool> UpdateListener(Listener listener)
		{
			_context.Update(listener);
			return await Save();
		}

		//DELETE Method
		public async Task<bool> DeleteListener(Listener listener)
		{
			_context.Remove(listener);
			return await Save();
		}
	}
}
