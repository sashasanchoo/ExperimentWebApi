using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestTaskWebAPI.Data;
using TestTaskWebAPI.Model;
using TestTaskWebAPI.ViewModel;

namespace TestTaskWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly TestTaskContext _context;
        public StatisticsController(TestTaskContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult> GetStatistics()
        {
            var experiments = await _context.Experiments.ToListAsync();
            List<ExperimentResponse> response = new List<ExperimentResponse>();
            experiments.ForEach(experiment => {
                response.Add(new ExperimentResponse { Key = experiment.Key, Value = experiment.Value });
            });
            return Ok(new
            {
                buttonColor = response.Where(r => r.Key == experimentKeys.button_color.ToString()),
                price = response.Where(r => r.Key == experimentKeys.price.ToString()),
                priceOptions = response.Where(r => r.Key == experimentKeys.price.ToString()).Select(r => r.Value).Distinct().ToList(),
                buttonColorOptions = response.Where(r => r.Key == experimentKeys.button_color.ToString()).Select(r => r.Value).Distinct().ToList()
            });
        }
    }
}
