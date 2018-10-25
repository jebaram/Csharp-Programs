
namespace Accord.Vision.Detection
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Accord.Vision.Detection.Cascades;

    [Serializable]
    public class HaarCascade : ICloneable
    {
        //   Gets the stages' base width.
        public int Width { get; protected set; }

        //   Gets the stages' base height.
        public int Height { get; protected set; }

        //   Gets the classification stages.
        public HaarCascadeStage[] Stages { get; protected set; }

        public bool HasTiltedFeatures { get; protected set; }

        public HaarCascade(int baseWidth, int baseHeight, HaarCascadeStage[] stages)
        {
            Width = baseWidth;
            Height = baseHeight;
            Stages = stages;

            // check if the classifier has tilted features
            HasTiltedFeatures = checkTiltedFeatures(stages);
        }

        protected HaarCascade(int baseWidth, int baseHeight)
        {
            Width = baseWidth;
            Height = baseHeight;
        }
        private static bool checkTiltedFeatures(HaarCascadeStage[] stages)
        {
            foreach (var stage in stages)
                foreach (var tree in stage.Trees)
                    foreach (var node in tree)
                        if (node.Feature.Tilted == true)
                            return true;
            return false;
        }
        public object Clone()
        {
            HaarCascadeStage[] newStages = new HaarCascadeStage[Stages.Length];
            for (int i = 0; i < newStages.Length; i++)
                newStages[i] = (HaarCascadeStage)Stages[i].Clone();

            HaarCascade r = new HaarCascade(Width, Height);
            r.HasTiltedFeatures = this.HasTiltedFeatures;
            r.Stages = newStages;

            return r;
        }

        public static HaarCascade FromXml(Stream stream)
        {
            return FromXml(new StreamReader(stream));
        }
        public static HaarCascade FromXml(string path)
        {
            return FromXml(new StreamReader(path));
        }

        public static HaarCascade FromXml(TextReader stringReader)
        {
            XmlTextReader xmlReader = new XmlTextReader(stringReader);

            // Gathers the base window size
            xmlReader.ReadToFollowing("size");
            string size = xmlReader.ReadElementContentAsString();

            // Proceeds to load the cascade stages
            xmlReader.ReadToFollowing("stages");
            XmlSerializer serializer = new XmlSerializer(typeof(HaarCascadeSerializationObject));
            var stages = (HaarCascadeSerializationObject)serializer.Deserialize(xmlReader);

            // Process base window size
            string[] s = size.Trim().Split(' ');
            int baseWidth = int.Parse(s[0], CultureInfo.InvariantCulture);
            int baseHeight = int.Parse(s[1], CultureInfo.InvariantCulture);

            // Create and return the new cascade
            return new HaarCascade(baseWidth, baseHeight, stages.Stages);
        }

        public void ToCode(string path, string className)
        {
            ToCode(new StreamWriter(path), className);
        }

        public void ToCode(TextWriter textWriter, string className)
        {
            new HaarCascadeWriter(textWriter).Write(this, className);
        }

    }
}
