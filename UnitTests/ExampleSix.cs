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
                LendingService.Lend(1, 1);

            if(isReturned)
                LendingService.Return(1);

            //Act
            Exception exception = null;
            if(isLending && isLent)
            {
                exception = Assert.Throws<InvalidOperationException>(() => LendingService.Lend(1, 1));
            }
            else
            {
                if (isReturned || !isLent)
                    exception = Assert.Throws<InvalidOperationException>(() => LendingService.Return(1));
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
                LendingService.Lend(1, 1);

            if (isReturned)
                LendingService.Return(1);

            //Act
            LendingDto lending = null;
            LendingDto returnedLending = null;
            if (isLending)
            {
                if (!isLent)
                    lending = LendingService.Lend(1, 1);
            }
            else
            {
                if (!isReturned || isLent)
                    returnedLending = LendingService.Return(1);
            }

            //Assert
            if (isLending)
            {
                Assert.IsNotNull(lending);
                Assert.AreEqual(1, lending.Book.Id);
                Assert.AreEqual(1, lending.Person.Id);
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