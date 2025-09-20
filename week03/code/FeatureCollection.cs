public class FeatureCollection
{
    public string? Type { get; set; }
    public List<Feature>? Features { get; set; }
}

public class Feature
{
    public string? Type { get; set; }
    public FeatureProperties? Properties { get; set; }
}

public class FeatureProperties
{
    public string? Place { get; set; }
    public double? Mag { get; set; }
}