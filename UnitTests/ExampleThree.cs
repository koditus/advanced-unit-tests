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
            VerifyBookRepository_GetOne_IsCalled(1);
            VerifyPersonRepository_GetOne_IsCalled(1);
            VerifyLendingRepository_GetAll_IsCalled(2);
            VerifyLendingRepository_Add_IsCalled(1);
            VerifyLendingRepository_Update_IsCalled(1);
        }

        [Test]
        public void GivenReturnedBook_WhenReturning_ThenGetException()
        {
            //Arrange
            LendingService.Lend(ValidBookId, ValidPersonId);
            LendingService.Return(ValidBookId);

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Return(ValidBookId));
            VerifyBookRepository_GetOne_IsCalled(1);
            VerifyPersonRepository_GetOne_IsCalled(1);
            VerifyLendingRepository_GetAll_IsCalled(3);
            VerifyLendingRepository_Add_IsCalled(1);
            VerifyLendingRepository_Update_IsCalled(1);
        }

        [Test]
        public void GivenNonLentBook_WhenReturning_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Return(ValidBookId));
            VerifyBookRepository_GetOne_IsCalled(0);
            VerifyPersonRepository_GetOne_IsCalled(0);
            VerifyLendingRepository_GetAll_IsCalled(1);
            VerifyLendingRepository_Add_IsCalled(0);
            VerifyLendingRepository_Update_IsCalled(0);
        }

    }
}