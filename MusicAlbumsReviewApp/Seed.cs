using MusicAlbumsReviewApp.Data;
using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp
{
	public class Seed
	{
		private readonly DataContext dataContext;

		public Seed(DataContext context)
		{
			this.dataContext = context;
		}

		public void SeedDataContext()
		{
			if (!dataContext.AlbumArtists.Any())
			{
				var albumArtists = new List<AlbumArtist>()
				{
					new AlbumArtist()
					{
						Album = new Album()
						{
							Title = "Back in Black",
							RealeaseDate = new DateTime(1980, 7, 25),
							AlbumGenres = new List<AlbumGenre>()
							{
								new AlbumGenre { Genre = new Genre() { Name = "Rock" } }
							},
							Reviews = new List<Review>()
							{
								new Review { Title = "Legendary", Text = "One of the best rock albums ever!", Rating = 5,
											 Listener = new Listener(){ FirstName = "John", LastName = "Doe" } },
								new Review { Title = "Classic", Text = "Timeless songs, pure energy.", Rating = 4,
											 Listener = new Listener(){ FirstName = "Alice", LastName = "Smith" } },
								new Review { Title = "Not my style", Text = "Too loud for me.", Rating = 2,
											 Listener = new Listener(){ FirstName = "Mark", LastName = "Brown" } },
							}
						},
						Artist = new Artist()
						{
							Name = "AC/DC",
							BandName = "AC/DC",
							Country = new Country()
							{
								Name = "Australia"
							}
						}
					},
					new AlbumArtist()
					{
						Album = new Album()
						{
							Title = "Thriller",
							RealeaseDate = new DateTime(1982, 11, 30),
							AlbumGenres = new List<AlbumGenre>()
							{
								new AlbumGenre { Genre = new Genre() { Name = "Pop" } },
								new AlbumGenre { Genre = new Genre() { Name = "Funk" } }
							},
							Reviews = new List<Review>()
							{
								new Review { Title = "Amazing", Text = "Best-selling album for a reason!", Rating = 5,
											 Listener = new Listener(){ FirstName = "Tom", LastName = "Wilson" } },
								new Review { Title = "Iconic", Text = "Billie Jean forever!", Rating = 5,
											 Listener = new Listener(){ FirstName = "Emma", LastName = "Taylor" } },
							}
						},
						Artist = new Artist()
						{
							Name = "Michael Jackson",
							BandName = "Michael Jackson",
							Country = new Country()
							{
								Name = "USA"
							}
						}
					},
					new AlbumArtist()
					{
						Album = new Album()
						{
							Title = "Hybrid Theory",
							RealeaseDate = new DateTime(2000, 10, 24),
							AlbumGenres = new List<AlbumGenre>()
							{
								new AlbumGenre { Genre = new Genre() { Name = "Nu Metal" } }
							},
							Reviews = new List<Review>()
							{
								new Review { Title = "Powerful", Text = "Changed my teenage years!", Rating = 4,
											 Listener = new Listener(){ FirstName = "Sarah", LastName = "Connor" } },
								new Review { Title = "Great", Text = "Linkin Park at its best.", Rating = 4,
											 Listener = new Listener(){ FirstName = "Paul", LastName = "Anderson" } },
							}
						},
						Artist = new Artist()
						{
							Name = "Linkin Park",
							BandName = "Linkin Park",
							Country = new Country()
							{
								Name = "USA"
							}
						}
					}
				};

				dataContext.AlbumArtists.AddRange(albumArtists);
				dataContext.SaveChanges();
			}
		}

	}
}
