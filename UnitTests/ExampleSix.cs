using System;
using NUnit.Framework;
using Services.DTOs;

namespace UnitTests
{
    [TestFixture]
    public class ExampleSix : TestsBase
    {
        [Test]
        [TestCase(false, true, false)]
        [TestCase(false, false, false)]
        [TestCase(true, false, true)]
        public void GivenBookStatus_WhenPerformingAction_ThenActionIsPerformedWithValidations(bool isLent, bool isReturned, bool isLending)
        {
            //Arrange
            if(isLent || isReturned)
                LendingService.Lend(ValidBookId, ValidPersonId);

            if(isReturned)
                LendingService.Return(ValidBookId);

            //Act
            Exception exception = null;
            if(isLending && isLent)
            {
                exception = Assert.Throws<InvalidOperationException>(() => LendingService.Lend(ValidBookId, ValidPersonId));
            }
            else
            {
                if (isReturned || !isLent)
                    exception = Assert.Throws<InvalidOperationException>(() => LendingService.Return(ValidBookId));
            }

            //Assert
            Assert.That(exception, Is.TypeOf(typeof(InvalidOperationException)));
        }

        [Test]
        [TestCase(true, false, false)]
        public void GivenBookStatus_WhenPerformingReturnAction_ThenActionIsPerformedWithValidations(bool isLent, bool isReturned, bool isLending)
        {
            //Arrange
            if (isLent || isReturned)
                LendingService.Lend(ValidBookId, ValidPersonId);

            if (isReturned)
                LendingService.Return(ValidBookId);

            //Act
            LendingDto lending = null;
            LendingDto returnedLending = null;
            if (isLending)
            {
                if (!isLent)
                    lending = LendingService.Lend(ValidBookId, ValidPersonId);
            }
            else
            {
                if (!isReturned || isLent)
                    returnedLending = LendingService.Return(ValidBookId);
            }

            //Assert
            if (isLending)
            {
                Assert.IsNotNull(lending);
                Assert.AreEqual(ValidBookId, lending.Book.Id);
                Assert.AreEqual(ValidPersonId, lending.Person.Id);
                Assert.IsNotNull(lending.StartDate);
            }
            else
            {
                Assert.IsNotNull(returnedLending);
                Assert.IsNotNull(returnedLending.EndDate);
            }
        }
    }
}