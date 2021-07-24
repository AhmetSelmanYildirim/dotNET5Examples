using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        public int GenreID { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public GenreDetailViewModel Handle()
        {
            var genre = _context.Genres.SingleOrDefault(x => x.IsActive && x.ID == GenreID);
            if( genre is null) {
                throw new InvalidOperationException("Book genre not found"); 
            }
            return _mapper.Map<GenreDetailViewModel>(genre);
        }

    }

    public class GenreDetailViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
}