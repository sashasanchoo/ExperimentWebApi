namespace TestTaskWebAPI.Services.Experement
{
    public class PriceExpResponseValueGenerator : IExperimentResponseValueGenerator
    {
        private const byte STANDART_PRICE = 10;
        private const byte PRICE_INCREASED_X2 = 20;
        private const byte PRICE_INCREASED_X5 = 50;
        private const byte PRICE_DECREASED_X2 = 5;
        private const byte FIRST_GROUP_BORDER = 75;
        private const byte SECOND_GROUP_BORDER = 85;
        private const byte THIRD_GROUP_BORDER = 95;
        private const byte FOURTH_GROUP_BORDER = 100;
        public string GetExperimentValue()
        {
            var option = (this as IExperimentResponseValueGenerator).GetRandomOptionNumber(1, 100);
            var result = 0;
            if (option <= FIRST_GROUP_BORDER)
            {
                result = STANDART_PRICE;
            }
            else if (option > FIRST_GROUP_BORDER && option <= SECOND_GROUP_BORDER)
            {
                result = PRICE_INCREASED_X2;
            }
            else if (option > SECOND_GROUP_BORDER && option <= THIRD_GROUP_BORDER)
            {
                result = PRICE_DECREASED_X2;
            }
            else if (option > THIRD_GROUP_BORDER && option <= FOURTH_GROUP_BORDER)
            {
                result = PRICE_INCREASED_X5;
            }
            return result.ToString();
        }
    }

}
