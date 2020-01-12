using Services.DAOs;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface IPersonEntityService : IEntityService<PersonDto, PersonDao>
    {
    }
}