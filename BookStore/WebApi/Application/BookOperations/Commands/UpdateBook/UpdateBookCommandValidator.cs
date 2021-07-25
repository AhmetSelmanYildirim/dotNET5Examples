using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.UpdateBook{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>{
        public UpdateBookCommandValidator(){
            RuleFor(command => command.BookId).GreaterThan(0);
            RuleFor(command => command.Model.Title).MinimumLength(2);
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.AuthorID).GreaterThan(0);
        }
    }
}