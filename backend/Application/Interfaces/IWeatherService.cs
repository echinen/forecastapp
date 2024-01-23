namespace Application.Interfaces;

public interface IWeatherService
{
    Task<WeatherDto> GetWeatherData(decimal latitude, decimal longitude);
    Task<Domain.Forecast> GetForecastData(string forecastUrl);
}