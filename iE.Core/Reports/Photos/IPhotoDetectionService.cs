namespace iE.Core.Reports.Photos;

public interface IPhotoDetectionService
{
    PhotoDetectionResult Detect(PhotoDetectionRequest request);
}
