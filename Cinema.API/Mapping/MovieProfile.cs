using AutoMapper;
using Cinema.API.Dtos;
using Cinema.API.Models;

namespace Cinema.API.Mapping
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieDto, Movie>().ReverseMap();
        }
    }
}