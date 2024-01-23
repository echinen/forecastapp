using System.Text.Json;
using Application.Interfaces;
using Domain;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Application.Services;

public class WeatherService : IWeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(IHttpClientFactory httpClientFactory, ILogger<WeatherService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<Forecast?> GetForecastData(string forecastUrl)
    {
        _logger.LogInformation($"forecastUrl, {forecastUrl}");

        var httpRequestMessage = new HttpRequestMessage(
     HttpMethod.Get,
     forecastUrl)
        {
            Headers =
            {
                { HeaderNames.Accept, "application/json" },
                { HeaderNames.Host, "api.weather.gov" },
                { HeaderNames.UserAgent, "(myweatherapp.com, contact@myweatherapp.com)" } // Authentication
            }
        };

        var httpClient = _httpClientFactory.CreateClient();
        var forecastResponse = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

        if (forecastResponse.IsSuccessStatusCode)
        {
            var forecastContent = await forecastResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

            _logger.LogInformation($"forecastContent, {forecastContent}");

            var forecastData = JsonSerializer.Deserialize<Forecast>(forecastContent);

            _logger.LogInformation($"forecastData, {forecastData}");

            return forecastData;
        }
        // else
        // {
        //     _logger.LogError($"Forecast API returned status code: {forecastResponse.StatusCode}");
        //     // Handle the error as needed
        //     return StatusCode((int)forecastResponse.StatusCode, "Error retrieving forecast data.");
        // }
        return null;
    }

    public async Task<WeatherDto?> GetWeatherData(decimal latitude, decimal longitude)
    {
        try
        {
            string weatherApiUrl = $"https://api.weather.gov/points/{latitude},{longitude}";
            var httpRequestMessage = new HttpRequestMessage(
      HttpMethod.Get,
      weatherApiUrl)
            {
                Headers =
            {
                { HeaderNames.Accept, "application/geo+json" },
                { HeaderNames.Host, "api.weather.gov" },
                { HeaderNames.UserAgent, "(myweatherapp.com, contact@myweatherapp.com)" } // Authentication
            }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var weatherResponse = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);

            _logger.LogInformation($"weatherResponse, {weatherResponse}");

            if (weatherResponse.IsSuccessStatusCode)
            {
                var weatherContent = await weatherResponse.Content.ReadAsStringAsync().ConfigureAwait(false);

                _logger.LogInformation($"weatherContent, {weatherContent}");

                var weatherData = JsonSerializer.Deserialize<WeatherDto>(weatherContent);

                _logger.LogInformation($"weatherData, {weatherData}");

                return weatherData;
            }
            // else
            // {
            //     _logger.LogError($"Weather API returned status code: {weatherResponse.StatusCode}");
            //     // Handle the error as needed
            //     return StatusCode((int)weatherResponse.StatusCode, "Error retrieving weather data.");
            // }
            return null;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}