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
            var lending = LendingService.Lend(1, 1);

            //Assert
            Assert.IsNotNull(lending);
            Assert.AreEqual(1, lending.Book.Id);
            Assert.AreEqual(1, lending.Person.Id);
        }

        [Test]
        public void GivenInvalidBookAndPersonIds_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act
            var exception = Assert.Throws<InvalidOperationException>(() => LendingService.Lend(6, 6));

            //Assert
            Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
        }
    }
}