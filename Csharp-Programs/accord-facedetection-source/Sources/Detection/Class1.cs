using System;
using System.Collections.Generic;
using System.Drawing;
using Accord.Imaging.Filters;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;
using System.Diagnostics;
using FaceDetection;
using System.Drawing.Imaging;
using AForge.Imaging;
using AForge.Vision;


namespace Detection
{
    public class Class1
    {

        private static Bitmap picture ;
        static ObjectDetectorSearchMode cbMode;
        static ObjectDetectorScalingMode cbScaling;

        public static string GetPicture(string path)
        {
            picture = new Bitmap(path);
            return path;
        }
        public static string DetectFace(string path)
        {
            GetPicture(path);
            // Process frame to detect objects
            HaarCascade cascade = new FaceHaarCascade();
            HaarObjectDetector detector = new HaarObjectDetector(cascade, 30);
            detector.SearchMode = (ObjectDetectorSearchMode)cbMode;
            detector.ScalingMode = (ObjectDetectorScalingMode)cbScaling;
            detector.ScalingFactor = 1.5f;
            
            Stopwatch sw = Stopwatch.StartNew();

            Rectangle[] objects = detector.ProcessFrame(picture);

            sw.Stop();

            if (objects.Length > 0)
            {
                Console.WriteLine("here");
                RectanglesMarker marker = new RectanglesMarker(objects, Color.Fuchsia);
                picture = marker.Apply(picture);
            }
            if (picture != null)
            {
                Console.WriteLine("trying to print picture");

                /*ImageCodecInfo myici = ImageCodecInfo.GetImageEncoders();
                int numCodecs = myici.GetLength(0);
                Encoder myEncoder = Encoder.Quality;
                EncoderParameters param = new EncoderParameters(1);
                EncoderParameter param1 = new EncoderParameter(myEncoder, 25L);

                picture.Save(@"output.jpg", myici, param);
                */

                picture.Save("file.png", ImageFormat.Png); ;
            }
            return path;
            
        }        
    }
}
