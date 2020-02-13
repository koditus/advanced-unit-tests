using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleFive : TestsBase
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
            VerifyBookRepository_GetOne_IsCalled(1);
            VerifyPersonRepository_GetOne_IsCalled(bookId == InvalidBookId ? 0 : 1);
            VerifyLendingRepository_GetAll_IsCalled(0);
            VerifyLendingRepository_Add_IsCalled(0);
            VerifyLendingRepository_Update_IsCalled(0);
        }

        [Test]
        [TestCase(ValidBookId, ValidPersonId)]
        public void GivenBookAndPersonIds_WhenLendingBookToAPerson_ThenLendsWithValidationChecks(int bookId,
            int personId)
        {
            //Arrange

            //Act
            var lending = LendingService.Lend(bookId, personId);

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
    }
}