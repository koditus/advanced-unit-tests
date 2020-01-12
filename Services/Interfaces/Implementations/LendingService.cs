using System;
using System.Linq;
using Services.DTOs;

namespace Services.Interfaces.Implementations
{
    public class LendingService : ILendingService
    {
        private readonly ILendingEntityService _lendingEntityService;
        private readonly IBookEntityService _bookEntityService;
        private readonly IPersonEntityService _personEntityService;

        public LendingService(ILendingEntityService lendingEntityService,
            IBookEntityService bookEntityService, IPersonEntityService personEntityService)
        {
            _lendingEntityService = lendingEntityService;
            _bookEntityService = bookEntityService;
            _personEntityService = personEntityService;
        }

        public LendingDto Lend(int bookId, int personId)
        {
            var book = _bookEntityService.GetOne(bookId);
            if(book == null)
                throw new InvalidOperationException($"Book not found! BookId: {bookId}");

            var person = _personEntityService.GetOne(personId);
            if(person == null)
                throw new InvalidOperationException($"Person not found! PersonId: {personId}");

            var allLendings = _lendingEntityService.GetAll();
            var lending = allLendings.FirstOrDefault(l => l.Book.Id == bookId);
            if (lending != null && lending.EndDate == null)
                throw new InvalidOperationException($"Book is already lent!");

            var newLending = new LendingDto
            {
                Id = allLendings.Length > 0 ? allLendings?.Last()?.Id ?? 0 + 1 : 1,
                Book = book,
                Person = person,
                StartDate = DateTime.UtcNow
            };
            _lendingEntityService.Add(newLending);
            return newLending;
        }

        public LendingDto Return(int bookId)
        {
            var allLendings = _lendingEntityService.GetAll();
            var lending = allLendings.FirstOrDefault(l => l.Book.Id == bookId && l.EndDate == null);
            if (lending == null)
                throw new InvalidOperationException($"Book with id: {bookId} is not lent!");

            if(lending.StartDate <= lending.EndDate)
                throw new InvalidOperationException($"Book with id: {bookId} is not lent!");

            lending.EndDate = DateTime.UtcNow;
            _lendingEntityService.Update(lending);
            return lending;
        }
    }
}