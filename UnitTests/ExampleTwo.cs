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
            LendingService.Lend(1, 1);
            var exception = Assert.Throws<InvalidOperationException>(() => LendingService.Lend(1, 1));

            //Assert
            Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
        }


        [Test]
        public void GivenInvalidBookIdAndValidPersonId_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => LendingService.Lend(6, 1));

            //Assert
            Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
        }

        [Test]
        public void GivenValidBookIdAndInvalidPersonId_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => LendingService.Lend(1, 6));

            //Assert
            Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
        }

    }
}