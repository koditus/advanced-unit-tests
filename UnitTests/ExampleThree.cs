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
            VerifyMocks(1, 1, 2, 1, 1);
        }

        [Test]
        public void GivenReturnedBook_WhenReturning_ThenGetException()
        {
            //Arrange
            LendingService.Lend(ValidBookId, ValidPersonId);
            LendingService.Return(ValidBookId);

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Return(ValidBookId));
            VerifyMocks(1, 1, 3, 1, 1);
        }

        [Test]
        public void GivenNonLentBook_WhenReturning_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Return(ValidBookId));
            VerifyMocks(0, 0, 1, 0, 0);
        }

    }
}