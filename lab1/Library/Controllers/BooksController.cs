using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class BooksController : ControllerBase
    {
        private static List<Book> _books = new()
        {
            new Book { Id = 1, Title = "Book 1", Author = "Author 1", Year = 2000 },
            new Book { Id = 2, Title = "Book 2", Author = "Author 2", Year = 2005 }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks() => _books;

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = _books.FirstOrDefault(p => p.Id == id);

            if (book == null)
                return NotFound();

            return book;
        }

        [HttpPost]
        public ActionResult<Book> PostBook(Book book)
        {
            book.Id = _books.Max(p => p.Id) + 1;
            _books.Add(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult PutBook(int id, Book book)
        {
            var existingBook = _books.FirstOrDefault(p => p.Id == id);

            if (existingBook == null)
                return NotFound();

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Year = book.Year;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _books.FirstOrDefault(p => p.Id == id);

            if (book == null)
                return NotFound();

            _books.Remove(book);
            return NoContent();
        }
    }
}