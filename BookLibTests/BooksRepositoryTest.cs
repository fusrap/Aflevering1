using BookLib;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookLibTests;

[TestClass]
public class BooksRepositoryTest
{
    private static int _id1 = 1;
    private static double _price1 = 50.0;
    private static string _title1 = "Harry Potter and the Sorcerer's Stone";
    private static Book _book1 = new Book(_id1, _title1, _price1);

    private static int _id2 = 2;
    private static double _price2 = 50.0;
    private static string _title2 = "To Kill a Mockingbird";
    private static Book _book2 = new Book(_id2, _title2, _price2);

    private static int _id3 = 3;
    private static double _price3 = 25.0;
    private static string _title3 = "The Great Gatsby";
    private static Book _book3 = new Book(_id3, _title3, _price3);

    private static int _id4 = 4;
    private static double _price4 = 15.0;
    private static string _title4 = "1864";
    private static Book _book4 = new Book(_id4, _title4, _price4);

    private static int _id5 = 5;
    private static double _price5 = 200.0;
    private static string _title5 = "The Hobbit";
    private static Book _book5 = new Book(_id5, _title5, _price5);

    private static List<Book> _expectedBooks = new()
    {
        _book1,
        _book2,
        _book3,
        _book4,
        _book5
    };

    [TestMethod]
    public void BooksRepositoryInit()
    {
        var repository = new BooksRepository(true);
        var actualBooks = repository.Get();

        Assert.AreEqual(_expectedBooks.Count, 5);

        for (int i = 0; i < repository.Get().Count; i++)
        {
            Assert.AreEqual(_expectedBooks[i].Id, actualBooks[i].Id);
            Assert.AreEqual(_expectedBooks[i].Title, actualBooks[i].Title);
            Assert.AreEqual(_expectedBooks[i].Price, actualBooks[i].Price);
        }
    }

    [TestMethod]
    public void Add_Get()
    {
        var repository = new BooksRepository();

        Assert.IsTrue(repository.Get().Count == 0);
        _expectedBooks.ForEach(book =>
        {
           var addedBook = repository.Add(book);
           Assert.AreEqual(addedBook,book);
        });
        Assert.IsTrue(repository.Get().Count == 5);

        var actualBooks = repository.Get();
        for (int i = 0; i < repository.Get().Count; i++)
        {
            Assert.AreEqual(_expectedBooks[i].Id, actualBooks[i].Id);
            Assert.AreEqual(_expectedBooks[i].Title, actualBooks[i].Title);
            Assert.AreEqual(_expectedBooks[i].Price, actualBooks[i].Price);
        }
    }

    [TestMethod]
    [DataRow(25)]
    [DataRow(50)]
    [DataRow(450)]
    [DataRow(500)]
    public void Get_FilterByPrice(double maxPrice)
    {
        var repository = new BooksRepository(true);
        var actualBooks = repository.Get(maxPrice);
        actualBooks.ForEach(book => Assert.IsTrue(book.Price <= maxPrice));
    }

    [TestMethod]
    public void Get_SortByTitle()
    {
        var repository = new BooksRepository(true);
        var actualBooksSorted = repository.Get(null,BookSortValue.Title);

        Assert.AreEqual(_expectedBooks[3].Title, actualBooksSorted[0].Title);
        Assert.AreEqual(_book4.Title, actualBooksSorted[0].Title); 

        Assert.AreEqual(_expectedBooks[0].Title, actualBooksSorted[1].Title);
        Assert.AreEqual(_book1.Title, actualBooksSorted[1].Title);

        Assert.AreEqual(_expectedBooks[2].Title, actualBooksSorted[2].Title);
        Assert.AreEqual(_book3.Title, actualBooksSorted[2].Title);

        Assert.AreEqual(_expectedBooks[4].Title, actualBooksSorted[3].Title);
        Assert.AreEqual(_book5.Title, actualBooksSorted[3].Title);

        Assert.AreEqual(_expectedBooks[1].Title, actualBooksSorted[4].Title);
        Assert.AreEqual(_book2.Title, actualBooksSorted[4].Title);
    }

    [TestMethod]
    public void Get_SortByPrice()
    {
        var repository = new BooksRepository(true);
        var sortedBooks = repository.Get(null, BookSortValue.Price);

        int j = 1;
        for (int i = 0; i < sortedBooks.Count; i++)
        {
            Assert.IsTrue(sortedBooks[i].Price <= sortedBooks[j].Price);
            if (j < sortedBooks.Count -1)
            {
                j++;
            }
        }
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Add_Exception()
    {
        var repository = new BooksRepository(true);
        repository.Add(_book1);
    }

    [TestMethod]
    public void GetById()
    {
        var repository = new BooksRepository(true);
        var book = repository.GetById(_book1.Id);

        Assert.IsNotNull(book);
        Assert.AreEqual(book.Id,_book1.Id);
        Assert.AreEqual(book.Title, _book1.Title);
        Assert.AreEqual(book.Price, _book1.Price);

        book = repository.GetById(0);
        Assert.IsNull(book);
    }

    [TestMethod]
    public void Update()
    {
        var repository = new BooksRepository();

        repository.Add(_book1);
        var updatedBook = repository.Update(_book2);
        Assert.IsNull(updatedBook);

        string updatedTitle = "new title";
        double updatedPrice = 500.0;

        updatedBook = repository.Update(new Book(_id1,updatedTitle,updatedPrice));

        Assert.AreEqual(_id1, updatedBook!.Id);
        Assert.AreEqual(updatedTitle, updatedBook.Title);
        Assert.AreEqual(updatedPrice, updatedBook.Price);
    }

    [TestMethod]
    public void Delete()
    {
        var repository = new BooksRepository();
        Assert.IsTrue(repository.Get().Count == 0);

        repository.Add(_book1);
        repository.Add(_book2);
        Assert.IsTrue(repository.Get().Count == 2);

        Assert.IsTrue(repository.Delete(_book1.Id)!.Equals(_book1));
        Assert.IsNull(repository.Delete(_book1.Id));
    }

    [TestMethod]
    public void ToStringTest()
    {
        var repository = new BooksRepository(true);
        Assert.AreEqual("Repository: _books: \nBook(Id:1), Title: Harry Potter and the Sorcerer's Stone, Price: 50\nBook(Id:2), Title: To Kill a Mockingbird, Price: 50\nBook(Id:3), Title: The Great Gatsby, Price: 25\nBook(Id:4), Title: 1864, Price: 15\nBook(Id:5), Title: The Hobbit, Price: 200", repository.ToString());
    }
}