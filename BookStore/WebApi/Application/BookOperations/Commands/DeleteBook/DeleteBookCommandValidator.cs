using FluentValidation;

namespace WebApi.Application.BookOperations.Commands.DeleteBook
{
    class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }

    }
}