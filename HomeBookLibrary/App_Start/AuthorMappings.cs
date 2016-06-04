using AutoMapper;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary
{
    public class AuthorMappings : Profile
    {
        public override string ProfileName => "AuthorMappings";

        protected override void Configure()
        {
            CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => src.Name));
        }
    }
}