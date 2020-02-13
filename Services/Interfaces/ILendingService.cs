using Services.Models;

namespace Services.Interfaces
{
    public interface ILendingService
    {
        Lending Lend(int bookId, int personId);
        Lending Return(int bookId);
    }
}