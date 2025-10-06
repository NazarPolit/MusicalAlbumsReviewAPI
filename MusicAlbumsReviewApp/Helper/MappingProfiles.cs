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
			CreateMap<AlbumDto, Album>().ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<Genre, GenreDto>();
			CreateMap<GenreDto, Genre>().ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<Country, CountryDto>();
			CreateMap<CountryDto, Country>().ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<ArtistDto, Artist>().ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<Artist, ArtistDto>();
			CreateMap<Review, ReviewDto>();
			CreateMap<ReviewDto, Review>().ForMember(dest => dest.Id, opt => opt.Ignore());
			CreateMap<Listener, ListenerDto>();
			CreateMap<ListenerDto, Listener>().ForMember(dest => dest.Id, opt => opt.Ignore());
		}
    }
}
