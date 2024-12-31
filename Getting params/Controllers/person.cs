using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using static System.Reflection.Metadata.BlobBuilder;

namespace Getting_params.Controllers
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Pagesize { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }

    public class AllList
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public List<Book>? AlllistsBooks { get; set; }
    }

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class Personinfo : ControllerBase
    {
        public static List<Person> person = new List<Person>();
        public static List<Book> books = new List<Book>();
        public static List<AllList> alllists = new List<AllList>();

        [HttpPost]
        //cretaing book
        public List<Book> CreatebNewBook([FromBody] List<Book> b)
        {
            int count = books.Count;
            foreach (var item in b)
            {
                item.Id = ++count;
                books.Add(item);
            }
            return books;
        }
        [HttpGet]
        //getting books
        public List<Book> GetBooks()
        {
            return books;
        }

        //static List<Book> book = new List<Book>();
        [HttpPost]
        //Create
        public List<Person>? CreatePerson([FromBody] List<Person> people)
        {
            int count1 = person.Count;
            foreach (var item in people)
            {
                item.Id = ++count1;
                person.Add(item);
            }
            return person;
        }
        [HttpGet]
        public List<Person> GetPersons()
        {
            return person;
        }

        [HttpPost]
        public IActionResult Update(int Personid, int bookid)
        {
            if (Personid > 0 && bookid > 0)
            {
                var personId = person.FirstOrDefault(x => x.Id == Personid);
                var book = books.FirstOrDefault(c => c.Id == bookid);
                if (personId != null && book != null)
                {
                    var a = alllists.FirstOrDefault(x => x.Id == Personid);
                    if (a == null || book == null)
                    {
                        alllists.Add(new AllList
                        {
                            Id = personId.Id,
                            Name = personId.Name,
                            Age = personId.Age,
                            AlllistsBooks = new List<Book>
                        {
                            new Book
                            {
                               Id=bookid,
                               Name=book.Name,
                               Pagesize = book.Pagesize,
                            }
                        }
                        }

                       );
                    }
                    else
                    {

                        var ab = a.AlllistsBooks.FirstOrDefault(x => x.Id == bookid);
                        a.AlllistsBooks.Add(new Book
                        {
                            Id = bookid,
                            Name = book.Name,
                            Pagesize = book.Pagesize,
                        });
                    }



                }
              

            }

            return Ok(alllists);

        }
    }
}