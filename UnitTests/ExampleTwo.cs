using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleTwo : LendingServiceTestsBase
    {
        [Test]
        public void GivenBookAlreadyLent_WhenLendingAgain_ThenGetException()
        {
            //Arrange

            //Act
            LendingService.Lend(ValidBookId, ValidPersonId);

            //Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(ValidBookId, ValidPersonId));

            VerifyBookRepository_GetOne_IsCalled(2);
            VerifyPersonRepository_GetOne_IsCalled(2);
            VerifyLendingRepository_GetAll_IsCalled(2);
            VerifyLendingRepository_Add_IsCalled(1);

            VerifyNoOtherCalls();
        }


        [Test]
        public void GivenInvalidBookIdAndValidPersonId_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(InvalidBookId, ValidPersonId));

            VerifyBookRepository_GetOne_IsCalled(1);

            VerifyNoOtherCalls();
        }

        [Test]
        public void GivenValidBookIdAndInvalidPersonId_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(ValidBookId, InvalidPersonId));

            VerifyBookRepository_GetOne_IsCalled(1);
            VerifyPersonRepository_GetOne_IsCalled(1);

            VerifyNoOtherCalls();
        }
    }
}