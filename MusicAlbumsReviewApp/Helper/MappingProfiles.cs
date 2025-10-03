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
            CreateMap<Genre, GenreDto>();
            CreateMap<Country, CountryDto>();
        }
    }
}
