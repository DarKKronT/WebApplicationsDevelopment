using Microsoft.AspNetCore.Mvc;
using Library.Models;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class AuthorsController : ControllerBase
    {
        private static List<Author> _authors = new()
        {
            new Author { Id = 1, Name = "Author 1", Country = "Country 1" },
            new Author { Id = 2, Name = "Author 2", Country = "Country 2" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors() => _authors;

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = _authors.FirstOrDefault(p => p.Id == id);

            if (author == null)
                return NotFound();

            return author;
        }

        [HttpPost]
        public ActionResult<Author> PostAuthor(Author author)
        {
            author.Id = _authors.Max(p => p.Id) + 1;
            _authors.Add(author);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult PutAuthor(int id, Author author)
        {
            var existingAuthor = _authors.FirstOrDefault(p => p.Id == id);

            if (existingAuthor == null)
                return NotFound();

            existingAuthor.Name = author.Name;
            existingAuthor.Country = author.Country;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = _authors.FirstOrDefault(p => p.Id == id);

            if (author == null)
                return NotFound();

            _authors.Remove(author);

            return NoContent();
        }
    }
}