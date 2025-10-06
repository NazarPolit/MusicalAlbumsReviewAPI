using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Interfaces
{
	public interface IListenerRepository
	{
		//For GET Methods
		Task<ICollection<Listener>> GetListeners();
		Task<Listener> GetListener(int listenerId);
		Task<ICollection<Review>> GetReviewsByListener(int listenerId);
		bool ListenerExists(int listenerId);

		//For POST Methods
		Task<bool> CreateListener(Listener listener);

		//For PUT Methods
		Task<bool> UpdateListener(Listener listener);

		Task<bool> Save();
		Task<Listener?> GetListenerByLastNameAsync(string lastName);

		//For DELETE Method
		Task<bool> DeleteListener(Listener listener);

	}
}
