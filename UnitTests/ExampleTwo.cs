using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleTwo : TestsBase
    {
        [Test]
        public void GivenBookAlreadyLent_WhenLendingAgain_ThenGetException()
        {
            //Arrange

            //Act
            LendingService.Lend(ValidBookId, ValidPersonId);

            //Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(ValidBookId, ValidPersonId));
        }


        [Test]
        public void GivenInvalidBookIdAndValidPersonId_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(InvalidBookId, ValidPersonId));
        }

        [Test]
        public void GivenValidBookIdAndInvalidPersonId_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(ValidBookId, InvalidPersonId));
        }

    }
}