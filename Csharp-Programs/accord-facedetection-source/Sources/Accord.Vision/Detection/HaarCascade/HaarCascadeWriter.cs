﻿
namespace Accord.Vision.Detection
{
    using System;
    using System.Globalization;
    using System.IO;

    public class HaarCascadeWriter
    {
        private TextWriter writer;

        public HaarCascadeWriter(TextWriter stream)
        {
            this.writer = stream;
        }
        public void Write(HaarCascade cascade, string className)
        {
            for (int i = 0; i < cascade.Stages.Length; i++)
                for (int j = 0; j < cascade.Stages[i].Trees.Length; j++)
                    if (cascade.Stages[i].Trees[j].Length != 1)
                        throw new ArgumentException("Only cascades with single node trees are currently supported.");


            writer.WriteLine("// This file has been automatically transcribed by the");
            writer.WriteLine("//");
            writer.WriteLine("// Accord Vision Library");
            writer.WriteLine("// The Accord.NET Framework");
            writer.WriteLine("// http://accord.googlecode.com");
            writer.WriteLine("//");
            writer.WriteLine();
            writer.WriteLine("namespace HaarCascades");
            writer.WriteLine("{");
            writer.WriteLine("    using System.Collections.Generic;");
            writer.WriteLine();
            writer.WriteLine("    /// <summary>");
            writer.WriteLine("    ///   Automatically generated haar-cascade definition");
            writer.WriteLine("    ///   to use with the Accord.NET Framework object detectors.");
            writer.WriteLine("    /// </summary>");
            writer.WriteLine("    /// ");
            writer.WriteLine("    public class {0} : Accord.Vision.Detection.HaarCascade", className);
            writer.WriteLine("    {");
            writer.WriteLine();
            writer.WriteLine("        /// <summary>");
            writer.WriteLine("        ///   Automatically generated transcription");
            writer.WriteLine("        /// </summary>");
            writer.WriteLine("        public {0}()", className);
            writer.WriteLine("            : base({0}, {1})", cascade.Width, cascade.Height);
            writer.WriteLine("        {");
            writer.WriteLine("            List<HaarCascadeStage> stages = new List<HaarCascadeStage>();");
            writer.WriteLine("            List<HaarFeatureNode[]> nodes;");
            writer.WriteLine("            HaarCascadeStage stage;");
            writer.WriteLine();

            if (cascade.HasTiltedFeatures)
            {
                writer.WriteLine("            HasTiltedFeatures = true;");
                writer.WriteLine();
            }

            // Write cascade stages
            for (int i = 0; i < cascade.Stages.Length; i++)
                writeStage(i, cascade.Stages[i]);

            writer.WriteLine();
            writer.WriteLine("            Stages = stages.ToArray();");
            writer.WriteLine("         }");
            writer.WriteLine("    }");
            writer.WriteLine("}");
        }

        private void writeStage(int i, HaarCascadeStage stage)
        {
            writer.WriteLine("            #region Stage {0}", i);
            writer.WriteLine("            stage = new HaarCascadeStage({0}, {1}, {2}); nodes = new List<HaarFeatureNode[]>();",
                stage.Threshold.ToString("R", NumberFormatInfo.InvariantInfo),
                stage.ParentIndex, stage.NextIndex);

            // Write stage trees
            for (int j = 0; j < stage.Trees.Length; j++)
                writeTrees(stage, j);

            writer.WriteLine("            stage.Trees = nodes.ToArray(); stages.Add(stage);");
            writer.WriteLine("            #endregion");
            writer.WriteLine();
        }

        private void writeTrees(HaarCascadeStage stage, int j)
        {
            writer.Write("            nodes.Add(new[] { ");

            // Assume trees have single node
            writeFeature(stage.Trees[j][0]);

            writer.WriteLine(" });");
        }

        private void writeFeature(HaarFeatureNode node)
        {

            writer.Write("new HaarFeatureNode({0}, {1}, {2}, ",
                node.Threshold.ToString("R", NumberFormatInfo.InvariantInfo),
                node.LeftValue.ToString("R", NumberFormatInfo.InvariantInfo),
                node.RightValue.ToString("R", NumberFormatInfo.InvariantInfo));

            if (node.Feature.Tilted)
                writer.Write("true, ");

            // Write haar-like rectangular features
            for (int k = 0; k < node.Feature.Rectangles.Length; k++)
            {
                writeRectangle(node.Feature.Rectangles[k]);

                if (k < node.Feature.Rectangles.Length - 1)
                    writer.Write(", ");
            }

            writer.Write(" )");
        }

        private void writeRectangle(HaarRectangle rectangle)
        {
            writer.Write("new int[] {{ {0}, {1}, {2}, {3}, {4} }}",
                rectangle.X.ToString(NumberFormatInfo.InvariantInfo),
                rectangle.Y.ToString(NumberFormatInfo.InvariantInfo),
                rectangle.Width.ToString(NumberFormatInfo.InvariantInfo),
                rectangle.Height.ToString(NumberFormatInfo.InvariantInfo),
                rectangle.Weight.ToString("R", NumberFormatInfo.InvariantInfo));
        }
    }
}