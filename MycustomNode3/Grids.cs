using Autodesk.DesignScript.Geometry;
using System.Collections.Generic;


namespace MycustomNode3
{
    public class Grids
    {
        public static List<Rectangle> CreateRectGridOnTrigger(int xCount, int yCount, bool execute)
        {
            var rectangles = new List<Rectangle>();

            // Only run if 'execute' is set to true
            if (execute)
            {
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
            }

            return rectangles;
        }



    }
}
