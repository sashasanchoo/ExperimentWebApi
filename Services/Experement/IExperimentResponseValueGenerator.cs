namespace TestTaskWebAPI.Services.Experement
{
    public interface IExperimentResponseValueGenerator
    {
        public string GetExperimentValue();

        public int GetRandomOptionNumber(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }
    }
}
