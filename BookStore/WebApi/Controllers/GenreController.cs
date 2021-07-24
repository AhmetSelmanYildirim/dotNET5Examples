using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebApi.DBOperations;
using WebApi.Application.GenreOperations.Queries.GetGenres;
using static WebApi.Application.BookOperations.Commands.CreateBook.CreateBookCommand;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.Application.GenreOperations.Commands.CreateGenre;
using WebApi.Application.GenreOperations.Commands.UpdateGenre;
using WebApi.Application.GenreOperations.Commands.DeleteGenre;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var obj = query.Handle();
            return Ok(obj);
        }


        [HttpGet("id")]
        public IActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreID = id;
            GetGenreDetailQueryValidator validator = new();
            validator.ValidateAndThrow(query);

            var obj = query.Handle();
            return Ok(obj);
        }


        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new(_context);
            command.Model = newGenre;

            CreateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }


        [HttpPut]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new(_context);
            command.GenreID = id;
            command.Model = updateGenre;

            UpdateGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new(_context);
            command.GenreID = id;

            DeleteGenreCommandValidator validator = new();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }



    }
}