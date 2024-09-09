using Microsoft.AspNetCore.Mvc;
using SubjectTracker.Brokers;
using SubjectTracker.Models;

namespace SubjectTracker.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private IStorageBroker istorageBroker;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStorageBroker storageBroker)
        {
            _logger = logger;
            this.istorageBroker = storageBroker;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task< IEnumerable<WeatherForecast> > Get()
        {
            Subject subject = new Subject();
            subject.Id = Guid.NewGuid();
            subject.Name = "Javohir";
            await istorageBroker.InsertSubjectAsync(subject);
            //StorageBroker storageBroker = new StorageBroker();
            
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
