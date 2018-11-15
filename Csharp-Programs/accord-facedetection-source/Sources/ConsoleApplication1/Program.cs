using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Detection;

namespace ConsoleApplication1
{
    class Program
    {
        // per vedere l'immagine in output con il face detection (file.png).
        //si vede nel debug
        static void Main(string[] args)
        {
            Class1.DetectFace(@"lena-color.jpg");
        }
    }
}
