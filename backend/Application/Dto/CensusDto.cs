namespace Application;

public class CensusDto
{
    public Result result { get; set; }
}

public class Result
{
    public Input input { get; set; }
    public List<AddressMatch> addressMatches { get; set; }
}

public class Input
{
    public Benchmark benchmark { get; set; }
}

public class Benchmark
{
    public bool isDefault { get; set; }
    public string benchmarkDescription { get; set; }
    public string id { get; set; }
    public string benchmarkName { get; set; }
}

public class AddressMatch
{
    public Coordinates coordinates { get; set; }
}

public class Coordinates
{
    public decimal x { get; set; }
    public decimal y { get; set; }
}