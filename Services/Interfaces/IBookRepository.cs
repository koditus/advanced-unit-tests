using Services.DAOs;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IBookRepository : IRepository<BookDto, BookDao>
    {
    }
}