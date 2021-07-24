using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Application.BookOperations.Commands.CreateBook;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.BookOperations.Queries.GetBooks;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
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

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            query.BookId = id;
            validator.ValidateAndThrow(query);
            result = query.Handle();

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


            command.Model = newBook;

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();

        }

        //PUT
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {

            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            command.BookId = id;
            command.Model = updatedBook;
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

        //DELETE
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {

            DeleteBookCommand command = new DeleteBookCommand(_context);

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();

            command.BookId = id;
            validator.ValidateAndThrow(command);
            command.Handle();

            return Ok();
        }

    }

}