using System;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Theory]
        [InlineData("Lord Of The Rings", 0, 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1, 1)]
        [InlineData("", 0, 0, 0)]
        [InlineData("", 100, 1, 1)]
        [InlineData("", 0, 1, 0)]
        [InlineData("Lor", 100, 1, 1)]
        [InlineData("Lord", 0, 0, 0)]
        [InlineData("Lord", 1, 0, 1)]

        public void WhenInvalidInputAreGiven_Validator_ShouldBeReturnErrors(string title, int PageCount, int GenreId, int AuthorID)
        {
            //arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = PageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = GenreId,
                AuthorID = AuthorID
            };

            //act
            CreateBookCommandValidator validator = new();
            var result = validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord of The Rings",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date,
                GenreId = 1,
                AuthorID = 1
            };

            CreateBookCommandValidator validator = new();
            var error = validator.Validate(command);

            error.Errors.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.Model = new CreateBookModel()
            {
                Title = "Lord of The Rings",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-2),
                GenreId = 1,
                AuthorID = 1
            };

            CreateBookCommandValidator validator = new();
            var error = validator.Validate(command);

            error.Errors.Count.Should().Equals(0);
        }

    }
}