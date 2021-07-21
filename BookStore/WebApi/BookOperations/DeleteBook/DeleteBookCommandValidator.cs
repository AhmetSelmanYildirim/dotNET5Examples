using FluentValidation;

namespace WebApi.BookOperations.DeleteBook
{
    class DeleteBookCommandValidator : AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }

    }
}