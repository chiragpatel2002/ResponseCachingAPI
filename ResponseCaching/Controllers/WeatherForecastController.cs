using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ResponseCaching.Caching;

namespace ResponseCaching.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private ICacheProvider _cacheProvider;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICacheProvider cacheProvider)
        {
            _logger = logger;
            _cacheProvider = cacheProvider;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost("GetTime")]
        //[ResponseCache(Duration = 10, Location = ResponseCacheLocation.Client, VaryByQueryKeys = new string[] { "Id " })]
        public string GetTime(int id)
        {
            if (!_cacheProvider.TryGetValue(CacheKeys.UserList, out string datetime))
            {
                datetime = DateTime.Now.Second.ToString() + "|| Id = " + id.ToString();

                var cacheEntryOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(10),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024
                };
                _cacheProvider.Set(CacheKeys.UserList, datetime, cacheEntryOption);
            }
            return datetime;
        }

        [HttpDelete("DeleteCache")]
        public bool DeleteCache()
        {
            try
            {
                _cacheProvider.Remove(CacheKeys.UserList);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpGet("GetTimeById/{id}")]
        //[ResponseCache(CacheProfileName  = "GetListById")]
        public string GetTimeById(int id)
        {
            return DateTime.Now.Second.ToString() + "|| Id = " + id.ToString();
        }
    }
}
