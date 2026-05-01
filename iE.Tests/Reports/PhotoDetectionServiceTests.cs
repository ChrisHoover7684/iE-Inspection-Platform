using iE.Core.Reports.Photos;

namespace iE.Tests.Reports;

public class PhotoDetectionServiceTests
{
    private readonly PhotoDetectionService _service = new();

    [Fact]
    public void DescriptionContainingCorrosion_ReturnsCorrosionDetection()
    {
        var result = _service.Detect(new PhotoDetectionRequest
        {
            PhotoId = "p1",
            Description = "Surface corrosion visible near support"
        });

        Assert.Contains(result.Detections, d => d.Type == PhotoDetectionType.Corrosion);
    }

    [Fact]
    public void WetInsulation_ReturnsCuiOrInsulationDetection()
    {
        var result = _service.Detect(new PhotoDetectionRequest
        {
            PhotoId = "p2",
            Description = "Wet insulation observed around the line"
        });

        Assert.Contains(result.Detections, d => d.Type is PhotoDetectionType.CuiIndicator or PhotoDetectionType.InsulationDamage);
    }

    [Fact]
    public void NormalPhoto_ReturnsNoDetections()
    {
        var result = _service.Detect(new PhotoDetectionRequest
        {
            PhotoId = "p3",
            Description = "General overview photo of area in good condition"
        });

        Assert.Empty(result.Detections);
    }
}
