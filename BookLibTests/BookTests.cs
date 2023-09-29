using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookLib.Tests;

[TestClass]
public class BookTests
{
    private const double MaxPrice = 1200;
    private const double ValidPrice = MaxPrice / 2;
    private const double ButtonPrice = double.Epsilon;
    private const string ValidTitle3 = "ABC";
    private const int Id = 1;


    [TestMethod]
    public void ToStringTest()
    {
        var book = new Book(Id, ValidTitle3, ValidPrice);
        Assert.AreEqual(
            $"Book(Id:{Id}), Title: {ValidTitle3}, Price: {ValidPrice}",
            book.ToString());
    }

    [TestMethod]
    [DataRow(null)]
    [ExpectedException(typeof(ArgumentNullException))]
    public void TitleNullTest(string title)
    {
        try
        {
            var book = new Book(Id, title, ValidPrice);
        }
        catch (Exception e)
        {
            Assert.AreEqual("Value cannot be null. (Parameter 'title')", e.Message);
            throw;
        }
    }

    [TestMethod]
    [DataRow("AB")]
    [DataRow(" AB")]
    [DataRow("AB ")]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void TitleInvalidLengthTest(string title)
    {
        try
        {
            var book = new Book(Id, title, ValidPrice);
        }
        catch (Exception e)
        {
            Assert.AreEqual($"Title must be at least 3 characters but was: {title.Length}. (Parameter 'title')", e.Message);
            throw;
        }
    }

    [TestMethod]
    [DataRow(0)]
    [DataRow(ButtonPrice)]
    [DataRow(MaxPrice)]
    [DataRow(MaxPrice + 0.0001)]
    public void ValidPriceTest(double price)
    {
        Book book;
        try
        {
            book = new Book(Id, ValidTitle3, price);
            Assert.AreEqual(price, book.Price);
        }
        catch (Exception e)
        {
            Assert.AreEqual($"Price must be maximum 1200 and greater than 0 but was: {price}. (Parameter 'price')", e.Message);
        }
    }
}