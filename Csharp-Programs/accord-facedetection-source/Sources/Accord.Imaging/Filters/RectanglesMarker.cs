
namespace Accord.Imaging.Filters
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using AForge.Imaging;
    using AForge.Imaging.Filters;

    //   Filter to mark (highlight) rectangles in a image.
    public class RectanglesMarker : BaseInPlaceFilter
    {
        private Color markerColor = Color.White;
        private IEnumerable<Rectangle> rectangles;
        private Dictionary<PixelFormat, PixelFormat> formatTranslations = new Dictionary<PixelFormat, PixelFormat>();

        //   Color used to mark pairs.

        public Color MarkerColor
        {
            get { return markerColor; }
            set { markerColor = value; }
        }

        //   The set of rectangles.

        public IEnumerable<Rectangle> Rectangles
        {
            get { return rectangles; }
            set { rectangles = value; }
        }

        //   Format translations dictionary.
        public override Dictionary<PixelFormat, PixelFormat> FormatTranslations
        {
            get { return formatTranslations; }
        }

        public RectanglesMarker(Color markerColor)
            : this(null, markerColor)
        {
        }

        public RectanglesMarker(params Rectangle[] rectangles)
            : this(rectangles, Color.White)
        {
        }

        public RectanglesMarker(IEnumerable<Rectangle> rectangles)
            : this(rectangles, Color.White)
        {
        }

        public RectanglesMarker(IEnumerable<Rectangle> rectangles, Color markerColor)
        {
            this.rectangles = rectangles;
            this.markerColor = markerColor;

            formatTranslations[PixelFormat.Format8bppIndexed] = PixelFormat.Format8bppIndexed;
            formatTranslations[PixelFormat.Format24bppRgb] = PixelFormat.Format24bppRgb;
        }

        protected override void ProcessFilter(UnmanagedImage image)
        {
            // mark all rectangular regions
            foreach (Rectangle rectangle in rectangles)
            {
                Drawing.Rectangle(image, rectangle, markerColor);
            }
        }
    }
}