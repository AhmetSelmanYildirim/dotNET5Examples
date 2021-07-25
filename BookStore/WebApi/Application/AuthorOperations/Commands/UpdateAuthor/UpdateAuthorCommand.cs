using System.Linq;
using System;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorID { get; set; }

        public UpdateAuthorModel Model { get; set; }

        private readonly BookStoreDbContext _context;

        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.ID == AuthorID);
            if (author is null)
            {
                throw new InvalidOperationException("Author not found");
            }
            if (_context.Authors.Any(x => x.Name.ToLower() == Model.Name.ToLower() && x.ID != AuthorID))
            {
                throw new InvalidOperationException("There is same author already exist.");
            }

            author.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? author.Name : Model.Name;
            author.Surname = string.IsNullOrEmpty(Model.Surname.Trim()) ? author.Surname : Model.Surname;
            author.DateOfBirth = string.IsNullOrEmpty(Model.DateOfBirth.ToString().Trim()) ? author.DateOfBirth : Model.DateOfBirth;

            _context.SaveChanges();
        }

        public class UpdateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }

        }
    }
}