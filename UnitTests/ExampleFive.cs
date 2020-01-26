using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleFive : TestsBase
    {
        [Test]
        [TestCase(6, 1)]
        [TestCase(1, 6)]
        [TestCase(6, 6)]
        public void GivenInvalidBookOrPersonIds_WhenLendingBookToAPerson_ThenGetException(int bookId, int personId)
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(bookId, personId));
        }

        [Test]
        [TestCase(1, 1)]
        public void GivenBookAndPersonIds_WhenLendingBookToAPerson_ThenLendsWithValidationChecks(int bookId,
            int personId)
        {
            //Arrange

            //Act
            var lending = LendingService.Lend(bookId, personId);

            //Assert
            Assert.IsNotNull(lending);
            Assert.AreEqual(1, lending.Book.Id);
            Assert.AreEqual(1, lending.Person.Id);
        }
    }
}