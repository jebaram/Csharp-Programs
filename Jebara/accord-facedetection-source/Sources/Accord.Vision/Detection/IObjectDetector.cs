
namespace Accord.Vision.Detection
{
    using System.Drawing;
    using AForge.Imaging;

    public interface IObjectDetector
    { 
        Rectangle[] DetectedObjects { get; }
        Rectangle[] ProcessFrame(UnmanagedImage image);
    }
}
