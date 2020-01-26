using Services.DAOs;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IPersonRepository : IRepository<PersonDto, PersonDao>
    {
    }
}