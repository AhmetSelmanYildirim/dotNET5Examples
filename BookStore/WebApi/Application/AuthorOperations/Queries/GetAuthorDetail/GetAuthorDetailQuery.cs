using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        public int AuthorID { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQuery(IMapper mapper, IBookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public AuthorDetailViewModel Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.ID == AuthorID);
            if (author is null)
            {
                throw new InvalidOperationException("Author not found");
            }
            return _mapper.Map<AuthorDetailViewModel>(author);
        }

        public class AuthorDetailViewModel
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string DateOfBirth { get; set; }
        }

    }
}