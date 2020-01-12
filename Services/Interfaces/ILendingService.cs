using Services.DTOs;

namespace Services.Interfaces
{
    public interface ILendingService
    {
        LendingDto Lend(int bookId, int personId);
        LendingDto Return(int bookId);
    }
}