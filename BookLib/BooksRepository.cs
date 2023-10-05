using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookLib
{
    public class BooksRepository
    {
        private List<Book> _books = new();

        public BooksRepository(bool init = false)
        {
            if(init) InitValues();
        }

        public List<Book> Get(double? maxPrice = null, BookSortValue sortBy = BookSortValue.NoSort)
        {
            List<Book> result = new(_books);
             
            if (maxPrice != null)
                result = FilterOnPrice(maxPrice, result);

            if (sortBy != BookSortValue.NoSort)
                result = SortBooks(sortBy, result);

            return result;
        }

        public List<Book> Get(Filter? filter)
        {
            List<Book> result = new(_books);
            if(filter is null) 
                return result;

            if (filter.BookSortValue is null)
                return Get(filter.MaxPrice, BookSortValue.NoSort);

            return Get(filter.MaxPrice,filter.BookSortValue.Value);
        }

        public Book? GetById(int id)
        {
            return _books.Find(book => id.Equals(book.Id)) ?? null;
        }

        public Book Add(Book book)
        {
            if (GetById(book.Id) == null)
            {
                _books.Add(book);
                return book;
            }

            throw new ArgumentException($"Book with id ({book.Id}) already exist in the repository. Either choose a new ID or use the Update(book) method if this was intended", nameof(book));
        }

        public Book? Update(Book update)
        {
            var book = GetById(update.Id);

            if (book == null) 
                return null;

            book.Price = update.Price;
            book.Title = update.Title;

            return book;
        }

        public Book? Delete(int id)
        {
            var book = GetById(id);
            if (book != null)
            { 
                _books.Remove(book);
            }
            return book;
        }

        private void InitValues()
        {
            _books.AddRange(new List<Book>
            {
                new Book(1, "Harry Potter and the Sorcerer's Stone", 50.0),
                new Book(2, "To Kill a Mockingbird", 50.0),
                new Book(3, "The Great Gatsby", 25.0),
                new Book(4, "1864", 15.0),
                new Book(5, "The Hobbit", 200.0)
            });
        }

        private List<Book> SortBooks(BookSortValue sortBy, List<Book> result)
        {
            switch (sortBy)
            {
                case BookSortValue.Title:
                    return result.OrderBy(book => book.Title).ToList();
                case BookSortValue.Price:
                    return result.OrderBy(book => book.Price).ToList();
            }
            return result;
        }

        private List<Book> FilterOnPrice(double? filterPrice, List<Book> result)
        {
            if (filterPrice.HasValue)
            {
                result = result.Where(book => book.Price <= filterPrice).ToList();
            }

            return result;
        }

        public override string ToString()
        {
            string books = $"Repository: {nameof(_books)}: ";
            _books.ForEach(book => books = books + "\n" + book.ToString());
            return books;
        }
    }
}
