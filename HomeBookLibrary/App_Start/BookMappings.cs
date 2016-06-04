using AutoMapper;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary
{
    public class BookMappings : Profile
    {
        public override string ProfileName => "BookMappings";

        protected override void Configure()
        {
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.BookTitle,
                    opts => opts.MapFrom(src => src.Title));
            CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => src.Author.Name));

            CreateMap<Book, BookDetailDTO>()
                .ForMember(dest => dest.BookTitle,
                    opts => opts.MapFrom(src => src.Title));
            CreateMap<Book, BookDetailDTO>()
                .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => src.Author.Name));
            CreateMap<Book, BookDetailDTO>()
                .ForMember(dest => dest.GenreType,
                    opts => opts.MapFrom(src => src.Genre.Type));
        }
    }
}