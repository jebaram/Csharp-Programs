
namespace Accord.Vision.Detection
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;

    [Serializable]
    public class HaarFeatureNode : ICloneable
    {
        private int rightNodeIndex = -1;
        private int leftNodeIndex = -1;

        //   Gets the threshold for this feature.
        [XmlElement("threshold")]
        public double Threshold { get; set; }

       //   Gets the left value for this feature.
        [XmlElement("left_val")]
        public double LeftValue { get; set; }

        //   Gets the right value for this feature.
        [XmlElement("right_val")]
        public double RightValue { get; set; }

        //   Gets the left node index for this feature.
        [XmlElement("left_node")]
        public int LeftNodeIndex
        {
            get { return leftNodeIndex; }
            set { leftNodeIndex = value; }
        }

        //   Gets the right node index for this feature.
 
        [XmlElement("right_node")]
        public int RightNodeIndex
        {
            get { return rightNodeIndex; }
            set { rightNodeIndex = value; }
        }

        //   Gets the feature associated with this node. 
        [XmlElement("feature", IsNullable = false)]
        public HaarFeature Feature { get; set; }

        //   Constructs a new feature tree node.

        public HaarFeatureNode()
        {
        }
        //   Constructs a new feature tree node.
        public HaarFeatureNode(double threshold, double leftValue, double rightValue, params int[][] rectangles)
            : this(threshold, leftValue, rightValue, false, rectangles)
        {
        }
        //   Constructs a new feature tree node.
        public HaarFeatureNode(double threshold, double leftValue, double rightValue, bool tilted, params int[][] rectangles)
        {
            this.Feature = new HaarFeature(tilted, rectangles);
            this.Threshold = threshold;
            this.LeftValue = leftValue;
            this.RightValue = rightValue;
        }
        //   Creates a new object that is a copy of the current instance.   
        //   A new object that is a copy of this instance.
        public object Clone()
        {
            HaarFeatureNode r = new HaarFeatureNode();

            r.Feature = (HaarFeature)Feature.Clone();
            r.Threshold = Threshold;

            r.RightValue = RightValue;
            r.LeftValue = LeftValue;

            r.LeftNodeIndex = leftNodeIndex;
            r.RightNodeIndex = rightNodeIndex;

            return r;
        }

    }
}
