using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Services.DTOs;
using Services.Interfaces;
using Services.Interfaces.Implementations;

namespace UnitTests
{
    public class TestsBase
    {
        protected LendingService LendingService;

        private List<LendingDto> _lendings = new List<LendingDto>
            {new LendingDto {Book = new BookDto {Id = 7}, EndDate = null, Id = 1, Person = new PersonDto {Id = 1}}};

        private Mock<ILendingRepository> _mockLendingRepository;
        private Mock<IBookRepository> _mockBookRepository;
        private Mock<IPersonRepository> _mockPersonRepository;
        protected const int ValidBookId = 1;
        protected const int InvalidBookId = 6;
        protected const int ValidPersonId = 1;
        protected const int InvalidPersonId = 6;
        protected const int LentBookId = 7;
        protected const int ReturnedBookId = 8;

        [SetUp]
        public void SetUp()
        {
            _mockLendingRepository = new Mock<ILendingRepository>();
            _mockLendingRepository.Setup(s => s.GetAll()).Returns(() => _lendings.ToArray());
            _mockLendingRepository.Setup(s => s.Add(It.IsAny<LendingDto>()))
                .Callback<LendingDto>(l => _lendings.Add(l))
                .Returns((LendingDto parameter) => parameter);
            _mockLendingRepository.Setup(s => s.Update(It.IsAny<LendingDto>()))
                .Callback<LendingDto>(parameter =>
                {
                    _lendings.RemoveAll(l => l.Id == parameter.Id);
                    _lendings.Add(parameter);
                })
                .Returns<LendingDto>(parameter => _lendings.First(l => l.Id == parameter.Id));

            _mockBookRepository = new Mock<IBookRepository>();
            _mockBookRepository.Setup(s => s.GetOne(ValidBookId))
                .Returns((int parameter) => new BookDto {Id = parameter});
            _mockBookRepository.Setup(s => s.GetOne(LentBookId))
                .Returns((int parameter) => new BookDto {Id = parameter});
            _mockBookRepository.Setup(s => s.GetOne(ReturnedBookId))
                .Returns((int parameter) => new BookDto {Id = parameter});
            _mockBookRepository.Setup(s => s.GetOne(InvalidBookId))
                .Returns<BookDto>(null);

            _mockPersonRepository = new Mock<IPersonRepository>();
            _mockPersonRepository.Setup(s => s.GetOne(ValidPersonId))
                .Returns((int parameter) => new PersonDto {Id = parameter});
            _mockPersonRepository.Setup(s => s.GetOne(InvalidPersonId))
                .Returns<PersonDto>(null);

            LendingService = new LendingService(_mockLendingRepository.Object, _mockBookRepository.Object,
                _mockPersonRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            LendingService = null;
            _mockLendingRepository = null;
            _mockBookRepository = null;
            _mockPersonRepository = null;
            _lendings = new List<LendingDto>
                {new LendingDto {Book = new BookDto {Id = 7}, EndDate = null, Id = 1, Person = new PersonDto {Id = 1}}};
        }

        protected void AssertBookIsValid(LendingDto lending, int bookId, int personId)
        {
            Assert.IsNotNull(lending);
            Assert.AreEqual(bookId, lending.Book.Id);
            Assert.AreEqual(personId, lending.Person.Id);
        }

        protected void AssertBookLent(LendingDto lending, int bookId, int personId)
        {
            AssertBookIsValid(lending, bookId, personId);
            Assert.IsNotNull(lending.StartDate);
            Assert.IsNull(lending.EndDate);
        }

        protected void AssertBookReturned(LendingDto lending, int bookId, int personId)
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
            _mockLendingRepository.Verify(mock => mock.Add(It.IsAny<LendingDto>()),
                Times.Exactly(times));
        }

        protected void VerifyLendingRepository_Update_IsCalled(int times)
        {
            _mockLendingRepository.Verify(mock => mock.Update(It.IsAny<LendingDto>()),
                Times.Exactly(times));
        }
    }
}