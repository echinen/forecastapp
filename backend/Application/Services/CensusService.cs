using System.Text.Json;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Application.Services;

public class CensusService : ICensusService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<CensusService> _logger;

    public CensusService(IHttpClientFactory httpClientFactory, ILogger<CensusService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<CensusDto> GetCensusData(string address)
    {
        try
        {
            string censusApiUrl = $"https://geocoding.geo.census.gov/geocoder/locations/onelineaddress?address={address}&benchmark=4&format=json";

            _logger.LogInformation($"censusApiUrl, {censusApiUrl}");

            var httpRequestMessage = new HttpRequestMessage(
         HttpMethod.Get,
         censusApiUrl)
            {
                Headers =
            {
                { HeaderNames.Accept, "application/json" },
                { HeaderNames.Host, "geocoding.geo.census.gov" }
            }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var censusResponse = await httpClient.GetStringAsync(censusApiUrl).ConfigureAwait(false);

            _logger.LogInformation($"censusResponse, {censusResponse}");

            var censusData = JsonSerializer.Deserialize<CensusDto>(censusResponse);

            _logger.LogInformation($"censusData, {censusData}");

            return censusData;
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}