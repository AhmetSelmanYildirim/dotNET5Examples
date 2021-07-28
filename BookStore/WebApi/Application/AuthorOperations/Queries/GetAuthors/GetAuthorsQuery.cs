using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAuthorsQuery(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors = _dbContext.Authors.OrderBy(x => x.ID);
            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(authors);
            return returnObj;
        }
    }

    public class AuthorsViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DateOfBirth { get; set; }
    }

}