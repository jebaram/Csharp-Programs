
namespace Accord.Vision.Detection
{
    using System;
    using System.Xml;
    using System.Xml.Serialization;
    using Accord.Imaging;

    [Serializable]
    [XmlRoot("_")]
    public class HaarCascadeStage : ICloneable
    {
        [XmlArray("trees")]
        [XmlArrayItem("_")]
        [XmlArrayItem("_", NestingLevel = 1)]
        public HaarFeatureNode[][] Trees { get; set; }

        [XmlElement("stage_threshold")]
        public double Threshold { get; set; }

        [XmlElement("parent")]
        public int ParentIndex { get; set; }

        [XmlElement("next")]
        public int NextIndex { get; set; }

        public HaarCascadeStage()
        {
        }

        public HaarCascadeStage(double threshold)
        {
            this.Threshold = threshold;
        }

        public HaarCascadeStage(double threshold, int parentIndex, int nextIndex)
        {
            this.Threshold = threshold;
            this.ParentIndex = parentIndex;
            this.NextIndex = nextIndex;
        }

        public bool Classify(IntegralImage2 image, int x, int y, double factor)
        {
            double value = 0;

            // For each feature in the feature tree of the current stage,
            foreach (HaarFeatureNode[] tree in Trees)
            {
                int current = 0;

                do
                {
                    // Get the feature node from the tree
                    HaarFeatureNode node = tree[current];

                    // Evaluate the node's feature
                    double sum = node.Feature.GetSum(image, x, y);

                    // And increase the value accumulator
                    if (sum < node.Threshold * factor)
                    {
                        value += node.LeftValue;
                        current = node.LeftNodeIndex;
                    }
                    else
                    {
                        value += node.RightValue;
                        current = node.RightNodeIndex;
                    }

                } while (current > 0);

                // Stop early if we have already surpassed the stage threshold value.
                //if (value > this.Threshold) return true;
            }

            // After we have evaluated the output for the
            //  current stage, we will check if the value
            //  is still lesser than the stage threshold. 
            if (value < this.Threshold)
            {
                // If it is, the stage has rejected the current
                // image and it doesn't contains our object.
                return false;
            }
            else
            {
                // The stage has accepted the current image
                return true;
            }
        }

        public object Clone()
        {
            HaarFeatureNode[][] newTrees = new HaarFeatureNode[Trees.Length][];

            for (int i = 0; i < newTrees.Length; i++)
            {
                HaarFeatureNode[] tree = Trees[i];
                HaarFeatureNode[] newTree = newTrees[i] =
                    new HaarFeatureNode[tree.Length];

                for (int j = 0; j < newTree.Length; j++)
                    newTree[j] = (HaarFeatureNode)tree[j].Clone();
            }

            HaarCascadeStage r = new HaarCascadeStage();
            r.NextIndex = NextIndex;
            r.ParentIndex = ParentIndex;
            r.Threshold = Threshold;
            r.Trees = newTrees;

            return r;
        }

    }

    [Serializable]
    [XmlRoot(Namespace = "", IsNullable = false, ElementName = "stages")]
    public class HaarCascadeSerializationObject
    {
        [XmlElement("_")]
        public Accord.Vision.Detection.HaarCascadeStage[] Stages { get; set; }
    }
}
