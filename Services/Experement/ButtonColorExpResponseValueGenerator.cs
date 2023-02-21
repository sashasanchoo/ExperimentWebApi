namespace TestTaskWebAPI.Services.Experement
{
    public class ButtonColorExpResponseValueGenerator : IExperimentResponseValueGenerator
    {
        private const string FIRST_OPTION = "#FF0000";
        private const string SECOND_OPTION = "#00FF00";
        private const string THIRD_OPTION = "#0000FF";
        private const byte FIRST_OPTION_NUMBER = 1;
        private const byte LAST_OPTION_NUMBER = 3;
        public string GetExperimentValue()
        {
            var option = (this as IExperimentResponseValueGenerator).GetRandomOptionNumber(FIRST_OPTION_NUMBER, LAST_OPTION_NUMBER);
            var value = string.Empty;
            switch (option)
            {
                case 1:
                    {
                        value = FIRST_OPTION;
                        break;
                    }
                case 2:
                    {
                        value = SECOND_OPTION;
                        break;
                    }
                case 3:
                    {
                        value = THIRD_OPTION;
                        break;
                    }
            }
            return value;
        }
    }
}
