namespace Domain
{
    public class Forecast
    {
        public Property properties { get; set; }
    }

    public class Property
    {
        public IEnumerable<Period> periods { get; set; }
    }

    public class Period 
    {
        public int number { get; set; }
        public string name { get; set; }
        public DateTimeOffset startTime { get; set; }
        public DateTimeOffset endTime { get; set; }
        public bool isDayTime { get; set; }
        public int temperature { get; set; }
        public string temperatureUnit { get; set; }
        public string temperatureTrend { get; set; }
        public string windSpeed { get; set; }
        public string windDirection { get; set; }
        public string icon { get; set; }
        public string shortForecast { get; set; }
        public string detailedForecast { get; set; }
        public ProbabilityOfPrecipitation probabilityOfPrecipitation { get; set; }
        public Dewpoint dewpoint { get; set; }
        public RelativeHumidity relativeHumidity { get; set; }
    }

    public class ProbabilityOfPrecipitation
    {
        public string unitCode { get; set; }
        public decimal? value { get; set; }
    }

    public class Dewpoint
    {
        public string unitCode { get; set; }
        public decimal? value { get; set; }
    }

    public class RelativeHumidity
    {
        public string unitCode { get; set; }
        public decimal? value { get; set; }
    }
}