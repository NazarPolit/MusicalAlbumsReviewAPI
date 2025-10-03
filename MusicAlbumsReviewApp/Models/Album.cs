namespace MusicAlbumsReviewApp.Models
{
	public class Album
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime RealeaseDate { get; set; }
		public  ICollection<Review> Reviews { get; set; }
		public ICollection<AlbumArtist> AlbumArtists { get; set; }
		public ICollection<AlbumGenre> AlbumGenres { get; set; }
	}
}
