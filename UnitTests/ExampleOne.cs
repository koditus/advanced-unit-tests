﻿using System;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class ExampleOne : LendingServiceTestsBase
    {
        [Test]
        public void GivenValidBookAndPersonIds_WhenLendingBookToAPerson_ThenBookGetsLent()
        {
            //Arrange

            //Act
            var lending = LendingService.Lend(ValidBookId, ValidPersonId);

            //Assert
            AssertBookIsValid(lending, ValidBookId, ValidPersonId);

            VerifyBookRepository_GetOne_IsCalled(1);
            VerifyPersonRepository_GetOne_IsCalled(1);
            VerifyLendingRepository_GetAll_IsCalled(1);
            VerifyLendingRepository_Add_IsCalled(1);

            VerifyNoOtherCalls();
        }

        [Test]
        public void GivenInvalidBookAndPersonIds_WhenLendingBookToAPerson_ThenGetException()
        {
            //Arrange

            //Act / Assert
            Assert.Throws<InvalidOperationException>(() => LendingService.Lend(InvalidBookId, InvalidPersonId));

            VerifyBookRepository_GetOne_IsCalled(1);

            VerifyNoOtherCalls();
        }
    }
}