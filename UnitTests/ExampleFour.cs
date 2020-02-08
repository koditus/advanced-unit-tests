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

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(bookId, personId));
            VerifyMocks(1, bookId == 6 ? 0 : 1, 0, 0, 0);
        }
    }
}