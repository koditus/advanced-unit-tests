using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Services.DTOs;
using Services.Interfaces;
using Services.Interfaces.Implementations;

namespace UnitTests
{
    [TestFixture]
    public class LendingTests
    {
        private LendingService _lendingService;
        private List<LendingDto> _lendings = new List<LendingDto>();
        private Mock<ILendingEntityService> _mockLendingEntityService;
        private Mock<IBookEntityService> _mockBookEntityService;
        private Mock<IPersonEntityService> _mockPersonEntityService;

        [SetUp]
        public void SetUp()
        {
            _mockLendingEntityService = new Mock<ILendingEntityService>();
            _mockLendingEntityService.Setup(s => s.GetAll()).Returns(() => _lendings.ToArray());
            _mockLendingEntityService.Setup(s => s.Add(It.IsAny<LendingDto>()))
                .Callback<LendingDto>(l => _lendings.Add(l))
                .Returns((LendingDto parameter) => parameter);
            _mockLendingEntityService.Setup(s => s.Update(It.IsAny<LendingDto>()))
                .Callback<LendingDto>(parameter =>
                {
                    _lendings.RemoveAll(l => l.Id == parameter.Id);
                    _lendings.Add(parameter);
                })
                .Returns<LendingDto>(parameter => _lendings.First(l => l.Id == parameter.Id));

            _mockBookEntityService = new Mock<IBookEntityService>();
            _mockBookEntityService.Setup(s => s.GetOne(It.IsInRange(1, 5, Range.Inclusive)))
                .Returns((int parameter) => new BookDto {Id = parameter});
            _mockBookEntityService.Setup(s => s.GetOne(It.IsInRange(6, 10, Range.Inclusive)))
                .Returns<BookDto>(null);

            _mockPersonEntityService = new Mock<IPersonEntityService>();
            _mockPersonEntityService.Setup(s => s.GetOne(It.IsInRange(1, 5, Range.Inclusive)))
                .Returns((int parameter) => new PersonDto {Id = parameter});
            _mockPersonEntityService.Setup(s => s.GetOne(It.IsInRange(6, 10, Range.Inclusive)))
                .Returns<PersonDto>(null);

            _lendingService = new LendingService(_mockLendingEntityService.Object, _mockBookEntityService.Object,
                _mockPersonEntityService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _lendingService = null;
            _mockLendingEntityService = null;
            _mockBookEntityService = null;
            _mockPersonEntityService = null;
            _lendings = new List<LendingDto>();
        }

        [Test]
        [TestCase(true, false, false, false)]
        [TestCase(false, true, false, true)]
        [TestCase(false, false, false, true)]
        [TestCase(true, false, true, true)]
        public void GivenBookStatus_WhenPerformingAction_ThenActionIsPerformedWithValidations(bool isLent, bool isReturned, bool isLending, bool hitsValidation)
        {
            //Arrange
            if(isLent || isReturned)
                _lendingService.Lend(1, 1);

            if(isReturned)
                _lendingService.Return(1);

            //Act
            LendingDto lending = null;
            LendingDto returnedLending = null;
            Exception exception = null;
            if(isLending)
            {
                if (isLent)
                    exception = Assert.Throws<InvalidOperationException>(() => _lendingService.Lend(1, 1));
                else
                    lending = _lendingService.Lend(1, 1);
            }
            else
            {
                if (isReturned || !isLent)
                    exception = Assert.Throws<InvalidOperationException>(() => _lendingService.Return(1));

                if(isLent)
                    returnedLending = _lendingService.Return(1);
            }

            //Assert
            if(isLending && !hitsValidation)
            {
                Assert.IsNotNull(lending);
                Assert.AreEqual(1, lending.Book.Id);
                Assert.AreEqual(1, lending.Person.Id);
                Assert.IsNotNull(lending.StartDate);
            }
            else if(!hitsValidation)
            {
                Assert.IsNotNull(returnedLending);
                Assert.IsNotNull(returnedLending.EndDate);
            }
            else
            {
                Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
            }
        }

        [Test]
        [TestCase(1, 1, false)]
        [TestCase(6, 1, true)]
        [TestCase(1, 6, true)]
        [TestCase(6, 6, true)]
        public void GivenBookAndPersonIds_WhenLendingBookToAPerson_ThenLendsWithValidationChecks(int bookId,
            int personId, bool hitsValidation)
        {
            //Arrange

            //Act
            Exception exception = null;
            LendingDto lending = null;
            if(hitsValidation)
                exception = Assert.Throws<InvalidOperationException>(() => _lendingService.Lend(bookId, personId));
            else
                lending = _lendingService.Lend(bookId, personId);

            //Assert
            if(hitsValidation)
                Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
            else
            {
                Assert.IsNotNull(lending);
                Assert.AreEqual(1, lending.Book.Id);
                Assert.AreEqual(1, lending.Person.Id);
            }
        }
    }
}