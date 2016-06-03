using AutoMapper;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Author, AuthorDTO>()
                .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => src.Name));

            Mapper.CreateMap<Genre, GenreDTO>();

            Mapper.CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.BookTitle,
                    opts => opts.MapFrom(src => src.Title));
            Mapper.CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => src.Author.Name));

            Mapper.CreateMap<Book, BookDetailDTO>()
                .ForMember(dest => dest.BookTitle,
                    opts => opts.MapFrom(src => src.Title));
            Mapper.CreateMap<Book, BookDetailDTO>()
                .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => src.Author.Name));
            Mapper.CreateMap<Book, BookDetailDTO>()
                .ForMember(dest => dest.GenreType,
                    opts => opts.MapFrom(src => src.Genre.Type));

            Mapper.CreateMap<Loan, LoanDTO>();

            Mapper.CreateMap<Loan, LoanDetailsDTO>()
                .ForMember(dest => dest.BookTitle,
                    opts => opts.MapFrom(src => src.Book.Title));
        }
    }
}