using System;
using Accord.Imaging;
using Accord.Imaging.Filters;
using Accord.Vision.Detection;
using Accord.Vision.Detection.Cascades;
using System.Drawing;
using Detection;
using System.IO;
using AForge.Vision;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDetectFace()
        {
            Image expected = Image.FromFile(@"...\...\expected.png");
            Class1.DetectFace(@"...\...\lena-color.jpg");
            Image actual = Image.FromFile(@"...\...\file.png");

            MemoryStream ms = new MemoryStream();
            expected.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            string expectedBitmap = Convert.ToBase64String(ms.ToArray());
            ms.Position = 0;
            actual.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            string actualBitmap = Convert.ToBase64String(ms.ToArray());

            Assert.AreEqual(expectedBitmap, actualBitmap);


        }
    }
}
