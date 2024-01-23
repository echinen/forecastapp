using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ICensusService _censusService;
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ICensusService censusService, IWeatherService weatherService)
    {
        _logger = logger;
        _censusService = censusService;
        _weatherService = weatherService;
    }

    public async Task<IActionResult> Get()
    {
        try
        {
            string? endereco = HttpContext.Request.Query["address"];

            if (string.IsNullOrWhiteSpace(endereco))
            {
                _logger.LogWarning("The field is empty.");
                return BadRequest("The field address is mandatory.");
            }
            string enderecoCodificado = HttpUtility.UrlEncode(endereco);

            var consusData = await _censusService.GetCensusData(enderecoCodificado);

            // _logger.LogInformation($"consusData, {consusData}");

            var weatherData = await _weatherService.GetWeatherData(consusData.result.addressMatches[0].coordinates.y, consusData.result.addressMatches[0].coordinates.x);

            // _logger.LogInformation($"weatherData, {weatherData}");

            var forecastData = await _weatherService.GetForecastData(weatherData.properties.forecast);

            // _logger.LogInformation($"forecastData, {forecastData}");

            return Ok(forecastData);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Error");
            return StatusCode(500, "Something went wrong. Please contact the system administrator.");
        }
    }
}