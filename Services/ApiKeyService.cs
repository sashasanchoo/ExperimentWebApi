using TestTaskWebAPI.Data;
using TestTaskWebAPI.Model;

namespace TestTaskWebAPI.Services
{
    public class ApiKeyService
    {
        private readonly TestTaskContext _context;
        public ApiKeyService(TestTaskContext context)
        {
            _context = context;
        }
        public async Task<UserApiKey> CreateApiKey()
        {
            var newApiKey = new UserApiKey
            {
                Value = GenerateApiKeyValue()
            };
            _context.UserApiKeys.Add(newApiKey);
            await _context.SaveChangesAsync();
            return newApiKey;
        }
        private string GenerateApiKeyValue()
        {
            return $"{Guid.NewGuid()}-{Guid.NewGuid()}";
        }
    }
}
