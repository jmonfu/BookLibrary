using AutoMapper;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary
{
    public class GenreMappings : Profile
    {
        public override string ProfileName => "GenreMappings";

        protected override void Configure()
        {
            CreateMap<Genre, GenreDTO>();
        }
    }
}