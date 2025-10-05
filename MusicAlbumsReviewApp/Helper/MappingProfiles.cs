using AutoMapper;
using MusicAlbumsReviewApp.Dto;
using MusicAlbumsReviewApp.Models;

namespace MusicAlbumsReviewApp.Helper
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<Album, AlbumDto>();
			CreateMap<AlbumDto, Album>();
			CreateMap<Genre, GenreDto>();
			CreateMap<GenreDto, Genre>();
			CreateMap<Country, CountryDto>();
			CreateMap<CountryDto, Country>();
			CreateMap<ArtistDto, Artist>();
			CreateMap<Artist, ArtistDto>();
			CreateMap<Review, ReviewDto>();
            CreateMap<Listener, ListenerDto>();
		}
    }
}
