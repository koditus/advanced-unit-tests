using Services.DAOs;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface ILendingEntityService : IEntityService<LendingDto, LendingDao>
    {
    }
}