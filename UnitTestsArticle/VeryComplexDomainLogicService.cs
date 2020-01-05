namespace UnitTestsArticle
{
    public class VeryComplexDomainLogicService
    {
        private const int BusinessRule = 500;

        public long PerformDomainCalculations(int firstValue, int secondValue, int thirdValue)
        {
            if (firstValue > BusinessRule)
                return (firstValue + secondValue) * thirdValue;

            return (firstValue - secondValue) * thirdValue;
        }
    }
}
