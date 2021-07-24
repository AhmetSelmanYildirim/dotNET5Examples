using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.DeleteGenre{
    public class DeleteGenreCommand{
        public int GenreID { get; set; }
        public readonly BookStoreDbContext _context;

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle(){
            var genre = _context.Genres.SingleOrDefault(x=> x.ID == GenreID);
            if(genre is null){
                throw new InvalidOperationException("Book genre not found");
            }
            _context.Genres.Remove(genre);
            _context.SaveChanges();

        }
    }
}