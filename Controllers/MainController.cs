using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskWebAPI.Data;
using TestTaskWebAPI.Model;
using TestTaskWebAPI.Services;
using TestTaskWebAPI.Services.Experement;
using TestTaskWebAPI.ViewModel;

namespace TestTaskWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MainController : ControllerBase 
    {
        private readonly ApiKeyService _apiKeyService;
        private readonly TestTaskContext _context;
        private readonly ButtonColorExpResponseValueGenerator _buttonColorExpResponseValueGenerator;
        private readonly PriceExpResponseValueGenerator _priceExpResponseValueGenerator;
        public MainController(ApiKeyService apiKeyService, 
            TestTaskContext context, 
            ButtonColorExpResponseValueGenerator responseValueGenerator,
            PriceExpResponseValueGenerator priceExpResponseValueGenerator)
        {
            _apiKeyService = apiKeyService;
            _context = context;
            _buttonColorExpResponseValueGenerator = responseValueGenerator;
            _priceExpResponseValueGenerator = priceExpResponseValueGenerator;
        }
        // GET: api/Main/ApiKey
        [HttpGet("ApiKey")]
        public async Task<ActionResult> CreateApiKey()
        {
            var apiKey = await IsUserAlreadyHasApiKey(Request.Cookies["key"]);
            if (apiKey == null)
            {
                var token = await _apiKeyService.CreateApiKey();
                Response.Cookies.Append("key", token.Value);
            }
            return NoContent();
        }

        // GET: api/Main/button-color
        [HttpGet("button-color")]
        public async Task<ActionResult<ExperimentResponse>> GetButtonColor(string apiKey)
        {
            var existingKey = await IsUserAlreadyHasApiKey(apiKey);
            if (existingKey == null)
            {
                return BadRequest("Device token is required");
            }
            var result = await GetExperimentResponse(existingKey, apiKey, experimentKeys.button_color);
            return result;
        }

        // GET: api/Main/button-color
        [HttpGet("price")]
        public async Task<ActionResult<ExperimentResponse>> GetPrice(string apiKey)
        {
            var existingKey = await IsUserAlreadyHasApiKey(apiKey);
            if (existingKey == null)
            {
                return BadRequest("Device token is required");
            }
            var result = await GetExperimentResponse(existingKey, apiKey, experimentKeys.price);
            return result;
        }

        //processing user's request based on api-key/device-token and experiment's key
        [NonAction]
        private async Task<ExperimentResponse> GetExperimentResponse(UserApiKey existingKey, string apiKey, experimentKeys experimentKey)
        {
            //if user has not been participated in any experiment
            if (!await IsUserAlreadyHasExpKeyValuePair(apiKey))
            {
                var value = string.Empty;
                switch (experimentKey)
                {
                    case experimentKeys.price:
                        {
                            value = _priceExpResponseValueGenerator.GetExperimentValue();
                            break;
                        }
                    case experimentKeys.button_color:
                        {
                            value = _buttonColorExpResponseValueGenerator.GetExperimentValue();
                            break;
                        }
                }
                await SaveChanges(existingKey, experimentKey.ToString(), value);
                return new ExperimentResponse { Key = experimentKey.ToString(), Value = value };
            }
            //if user already has been participated in any experiment
            else
            {
                return new ExperimentResponse { Key = existingKey.Experiment.Key, Value = existingKey.Experiment.Value };
            }
        }
        [NonAction]
        private async Task SaveChanges(UserApiKey existingKey, string experimentKey, string experimentValue)
        {
            var experiment = new Experiment { Key = experimentKey, Value = experimentValue, UserApiKeyId = existingKey.Id };
            existingKey.Experiment = experiment;
            await _context.SaveChangesAsync();
        }
        //checking if user already has api-key/device-token
        [NonAction]
        private async Task<UserApiKey> IsUserAlreadyHasApiKey(string key)
        {
            var apiKey = await _context.UserApiKeys.FirstOrDefaultAsync(k => k.Value == key);
            return apiKey;
        }

        //checking if user already has been participated in any experiment
        [NonAction]
        private async Task<bool> IsUserAlreadyHasExpKeyValuePair(string? key)
        {
            var apiKey = await IsUserAlreadyHasApiKey(key);
            if(apiKey == null)
            {
                return false;
            }
            var experiment = await _context.Experiments.FirstOrDefaultAsync(e => e.UserApiKeyId == apiKey.Id);
            if (experiment == null)
            {
                return false;
            }
            return true;
        }
    }
}
