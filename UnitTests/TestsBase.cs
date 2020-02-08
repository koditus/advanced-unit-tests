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

        protected List<LendingDto> Lendings = new List<LendingDto>
            {new LendingDto {Book = new BookDto {Id = 7}, EndDate = null, Id = 1, Person = new PersonDto {Id = 1}}};

        protected Mock<ILendingRepository> MockLendingRepository;
        protected Mock<IBookRepository> MockBookRepository;
        protected Mock<IPersonRepository> MockPersonRepository;
        protected int ValidBookId = 1;
        protected int InvalidBookId = 6;
        protected int ValidPersonId = 1;
        protected int InvalidPersonId = 6;
        protected int LentBookId = 7;
        protected int ReturnedBookId = 8;

        [SetUp]
        public void SetUp()
        {
            MockLendingRepository = new Mock<ILendingRepository>();
            MockLendingRepository.Setup(s => s.GetAll()).Returns(() => Lendings.ToArray());
            MockLendingRepository.Setup(s => s.Add(It.IsAny<LendingDto>()))
                .Callback<LendingDto>(l => Lendings.Add(l))
                .Returns((LendingDto parameter) => parameter);
            MockLendingRepository.Setup(s => s.Update(It.IsAny<LendingDto>()))
                .Callback<LendingDto>(parameter =>
                {
                    Lendings.RemoveAll(l => l.Id == parameter.Id);
                    Lendings.Add(parameter);
                })
                .Returns<LendingDto>(parameter => Lendings.First(l => l.Id == parameter.Id));

            MockBookRepository = new Mock<IBookRepository>();
            MockBookRepository.Setup(s => s.GetOne(ValidBookId))
                .Returns((int parameter) => new BookDto {Id = parameter});
            MockBookRepository.Setup(s => s.GetOne(LentBookId))
                .Returns((int parameter) => new BookDto { Id = parameter });
            MockBookRepository.Setup(s => s.GetOne(ReturnedBookId))
                .Returns((int parameter) => new BookDto { Id = parameter });
            MockBookRepository.Setup(s => s.GetOne(InvalidBookId))
                .Returns<BookDto>(null);

            MockPersonRepository = new Mock<IPersonRepository>();
            MockPersonRepository.Setup(s => s.GetOne(ValidPersonId))
                .Returns((int parameter) => new PersonDto {Id = parameter});
            MockPersonRepository.Setup(s => s.GetOne(InvalidPersonId))
                .Returns<PersonDto>(null);

            LendingService = new LendingService(MockLendingRepository.Object, MockBookRepository.Object,
                MockPersonRepository.Object);
        }

        [TearDown]
        public void TearDown()
        {
            LendingService = null;
            MockLendingRepository = null;
            MockBookRepository = null;
            MockPersonRepository = null;
            Lendings = new List<LendingDto>
                {new LendingDto {Book = new BookDto {Id = 7}, EndDate = null, Id = 1, Person = new PersonDto {Id = 1}}};
        }

        protected void VerifyMocks(int bookRepoCalledTimes, int personRepoCalledTimes, int lendingRepoGetAllCalledTimes,
            int lendingRepoAddCalledTimes, int lendingRepoUpdateCalledTimes)
        {
            MockBookRepository.Verify(mock => mock.GetOne(It.IsAny<int>()), Times.Exactly(bookRepoCalledTimes));
            MockPersonRepository.Verify(mock => mock.GetOne(It.IsAny<int>()), Times.Exactly(personRepoCalledTimes));
            MockLendingRepository.Verify(mock => mock.GetAll(), Times.Exactly(lendingRepoGetAllCalledTimes));
            MockLendingRepository.Verify(mock => mock.Add(It.IsAny<LendingDto>()),
                Times.Exactly(lendingRepoAddCalledTimes));
            MockLendingRepository.Verify(mock => mock.Update(It.IsAny<LendingDto>()),
                Times.Exactly(lendingRepoUpdateCalledTimes));
        }
    }
}