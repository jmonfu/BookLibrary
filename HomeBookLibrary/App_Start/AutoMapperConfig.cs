using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapper.Mapper.CreateMap<Author, AuthorDTO>()
                    .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => src.Name));

            AutoMapper.Mapper.CreateMap<Genre, GenreDTO>();

            AutoMapper.Mapper.CreateMap<Book, BookDTO>()
                    .ForMember(dest => dest.BookTitle,
                    opts => opts.MapFrom(src => src.Title));
            AutoMapper.Mapper.CreateMap<Book, BookDTO>()
                .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => src.Author.Name));

            AutoMapper.Mapper.CreateMap<Book, BookDetailDTO>()
                    .ForMember(dest => dest.BookTitle,
                    opts => opts.MapFrom(src => src.Title));
            AutoMapper.Mapper.CreateMap<Book, BookDetailDTO>()
                .ForMember(dest => dest.AuthorName,
                    opts => opts.MapFrom(src => src.Author.Name));
            AutoMapper.Mapper.CreateMap<Book, BookDetailDTO>()
                .ForMember(dest => dest.GenreType,
                    opts => opts.MapFrom(src => src.Genre.Type));

            AutoMapper.Mapper.CreateMap<Loan, LoanDTO>();

            AutoMapper.Mapper.CreateMap<Loan, LoanDetailsDTO>()
                .ForMember(dest => dest.BookTitle,
                    opts => opts.MapFrom(src => src.Book.Title));
        }
    }

}