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
        }

        [Test]
        public void GivenInvalidBookAndPersonIds_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => LendingService.Lend(InvalidBookId, InvalidPersonId));

            //Assert
            Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
        }
    }
}