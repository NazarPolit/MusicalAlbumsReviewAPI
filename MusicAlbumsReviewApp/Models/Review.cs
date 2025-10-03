namespace MusicAlbumsReviewApp.Models
{
	public class Review
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public int Rating { get; set; }
		public Listener Listener { get; set; }
		public Album Album { get; set; }
	}
}
