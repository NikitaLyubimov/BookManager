using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public partial class Book
    {
        private BookManagerDbEntities _dbContext = new BookManagerDbEntities();

        public string AuthorName
        {
            get
            {
                List<BookAuthor> bookAuthor = BookAuthor.Where(x => x.Book.Title.Contains(Title)).ToList();
                string author = "";

                if (bookAuthor.Count > 1)
                {
                    foreach (BookAuthor ba in bookAuthor)
                    {
                        author += $"{ba.Author.Name}, ";
                    }
                    return author;
                }
                else
                    return bookAuthor[0].Author.Name;
                   
            }
        }
    }
}
