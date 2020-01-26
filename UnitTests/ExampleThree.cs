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
            LendingService.Lend(ValidBookId, ValidPersonId);

            //Act
            var returnedLending = LendingService.Return(ValidBookId);

            //Assert
            Assert.IsNotNull(returnedLending);
            Assert.IsNotNull(returnedLending.EndDate);
        }

        [Test]
        public void GivenReturnedBook_WhenReturning_ThenGetException()
        {
            //Arrange
            LendingService.Lend(ValidBookId, ValidPersonId);
            LendingService.Return(ValidBookId);

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Return(ValidBookId));
        }

        [Test]
        public void GivenNonLentBook_WhenReturning_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Return(ValidBookId));
        }

    }
}