namespace MusicAlbumsReviewApp.Models
{
	public class Artist
	{
        public int Id { get; set; }
		public string Name { get; set; }
		public string BandName { get; set; }
		public Country Country { get; set; }
		public ICollection<AlbumArtist> AlbumArtists { get; set; }
	}
}
