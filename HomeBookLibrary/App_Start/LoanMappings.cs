using AutoMapper;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary
{
    public class LoanMappings : Profile
    {
        public override string ProfileName => "LoanMappings";

        protected override void Configure()
        {
            CreateMap<Loan, LoanDTO>();

            CreateMap<Loan, LoanDetailsDTO>()
                .ForMember(dest => dest.BookTitle,
                    opts => opts.MapFrom(src => src.Book.Title));
        }
    }
}