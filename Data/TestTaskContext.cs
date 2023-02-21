using Microsoft.EntityFrameworkCore;
using TestTaskWebAPI.Model;

namespace TestTaskWebAPI.Data
{
    public class TestTaskContext:DbContext
    {
        public TestTaskContext(DbContextOptions<TestTaskContext> options) : base(options)
        {

        }
        public DbSet<UserApiKey> UserApiKeys { get; set; }
        public DbSet<Experiment> Experiments { get; set; }
    }
}
