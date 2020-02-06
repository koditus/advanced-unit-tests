using System;
using Moq;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleOne : TestsBase
    {
        [Test]
        public void GivenValidBookAndPersonIds_WhenLendingBookToAPerson_ThenBookGetsLent()
        {
            //Arrange

            //Act
            var lending = LendingService.Lend(ValidBookId, ValidPersonId);

            //Assert
            Assert.IsNotNull(lending);
            Assert.AreEqual(ValidBookId, lending.Book.Id);
            Assert.AreEqual(ValidPersonId, lending.Person.Id);
            MockBookRepository.Verify(mock => mock.GetOne(ValidBookId), Times.Once);
            MockPersonRepository.Verify(mock => mock.GetOne(ValidPersonId), Times.Once);
            MockLendingRepository.Verify(mock => mock.GetAll(), Times.Once);
        }

        [Test]
        public void GivenInvalidBookAndPersonIds_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(InvalidBookId, InvalidPersonId));
            MockBookRepository.Verify(mock => mock.GetOne(InvalidBookId), Times.Once);
            MockPersonRepository.Verify(mock => mock.GetOne(InvalidPersonId), Times.Never);
            MockLendingRepository.Verify(mock => mock.GetAll(), Times.Never);
        }
    }
}