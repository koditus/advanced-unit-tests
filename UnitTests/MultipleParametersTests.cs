using NUnit.Framework;
using UnitTestsArticle;

namespace UnitTests
{
    [TestFixture]
    public class MultipleParametersTests
    {
        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void
            GivenDomainLogicService_WhenPassingBusinessValueBiggerThan500_ThenAddsSecondBusinessValueAndMultipliesByThird(
                [Range(501, 550, 1)] int businessValue, [Range(1, 10, 1)] int secondBusinessValue,
                [Range(1, 10, 1)] int thirdBusinessValue)
        {
            //Arrange
            var businessLogicService = new VeryComplexDomainLogicService();

            //Act
            var result =
                businessLogicService.PerformDomainCalculations(businessValue, secondBusinessValue, thirdBusinessValue);

            //Assert
            long expectedResult = (businessValue + secondBusinessValue) * thirdBusinessValue;
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void
            GivenDomainLogicService_WhenPassingBusinessValueLowerThan500_ThenSubtractsSecondBusinessValueAndMultipliesByThird(
                [Range(1, 50, 1)] int businessValue, [Range(1, 10, 1)] int secondBusinessValue,
                [Range(1, 10, 1)] int thirdBusinessValue)
        {
            //Arrange
            var businessLogicService = new VeryComplexDomainLogicService();

            //Act
            var result =
                businessLogicService.PerformDomainCalculations(businessValue, secondBusinessValue, thirdBusinessValue);

            //Assert
            long expectedResult = (businessValue - secondBusinessValue) * thirdBusinessValue;
            Assert.AreEqual(expectedResult, result);
        }
    }
}
