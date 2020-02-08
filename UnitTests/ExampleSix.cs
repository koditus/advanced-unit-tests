using System;
using NUnit.Framework;
using Services.DTOs;

namespace UnitTests
{
    [TestFixture]
    public class ExampleSix : TestsBase
    {
        [Test]
        [Theory]
        public void GivenBookStatus_WhenPerformingAction_ThenActionIsPerformedWithValidations(bool isLent,
            bool isReturned, bool isLending)
        {
            //Arrange
            if(isLent || isReturned)
                LendingService.Lend(ValidBookId, ValidPersonId);

            if(isReturned)
                LendingService.Return(ValidBookId);

            //Act
            LendingDto lending = null;
            LendingDto returnedLending = null;
            if(isLending)
            {
                if(isLent && !isReturned)
                    Assert.Throws<InvalidOperationException>(() => LendingService.Lend(ValidBookId, ValidPersonId));

                if(!isLent)
                    lending = LendingService.Lend(ValidBookId, ValidPersonId);
            }
            else
            {
                if(isReturned || !isLent)
                    Assert.Throws<InvalidOperationException>(() => LendingService.Return(ValidBookId));

                if(!isReturned && isLent)
                    returnedLending = LendingService.Return(ValidBookId);
            }

            //Assert
            if(isLending)
            {
                if(!isLent)
                {
                    Assert.IsNotNull(lending);
                    Assert.AreEqual(ValidBookId, lending.Book.Id);
                    Assert.AreEqual(ValidPersonId, lending.Person.Id);
                    Assert.IsNotNull(lending.StartDate);
                }
            }
            else
            {
                if(!isReturned && isLent)
                {
                    Assert.IsNotNull(returnedLending);
                    Assert.IsNotNull(returnedLending.EndDate);
                }
            }
        }
    }
}