using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Services;
using Services.Interfaces;
using Services.Models;

namespace UnitTests
{
    public class LendingServiceTestsBase
    {
        protected const int ValidBookId = 1;
        protected const int InvalidBookId = 6;
        protected const int ValidPersonId = 1;
        protected const int InvalidPersonId = 6;
        protected const int LentBookId = 7;
        protected const int ReturnedBookId = 8;

        protected LendingService LendingService;

        private Dictionary<int, Lending> _lendings;

        private Mock<ILendingRepository> _mockLendingRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private Mock<IPersonRepository> _mockPersonRepository;

        private int NextLendingId => _lendings.Keys.Append(0).Max() + 1;

        [SetUp]
        public void SetUp()
        {
            _lendings = new Dictionary<int, Lending>
            {
                {
                    1, new Lending
                    {
                        Book = new Book
                        {
                            Id = LentBookId
                        },
                        EndDate = null,
                        Id = 1,
                        Person = new Person
                        {
                            Id = ValidPersonId
                        }
                    }
                }
            };

            _mockLendingRepository = new Mock<ILendingRepository>();
            _mockLendingRepository.Setup(repo => repo.GetAll()).Returns(() => _lendings.Values.ToArray());
            _mockLendingRepository.Setup(repo => repo.Add(It.IsAny<Lending>()))
                .Callback<Lending>(lending =>
                {
                    lending.Id = NextLendingId;
                    _lendings.Add(lending.Id, lending);
                })
                .Returns((Lending lending) => lending);
            _mockLendingRepository.Setup(repo => repo.Update(It.IsAny<Lending>()))
                .Callback<Lending>(lending => _lendings[lending.Id] = lending)
                .Returns<Lending>(lending => _lendings[lending.Id]);

            _mockBookRepository = new Mock<IBookRepository>();
            _mockBookRepository.Setup(repo => repo.GetOne(ValidBookId))
                .Returns((int bookId) => new Book { Id = bookId });
            _mockBookRepository.Setup(repo => repo.GetOne(LentBookId))
                .Returns((int bookId) => new Book { Id = bookId });
            _mockBookRepository.Setup(repo => repo.GetOne(ReturnedBookId))
                .Returns((int bookId) => new Book { Id = bookId });
            _mockBookRepository.Setup(repo => repo.GetOne(InvalidBookId))
                .Returns<Book>(null);

            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockPersonRepository.Setup(repo => repo.GetOne(ValidPersonId))
                .Returns((int parameter) => new Person { Id = parameter });
            _mockPersonRepository.Setup(repo => repo.GetOne(InvalidPersonId))
                .Returns<Person>(null);

            LendingService = new LendingService(_mockLendingRepository.Object, _mockBookRepository.Object,
                _mockPersonRepository.Object);
        }

        protected void AssertBookIsValid(Lending lending, int bookId, int personId)
        {
            Assert.IsNotNull(lending);
            Assert.AreEqual(bookId, lending.Book.Id);
            Assert.AreEqual(personId, lending.Person.Id);
        }

        protected void AssertBookLent(Lending lending, int bookId, int personId)
        {
            AssertBookIsValid(lending, bookId, personId);
            Assert.IsNotNull(lending.StartDate);
            Assert.IsNull(lending.EndDate);
        }

        protected void AssertBookReturned(Lending lending, int bookId, int personId)
        {
            AssertBookIsValid(lending, bookId, personId);
            Assert.IsNotNull(lending.StartDate);
            Assert.IsNotNull(lending.EndDate);
        }

        protected void VerifyNoOtherCalls()
        {
            _mockBookRepository.VerifyNoOtherCalls();
            _mockLendingRepository.VerifyNoOtherCalls();
            _mockPersonRepository.VerifyNoOtherCalls();
        }

        protected void VerifyBookRepository_GetOne_IsCalled(int times)
        {
            _mockBookRepository.Verify(mock => mock.GetOne(It.IsAny<int>()), Times.Exactly(times));
        }

        protected void VerifyPersonRepository_GetOne_IsCalled(int times)
        {
            _mockPersonRepository.Verify(mock => mock.GetOne(It.IsAny<int>()), Times.Exactly(times));
        }

        protected void VerifyLendingRepository_GetAll_IsCalled(int times)
        {
            _mockLendingRepository.Verify(mock => mock.GetAll(), Times.Exactly(times));
        }

        protected void VerifyLendingRepository_Add_IsCalled(int times)
        {
            _mockLendingRepository.Verify(mock => mock.Add(It.IsAny<Lending>()),
                Times.Exactly(times));
        }

        protected void VerifyLendingRepository_Update_IsCalled(int times)
        {
            _mockLendingRepository.Verify(mock => mock.Update(It.IsAny<Lending>()),
                Times.Exactly(times));
        }
    }
}