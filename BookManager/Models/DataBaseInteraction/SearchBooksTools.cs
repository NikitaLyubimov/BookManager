using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BookManager.Models
{
    public class SearchBooksTools
    {
        private static BookManagerDbEntities _dbContext = new BookManagerDbEntities();

        
        /// <summary>
        /// Method which searches book depends on user request
        /// </summary>
        /// <param name="title">Book's title</param>
        /// <param name="author">Book's author</param>
        /// <param name="subject">Book's subject</param>
        /// <returns></returns>
        public static List<BookAuthor> Search(string title, string author = null, string subject = null)
        {

            if (string.IsNullOrEmpty(author) && string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(title))
                return _dbContext.BookAuthor.Where(x => x.Book.Title.Contains(title)).ToList();

            else if (string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(author) && !string.IsNullOrEmpty(title))
   
                return _dbContext.BookAuthor.Where(x => x.Book.Title.Contains(title) && x.Author.Name.Contains(author)).ToList();

            else if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(subject) && string.IsNullOrEmpty(author))
            {
                return _dbContext.BookSubject.Where(x => x.Book.Title.Contains(title) && x.Subject.Contains(subject))
                    .Select(x => x.Book)
                    .Select(x => x.BookAuthor) as List<BookAuthor>;

            }

            else if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(subject) && !string.IsNullOrEmpty(author))
            {
                return _dbContext.BookSubject.Where(x => x.Book.Title.Contains(title) && x.Subject.Contains(subject)
                                            && x.Book.BookAuthor.FirstOrDefault(y => y.Author.Name.Contains(author)) != null)
                                            .Select(x => x.Book)
                                            .Select(x => x.BookAuthor) as List<BookAuthor>;

            }

            else if (!string.IsNullOrEmpty(author) && !string.IsNullOrEmpty(subject) && string.IsNullOrEmpty(title))
            {
                return  _dbContext.BookSubject.Where(x => x.Book.BookAuthor.FirstOrDefault(y => y.Author.Name.Contains(author)) != null && x.Subject.Contains(subject))
                    .Select(x => x.Book)
                    .Select(x => x.BookAuthor) as List<BookAuthor>;

            }

            else
            {
                return  _dbContext.BookAuthor.Where(x => x.Author.Name.Contains(author)).ToList();

            }
        }
    }
}
