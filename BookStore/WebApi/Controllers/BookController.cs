using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.BookOperations.CreateBook;
using WebApi.BookOperations.DeleteBook;
using WebApi.BookOperations.GetBookDetail;
using WebApi.BookOperations.GetBooks;
using WebApi.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.AddControllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {


        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        //Inject edilen instance
        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*
        private static List<Book> BookList = new List<Book>()
        {
            new Book{
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1,
                PageCount = 200,
                PublishDate = new System.DateTime(2001,06,12)
            },
            new Book{
                Id = 2,
                Title = "Herland",
                GenreId = 2,
                PageCount = 250,
                PublishDate = new System.DateTime(2010,05,23)
            },
            new Book{
                Id = 3,
                Title = "Dune",
                GenreId = 2,
                PageCount = 540,
                PublishDate = new System.DateTime(2001,12,21)
            }
        };
        */

        //Get All
        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        //Get By ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {

            BookDetailViewModel result;

            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
                query.BookId = id;
                result = query.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);

        }

        // [HttpGet]
        // public Book Get([FromQuery] string id)
        // {
        //     var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }


        //POST
        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {
            CreateBookCommand command = new CreateBookCommand(_context, _mapper);

            try
            {
                command.Model = newBook;
                command.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();

        }

        //PUT
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {

            try
            {
                UpdateBookCommand command = new UpdateBookCommand(_context);
                command.BookId = id;
                command.Model = updatedBook;
                command.Handle();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                DeleteBookCommand command = new DeleteBookCommand(_context);
                command.BookId = id;
                command.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

    }

}