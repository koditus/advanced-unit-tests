using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleFour : TestsBase
    {
        [Test]
        [TestCase(6, 1)]
        [TestCase(1, 6)]
        [TestCase(6, 6)]
        public void GivenInvalidBookOrPersonIds_WhenLendingBookToAPerson_ThenGetException(int bookId, int personId)
        {
            //Arrange

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => LendingService.Lend(bookId, personId));

            //Assert
            Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
        }

    }
}