using System.ComponentModel.DataAnnotations;

namespace TestTaskWebAPI.Model
{
    public class UserApiKey
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public Experiment Experiment { get; set; }
    }
}
