namespace TestTaskWebAPI.Model
{
    public class Experiment
    {
        public int Id { get; set; }
        public int UserApiKeyId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
    public enum experimentKeys
    {
        button_color = 1,
        price = 2
    }
    
}
