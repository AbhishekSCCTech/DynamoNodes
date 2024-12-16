using System;
using System.Collections.Generic;
using Autodesk.DesignScript.Geometry;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomNode2
{
    public class RectangularGrid
    {
        public static List<Rectangle> CreateRectGrid(int xCount, int yCount)
        {
            var rectangles = new List<Rectangle>();

            // Only run if 'execute' is set to true

            double x = 0;
            double y = 0;

            for (int i = 0; i < xCount; i++)
            {
                y++;
                x = 0;

                for (int j = 0; j < yCount; j++)
                {
                    x++;
                    Point pt = Point.ByCoordinates(x, y);
                    Vector vec = Vector.ZAxis();
                    Plane bP = Plane.ByOriginNormal(pt, vec);
                    Rectangle rect = Rectangle.ByWidthLength(bP, 1, 1);
                    rectangles.Add(rect);
                }
            }


            return rectangles;
        }
    }
}
