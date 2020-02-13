using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleFour : TestsBase
    {
        [Test]
        [TestCase(InvalidBookId, ValidPersonId)]
        [TestCase(ValidBookId, InvalidPersonId)]
        [TestCase(InvalidBookId, InvalidPersonId)]
        public void GivenInvalidBookOrPersonIds_WhenLendingBookToAPerson_ThenGetException(int bookId, int personId)
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(bookId, personId));
            VerifyMocks(1, bookId == InvalidBookId ? 0 : 1, 0, 0, 0);
        }
    }
}