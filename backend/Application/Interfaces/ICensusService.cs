namespace Application.Interfaces;

public interface ICensusService
{
    Task<CensusDto> GetCensusData(string address);
}