using Services.DAOs;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IBookEntityService : IEntityService<BookDto, BookDao>
    {
    }
}