using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Interfaces;
using Autodesk.DesignScript.Geometry;

namespace MyCustomNodee4
{
    public class CustomNode4
    {
        private double _a;
        private double _b;

        internal CustomNode4(double a, double b)
        {
            _a = a;
            _b = b;
        }


        public static CustomNode4 ByTwoDoubles(double a, double b)
        {
            return new CustomNode4(a, b);
        }

   
        public double A
        {
            get { return _a; }
        }

        public double B
        {
            get { return _b; }
        }

  
        [MultiReturn(new[] { "add", "mult" })]
        public static Dictionary<string, object> ReturnMultiExample(double a, double b)
        {
            return new Dictionary<string, object>
            {
                { "add", (a + b) },
                { "mult", (a * b) }
            };
        }

        public static double DoubleLength(Autodesk.DesignScript.Geometry.Curve curve)
        {
            return curve.Length * 2.0;
        }
    }
}
