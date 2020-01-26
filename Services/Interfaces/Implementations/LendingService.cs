using System;
using System.Linq;
using Services.DTOs;

namespace Services.Interfaces.Implementations
{
    public class LendingService : ILendingService
    {
        private readonly ILendingRepository _lendingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IPersonRepository _personRepository;

        public LendingService(ILendingRepository lendingRepository,
            IBookRepository bookRepository, IPersonRepository personRepository)
        {
            _lendingRepository = lendingRepository;
            _bookRepository = bookRepository;
            _personRepository = personRepository;
        }

        public LendingDto Lend(int bookId, int personId)
        {
            var book = _bookRepository.GetOne(bookId);
            if(book == null)
                throw new InvalidOperationException($"Book not found! BookId: {bookId}");

            var person = _personRepository.GetOne(personId);
            if(person == null)
                throw new InvalidOperationException($"Person not found! PersonId: {personId}");

            var allLendings = _lendingRepository.GetAll();
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
            _lendingRepository.Add(newLending);
            return newLending;
        }

        public LendingDto Return(int bookId)
        {
            var allLendings = _lendingRepository.GetAll();
            var lending = allLendings.FirstOrDefault(l => l.Book.Id == bookId && l.EndDate == null);
            if (lending == null)
                throw new InvalidOperationException($"Book with id: {bookId} is not lent!");

            if(lending.StartDate <= lending.EndDate)
                throw new InvalidOperationException($"Book with id: {bookId} is not lent!");

            lending.EndDate = DateTime.UtcNow;
            _lendingRepository.Update(lending);
            return lending;
        }
    }
}