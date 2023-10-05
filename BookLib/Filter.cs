using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLib
{
    public enum BookSortValue
    {
        Title, Price, NoSort
    }
    public record Filter
    {
        public BookSortValue? BookSortValue { get; init; }
        public int? MaxPrice { get; init; }
    }
}
