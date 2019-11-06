using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManager.Models
{
    public partial class BookAuthor
    {
        public string AuthorName
        {
            get
            {
                BookManagerDbEntities dbContext = new BookManagerDbEntities();
                List<BookAuthor> books = dbContext.BookAuthor.Where(x => x.BookKey == BookKey).ToList();

                StringBuilder sb = new StringBuilder();

                if (books.Count() > 1)
                {
                    for(int i = 0;i < books.Count(); i++)
                    {
                        if (i == books.Count() - 1)
                            sb.Append(books[i].Author.Name);
                        else
                            sb.Append($"{books[i].Author.Name}, ");
                    }
                }
                else
                {
                    sb.Append(books[0].Author.Name);
                }

                return sb.ToString();
            }
        }
    }
}
