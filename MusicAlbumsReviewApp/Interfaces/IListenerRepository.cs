using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IListenerRepository
	{
		Task<ICollection<Listener>> GetListeners();
		Task<Listener> GetListener(int listenerId);
		Task<ICollection<Review>> GetReviewsByListener(int listenerId);
		bool ListenerExists(int listenerId);
	}
}
