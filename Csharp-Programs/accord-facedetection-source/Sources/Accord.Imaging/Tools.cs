
namespace Accord.Imaging
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using AForge.Imaging;
    public static class Tools
    {

        private const double SQRT2 = 1.4142135623730951;
        //   Compares two rectangles for equality, considering an acceptance threshold.
        public static bool IsEqual(this Rectangle objA, Rectangle objB, int threshold)
        {
            return (Math.Abs(objA.X - objB.X) < threshold) &&
                   (Math.Abs(objA.Y - objB.Y) < threshold) &&
                   (Math.Abs(objA.Width - objB.Width) < threshold) &&
                   (Math.Abs(objA.Height - objB.Height) < threshold);
        }

    }
}
