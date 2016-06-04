using AutoMapper;
using HomeBookLibrary.Models;
using HomeBookLibrary.Models.DTO;

namespace HomeBookLibrary.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<AuthorMappings>();
                x.AddProfile<BookMappings>();
                x.AddProfile<GenreMappings>();
                x.AddProfile<LoanMappings>();
            });
        }


    }
}