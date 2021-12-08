using System;
using System.Drawing;
using System.Collections.Generic;

namespace RetroBricksGame.GameObjects.Abstract
{
    public abstract class Shape
    {
        // Dimensional data
        protected List<Point> Points;
        protected Point Center;
        protected int Width;

        // Abstract methods
        public abstract int ComputeWidth();

        protected Shape(params Point[] points)
        {
            Points = new List<Point>();
            Points.Add(new Point(0, 0)); // Origin
            foreach (Point pt in points)
                Points.Add(pt);

            Center = new Point(0, 0); // Origin
            Width = ComputeWidth();
        }

        public List<Point> GetPoints()
        {
            return Points;
        }

        public Point GetCenter()
        {
            return Center;
        }
        public void SetCenter(Point Center)
        {
            this.Center = Center;
        }

        public int GetWidth()
        {
            return Width;
        }

        // Computes and rotates each point around the origin by -90 degrees with the original
        public void RotateClockwise()
        {
            /*
                new_slope = Tan(Atan(Y/X) - 90deg)
                distance_from_origin = √(Y^2 + X^2)
                x = distance_from_origin / √(1 + new_slope^2)
                y = new_slope * x
             */

            for (int i = 0; i < Points.Count; i++)
            {
                Point pt = Points[i];

                // Case if X is zero
                if (pt.X == 0)
                {
                    pt.X = pt.Y;
                    pt.Y = 0;

                    Points[i] = pt;

                    continue;
                }

                // Case if Y is zero
                if (pt.Y == 0)
                {
                    pt.Y = pt.X * -1;
                    pt.X = 0;

                    Points[i] = pt;

                    continue;
                }

                double current_angle = Math.Atan(pt.Y / pt.X);
                double new_slope = Math.Tan(current_angle - 90);
                double distance_from_origin = Math.Sqrt(Math.Pow(pt.Y, 2) + Math.Pow(pt.X, 2));
                pt.X = (int)Math.Round(distance_from_origin / Math.Sqrt(1 + Math.Pow(new_slope, 2)));
                pt.Y = (int)Math.Round(new_slope * pt.X);

                Points[i] = pt;
            }
        }
        // Emulates computation and rotation of each point around the origin by -90 degrees with the origin
        public List<Point> EmulateRotateClockwise()
        {
            /*
                new_slope = Tan(Atan(Y/X) - 90deg)
                distance_from_origin = √(Y^2 + X^2)
                x = distance_from_origin / √(1 + new_slope^2)
                y = new_slope * x
             */

            // Copy contents
            List<Point> Points = new List<Point>();
            foreach (Point point in this.Points)
                Points.Add(point);

            for (int i = 0; i < Points.Count; i++)
            {
                Point pt = Points[i];

                // Case if X is zero
                if (pt.X == 0)
                {
                    pt.X = pt.Y;
                    pt.Y = 0;

                    Points[i] = pt;

                    continue;
                }

                // Case if Y is zero
                if (pt.Y == 0)
                {
                    pt.Y = pt.X * -1;
                    pt.X = 0;

                    Points[i] = pt;

                    continue;
                }

                double current_angle = Math.Atan(pt.Y / pt.X);
                double new_slope = Math.Tan(current_angle - 90);
                double distance_from_origin = Math.Sqrt(Math.Pow(pt.Y, 2) + Math.Pow(pt.X, 2));
                pt.X = (int)Math.Round(distance_from_origin / Math.Sqrt(1 + Math.Pow(new_slope, 2)));
                pt.Y = (int)Math.Round(new_slope * pt.X);

                Points[i] = pt;
            }

            return Points;
        }
        // Computes and rotates each point around the origin by +90 degrees with the original
        public void RotateCClockwise()
        {
            /*
                new_slope = Tan(Atan(Y/X) + 90deg)
                distance_from_origin = √(Y^2 + X^2)
                x = distance_from_origin / √(1 + new_slope^2)
                y = new_slope * x
             */

            for (int i = 0; i < Points.Count; i++)
            {
                Point pt = Points[i];

                // Case if X is zero
                if (pt.X == 0)
                {
                    pt.X = -1 * pt.Y;
                    pt.Y = 0;

                    Points[i] = pt;

                    continue;
                }

                // Case if Y is zero
                if (pt.Y == 0)
                {
                    pt.Y = pt.X;
                    pt.X = 0;

                    Points[i] = pt;

                    continue;
                }

                double current_angle = Math.Atan(pt.Y / pt.X);
                double new_slope = Math.Tan(current_angle + 90);
                double distance_from_origin = Math.Sqrt(Math.Pow(pt.Y, 2) + Math.Pow(pt.X, 2));
                pt.X = (int)Math.Round(distance_from_origin / Math.Sqrt(1 + Math.Pow(new_slope, 2)));
                pt.Y = (int)Math.Round(new_slope * pt.X);

                Points[i] = pt;
            }
        }
        // Emulates computation and rotation of each point around the origin by +90 degrees with the origin
        public List<Point> EmulateRotateCClockwise()
        {
            /*
                new_slope = Tan(Atan(Y/X) + 90deg)
                distance_from_origin = √(Y^2 + X^2)
                x = distance_from_origin / √(1 + new_slope^2)
                y = new_slope * x
             */

            // Copy contents
            List<Point> Points = new List<Point>();
            foreach (Point point in this.Points)
                Points.Add(point);

            for (int i = 0; i < Points.Count; i++)
            {
                Point pt = Points[i];

                // Case if X is zero
                if (pt.X == 0)
                {
                    pt.X = -1 * pt.Y;
                    pt.Y = 0;

                    Points[i] = pt;

                    continue;
                }

                // Case if Y is zero
                if (pt.Y == 0)
                {
                    pt.Y = pt.X;
                    pt.X = 0;

                    Points[i] = pt;

                    continue;
                }

                double current_angle = Math.Atan(pt.Y / pt.X);
                double new_slope = Math.Tan(current_angle + 90);
                double distance_from_origin = Math.Sqrt(Math.Pow(pt.Y, 2) + Math.Pow(pt.X, 2));
                pt.X = (int)Math.Round(distance_from_origin / Math.Sqrt(1 + Math.Pow(new_slope, 2)));
                pt.Y = (int)Math.Round(new_slope * pt.X);

                Points[i] = pt;
            }

            return Points;
        }

        // Re-aligns the center of the shape towards right by one unit
        public void MoveRight()
        {
            // Delta(+1, 0)

            // Modify Center
            Center.Y++;
        }
        // Emulates re-alignment of all points of the shape towards right by one unit
        public List<Point> EmulateMoveRight()
        {
            // Delta(+1, 0)

            // Copy contents
            List<Point> Points = new List<Point>();
            foreach (Point point in this.Points)
                Points.Add(point);

            for (int i = 0; i < Points.Count; i++)
            {
                Point pt = Points[i];
                pt.Y++;

                Points[i] = pt;
            }

            return Points;
        }
        // Re-aligns the center of the shape towards left by one unit
        public void MoveLeft()
        {
            // Delta(-1, 0)

            // Modify Center
            Center.Y--;
        }
        // Emulates re-alignment of all points of the shape towards left by one unit
        public List<Point> EmulateMoveLeft()
        {
            // Delta(-1, 0)

            // Copy contents
            List<Point> Points = new List<Point>();
            foreach (Point point in this.Points)
                Points.Add(point);

            for (int i = 0; i < Points.Count; i++)
            {
                Point pt = Points[i];
                pt.Y--;

                Points[i] = pt;
            }

            return Points;
        }
    }
}