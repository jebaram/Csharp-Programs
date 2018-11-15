
namespace Accord.Vision.Detection
{
    using System;
    using System.Collections.Generic;
    using System.Xml;
    using System.Xml.Schema;
    using System.Xml.Serialization;
    using Accord.Imaging;

    [Serializable]
    public sealed class HaarFeature : IXmlSerializable, ICloneable
    {

        //   Gets or sets whether this feature is tilted.
        public bool Tilted { get; set; }

        //   Gets or sets the Haar rectangles for this feature.
        public HaarRectangle[] Rectangles { get; set; }

        //   Constructs a new Haar-like feature.
        public HaarFeature()
        {
            this.Rectangles = new HaarRectangle[2];
        }

        //   Constructs a new Haar-like feature.
        public HaarFeature(params HaarRectangle[] rectangles)
        {
            this.Rectangles = rectangles;
        }
        //   Constructs a new Haar-like feature.
        public HaarFeature(params int[][] rectangles)
            : this(false, rectangles)
        {
        }

        //   Constructs a new Haar-like feature.
        public HaarFeature(bool tilted, params int[][] rectangles)
        {
            this.Tilted = tilted;
            this.Rectangles = new HaarRectangle[rectangles.Length];
            for (int i = 0; i < rectangles.Length; i++)
                this.Rectangles[i] = new HaarRectangle(rectangles[i]);
        }
        //   Gets the sum of the areas of the rectangular features in an integral image.
        public double GetSum(IntegralImage2 image, int x, int y)
        {
            double sum = 0.0;

            if (!Tilted)
            {
                // Compute the sum for a standard feature
                foreach (HaarRectangle rect in Rectangles)
                {
                    sum += image.GetSum(x + rect.ScaledX, y + rect.ScaledY,
                        rect.ScaledWidth, rect.ScaledHeight) * rect.ScaledWeight;
                }
            }
            else
            {
                // Compute the sum for a rotated feature
                foreach (HaarRectangle rect in Rectangles)
                {
                    sum += image.GetSumT(x + rect.ScaledX, y + rect.ScaledY,
                        rect.ScaledWidth, rect.ScaledHeight) * rect.ScaledWeight;
                }
            }

            return sum;
        }

        //   Sets the scale and weight of a Haar-like rectangular feature container.
        public void SetScaleAndWeight(float scale, float weight)
        {
            // manual loop unfolding

            if (Rectangles.Length == 2)
            {
                HaarRectangle a = Rectangles[0];
                HaarRectangle b = Rectangles[1];

                b.ScaleRectangle(scale);
                b.ScaleWeight(weight);

                a.ScaleRectangle(scale);
                a.ScaledWeight = -(b.Area * b.ScaledWeight) / a.Area;
            }
            else // rectangles.Length == 3
            {
                HaarRectangle a = Rectangles[0];
                HaarRectangle b = Rectangles[1];
                HaarRectangle c = Rectangles[2];

                c.ScaleRectangle(scale);
                c.ScaleWeight(weight);

                b.ScaleRectangle(scale);
                b.ScaleWeight(weight);

                a.ScaleRectangle(scale);
                a.ScaledWeight = -(b.Area * b.ScaledWeight
                    + c.Area * c.ScaledWeight) / (a.Area);
            }
        }


        #region IXmlSerializable Members

        XmlSchema IXmlSerializable.GetSchema()
        {
            throw new NotSupportedException();
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            reader.ReadStartElement("feature");

            reader.ReadToFollowing("rects");
            reader.ReadToFollowing("_");

            var rec = new List<HaarRectangle>();
            while (reader.Name == "_")
            {
                string str = reader.ReadElementContentAsString();
                rec.Add(HaarRectangle.Parse(str));

                while (reader.Name != "_" && reader.Name != "tilted" &&
                    reader.NodeType != XmlNodeType.EndElement)
                    reader.Read();
            }

            Rectangles = rec.ToArray();

            reader.ReadToFollowing("tilted", reader.BaseURI);
            Tilted = reader.ReadElementContentAsInt() == 1;

            reader.ReadEndElement();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            throw new NotSupportedException();
        }

        #endregion

        public object Clone()
        {
            HaarRectangle[] newRectangles = new HaarRectangle[Rectangles.Length];
            for (int i = 0; i < newRectangles.Length; i++)
            {
                HaarRectangle rect = Rectangles[i];
                newRectangles[i] = new HaarRectangle(rect.X, rect.Y,
                    rect.Width, rect.Height, rect.Weight);
            }

            HaarFeature r = new HaarFeature();
            r.Rectangles = newRectangles;
            r.Tilted = Tilted;

            return r;
        }

    }

}
