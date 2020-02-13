using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleOne : TestsBase
    {
        [Test]
        public void GivenValidBookAndPersonIds_WhenLendingBookToAPerson_ThenBookGetsLent()
        {
            //Arrange

            //Act
            var lending = LendingService.Lend(ValidBookId, ValidPersonId);

            //Assert
            Assert.IsNotNull(lending);
            Assert.AreEqual(ValidBookId, lending.Book.Id);
            Assert.AreEqual(ValidPersonId, lending.Person.Id);
            VerifyBookRepository_GetOne_IsCalled(1);
            VerifyPersonRepository_GetOne_IsCalled(1);
            VerifyLendingRepository_GetAll_IsCalled(1);
            VerifyLendingRepository_Add_IsCalled(1);
            VerifyLendingRepository_Update_IsCalled(0);
        }

        [Test]
        public void GivenInvalidBookAndPersonIds_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(InvalidBookId, InvalidPersonId));
            VerifyBookRepository_GetOne_IsCalled(1);
            VerifyPersonRepository_GetOne_IsCalled(0);
            VerifyLendingRepository_GetAll_IsCalled(0);
            VerifyLendingRepository_Add_IsCalled(0);
            VerifyLendingRepository_Update_IsCalled(0);
        }
    }
}