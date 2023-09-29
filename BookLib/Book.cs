using System.Diagnostics.CodeAnalysis;

namespace BookLib
{
    public class Book
    {
        private double _price;
        private string _title;
        public int Id { get; init; }

        public string Title
        {
            get => _title;
            set => _title = ValidTitle(value);
        }
        public double Price
        {
            get => _price;
            set => _price = ValidPrice(value);
        }

        public Book(int id, string title, double price)
        {
            Id = id;
            Title = title;
            Price = price;
        }

        private double ValidPrice(double price)
        {
            if (price is <= 1200 and > 0)
            {
                return price;
            }
            throw new ArgumentOutOfRangeException("price", $"Price must be maximum 1200 and greater than 0 but was: {price}.");
        }

        private string ValidTitle(string title)
        {
            if (title == null)
            {
                throw new ArgumentNullException("title");
            }

            if (title.Trim().Length < 3)
            {
                throw new ArgumentOutOfRangeException("title",$"Title must be at least 3 characters but was: {title.Length}.");
            }

            return title;
        }


        public override string ToString()
        {
            return $"Book({nameof(Id)}:{Id}), {nameof(Title)}: {Title}, {nameof(Price)}: {Price}";
        }
    }
}