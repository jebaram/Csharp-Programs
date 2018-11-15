
namespace Accord.Vision.Detection
{
    using System;
    using System.Globalization;

    [Serializable]
    public class HaarRectangle : ICloneable
    {

        //   Gets or sets the x-coordinate of this Haar feature rectangle.
        public int X { get; set; }

        //   Gets or sets the y-coordinate of this Haar feature rectangle.
        public int Y { get; set; }

        //   Gets or sets the width of this Haar feature rectangle.
        public int Width { get; set; }

        //   Gets or sets the height of this Haar feature rectangle.
        public int Height { get; set; }

        //   Gets or sets the weight of this Haar feature rectangle.

        public float Weight { get; set; }

        //   Gets or sets the scaled x-coordinate of this Haar feature rectangle.
        public int ScaledX { get; set; }

        //   Gets or sets the scaled y-coordinate of this Haar feature rectangle.
        public int ScaledY { get; set; }

        //   Gets or sets the scaled width of this Haar feature rectangle.
        public int ScaledWidth { get; set; }

        //   Gets or sets the scaled height of this Haar feature rectangle.
        public int ScaledHeight { get; set; }

        //   Gets or sets the scaled weight of this Haar feature rectangle.
        public float ScaledWeight { get; set; }

        //   Constructs a new Haar-like feature rectangle.
        public HaarRectangle(int[] values)
        {
            this.X = values[0];
            this.Y = values[1];
            this.Width = values[2];
            this.Height = values[3];
            this.Weight = values[4];
        }
        //   Constructs a new Haar-like feature rectangle.
        public HaarRectangle(int x, int y, int width, int height, float weight)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Weight = weight;
        }

        private HaarRectangle()
        {
        }
        //   Gets the area of this rectangle.
        public int Area
        {
            get { return ScaledWidth * ScaledHeight; }
        }
        //   Scales the values of this rectangle.
        public void ScaleRectangle(float value)
        {
            ScaledX = (int)(X * value);
            ScaledY = (int)(Y * value);
            ScaledWidth = (int)(Width * value);
            ScaledHeight = (int)(Height * value);
        }
        //   Scales the weight of this rectangle.
        public void ScaleWeight(float scale)
        {
            ScaledWeight = Weight * scale;
        }
        // Converts from a string representation. 
        public static HaarRectangle Parse(string s)
        {
            string[] values = s.Trim().Split(' ');

            int x = int.Parse(values[0], CultureInfo.InvariantCulture);
            int y = int.Parse(values[1], CultureInfo.InvariantCulture);
            int w = int.Parse(values[2], CultureInfo.InvariantCulture);
            int h = int.Parse(values[3], CultureInfo.InvariantCulture);
            float weight = float.Parse(values[4], CultureInfo.InvariantCulture);

            return new HaarRectangle(x, y, w, h, weight);
        }

        //   Creates a new object that is a copy of the current instance.
      //   A new object that is a copy of this instance.
        public object Clone()
        {
            HaarRectangle r = new HaarRectangle();
            r.Height = Height;
            r.ScaledHeight = ScaledHeight;
            r.ScaledWeight = ScaledWeight;
            r.ScaledWidth = ScaledWidth;
            r.ScaledX = ScaledX;
            r.ScaledY = ScaledY;
            r.Weight = Weight;
            r.Width = Width;
            r.X = X;
            r.Y = Y;

            return r;
        }

    }
}
