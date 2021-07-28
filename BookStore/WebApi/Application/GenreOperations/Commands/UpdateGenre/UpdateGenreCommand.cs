using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreID { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        public UpdateGenreCommand(IBookStoreDbContext context = null)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.ID == GenreID);
            if (genre is null)
            {
                throw new InvalidOperationException("Book genre not found");
            }
            if(_context.Genres.Any(x=> x.Name.ToLower()== Model.Name.ToLower() && x.ID != GenreID)){
                throw new InvalidOperationException("There is same genre already exist.");
            }
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name ;
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }

    }
    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}