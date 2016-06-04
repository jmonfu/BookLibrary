using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HomeBookLibrary.App_Start;
using HomeBookLibrary.Controllers;
using HomeBookLibrary.DAL;
using Moq;
using NUnit.Framework;
using HomeBookLibrary.Models;

namespace HomeBookLibrary.Tests
{
    [TestFixture]
    public class BooksControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        BooksController objController;
        List<Book> books = new List<Book>();

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            objController = new BooksController(_unitOfWorkMock.Object);
            books = new List<Book>()
            {
                new Book {Id = 1, Title = "Hamlet", ISBN = 1853260096, AuthorId = 1, GenreId = 1, IsAvailable = true, Summary = "Hamlet is not only one of Shakespeare's greatest plays, but also the most fascinatingly problematical tragedy. "},
                new Book {Id = 2, Title = "Macbeth", ISBN = 1853260355, AuthorId = 1, GenreId = 1, IsAvailable = true, Summary = "Shakespeare’s Macbeth is one of the greatest tragic dramas the world has known. "},
                new Book {Id = 3, Title = "Romeo And Juliet", ISBN = 1840224339, AuthorId = 1, GenreId = 2, IsAvailable = true, Summary = "Romeo and Juliet is the world's most famous drama of tragic young love. "}
            };
        }

        [Test]
        public void AutoMapper_Basic_Configuration_IsValid()
        {
            //Arrange

            //Act
            AutoMapperConfig.RegisterMappings();

            //Assert
            Mapper.AssertConfigurationIsValid();
        }

        [Test]
        public void Get_Post_Should_Get_All_Books()
        {
            // Arrange
            //_countryServiceMock.Setup(x => x.GetAll()).Returns(listCountry);
            _unitOfWorkMock.Setup(x => x.BookRepository.GetBooks("")).Returns(books.AsQueryable());
            //_unitOfWorkMock.Setup(x => x.BookRepository.Get("")).Returns(books.AsQueryable());

            // Act
            //var result = ((objController.Index() as ViewResult).Model) as List<Country>;
            var result = objController.GetBooks().ToList();

            // Assert
            Assert.AreEqual(result.Count, 3);
        }
    }

}
