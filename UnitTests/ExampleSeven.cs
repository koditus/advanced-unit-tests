using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleSeven : TestsBase
    {
        [Test, Sequential]
        public void GivenInvalidBookOrPerson_WhenLending_ThenValidations(
            [Values(ValidBookId, InvalidBookId, LentBookId, ReturnedBookId)] int bookId, 
            [Values(InvalidPersonId, ValidPersonId, ValidPersonId, InvalidPersonId)] int personId)
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(bookId, personId));
        }

        [Test, Sequential]
        public void GivenValidBookAndPerson_WhenLending_ThenLent([Values(ValidBookId, ReturnedBookId)] int bookId,
            [Values(ValidPersonId, ValidPersonId)] int personId)
        {
            //Arrange

            //Act
            var lending = LendingService.Lend(bookId, personId);

            //Assert
            Assert.IsNotNull(lending);
            Assert.AreEqual(bookId, lending.Book.Id);
            Assert.AreEqual(personId, lending.Person.Id);
            Assert.IsNotNull(lending.StartDate);
            Assert.IsNull(lending.EndDate);
        }

        [Test, Sequential]
        public void GivenInvalidBooksForReturn_WhenReturning_ThenValidations([Values(ValidBookId, InvalidBookId, ReturnedBookId)] int bookId)
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Return(bookId));
        }

        [Test, Sequential]
        public void GivenLentBook_WhenReturning_ThenBookReturned([Values(LentBookId)] int bookId)
        {
            //Arrange

            //Act / Assert
            var lending = LendingService.Return(bookId);

            //Assert
            Assert.IsNotNull(lending);
            Assert.AreEqual(bookId, lending.Book.Id);
            Assert.AreEqual(ValidPersonId, lending.Person.Id);
            Assert.IsNotNull(lending.StartDate);
            Assert.IsNotNull(lending.EndDate);
        }
    }
}