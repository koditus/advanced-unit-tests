using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleThree : TestsBase
    {
        [Test]
        public void GivenBookAlreadyLent_WhenReturning_ThenBookGetsReturned()
        {
            //Arrange
            LendingService.Lend(1, 1);

            //Act
            var returnedLending = LendingService.Return(1);

            //Assert
            Assert.IsNotNull(returnedLending);
            Assert.IsNotNull(returnedLending.EndDate);
        }

        [Test]
        public void GivenReturnedBook_WhenReturning_ThenGetException()
        {
            //Arrange
            LendingService.Lend(1, 1);
            LendingService.Return(1);

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => LendingService.Return(1));

            //Assert
            Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
        }

        [Test]
        public void GivenNonLentBook_WhenReturning_ThenGetException()
        {
            //Arrange

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => LendingService.Return(1));

            //Assert
            Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
        }

    }
}