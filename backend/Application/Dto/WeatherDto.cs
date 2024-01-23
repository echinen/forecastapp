namespace Application;

public class WeatherDto
{
    public Properties properties { get; set; }
}

public class Properties
{
    public string forecast { get; set; }
    public string forecastHourly { get; set; }
    public string forecastGridData { get; set; }
    public string observationStations { get; set; }
}
