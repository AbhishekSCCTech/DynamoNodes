using System;
using System.Collections.Generic;
using Autodesk.DesignScript.Geometry;
 
namespace ParametricStairGenerator   
{
    public static class StairGenerator 
    {
        /// <summary>
        /// Generates a stair with the specified parameters.
        /// </summary>
        /// <param name="totalHeight">Total height of the stairs (in meters).</param>
        /// <param name="riserHeight">Height of each riser (in meters).</param>
        /// <param name="treadDepth">Depth of each tread (in meters).</param>
        /// <returns>A list of steps represented as 3D solids.</returns>
        public static List<Solid> GenerateStairs(double totalHeight, double riserHeight, double treadDepth)
        {
            if (totalHeight <= 0 || riserHeight <= 0 || treadDepth <= 0)
            {
                throw new ArgumentException("All dimensions must be positive.");
            }

            int numberOfSteps = (int)Math.Ceiling(totalHeight / riserHeight);
            double actualRiserHeight = totalHeight / numberOfSteps;

            List<Solid> steps = new List<Solid>();
            double currentHeight = 0;

            for (int i = 0; i < numberOfSteps; i++)
            {
                // Define the base points of the step
                Point basePoint = Point.ByCoordinates(0, i * treadDepth, currentHeight);
                Point cornerPoint = Point.ByCoordinates(treadDepth, (i + 1) * treadDepth, currentHeight + actualRiserHeight);

                // Create a rectangular solid for the step
                Solid step = Cuboid.ByCorners(basePoint, cornerPoint);
                steps.Add(step);

                // Update the current height
                currentHeight += actualRiserHeight;
            }

            return steps;
        }
    }
}
