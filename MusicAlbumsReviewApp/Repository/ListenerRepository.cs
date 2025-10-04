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
	}
}
