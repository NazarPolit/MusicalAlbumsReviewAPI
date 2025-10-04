using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Dto
{
	public class ListenerDto
	{
		public int Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<ReviewDto> Reviews { get; set; }
	}
}
