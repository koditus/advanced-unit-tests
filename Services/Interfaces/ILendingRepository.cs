using Services.DAOs;
using Services.DTOs;

namespace Services.Interfaces
{
    public interface ILendingRepository : IRepository<LendingDto, LendingDao>
    {
    }
}