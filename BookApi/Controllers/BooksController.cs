using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookApi.Controllers
{
    public class BooksController : ApiController
    {
        private BookManagerDbEntities _context = new BookManagerDbEntities();

        public List<BookModel> Get([FromUri]string offset)
        {
            int offsetInt = int.Parse(offset);

            List<BookModel> bookModels = new List<BookModel>();
            List<Book> books = _context.Book.ToList().GetRange(offsetInt, 50);

            foreach(Book book in books)
            {
                BookModel newBook = new BookModel
                {
                    Key = book.Key,
                    Title = book.Title,
                    Subtitle = book.Subtitle,
                    FirstPublishDate = book.FirstPublishDate,
                    Description = book.Description
                };

                bookModels.Add(newBook);
            }

            return bookModels;
            
        }
    }
}
