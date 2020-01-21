using System;

namespace DifferentShapesArea
{
    public class Circle : IShape
    {
        private double Radius { get; set; } = 0;
        public Circle(double radius)
        {
            if (radius > 0)
                Radius = radius;
            else
                throw new ArgumentException("Radius cant be zero or less");
        }
        public double GetArea()
        {
            return Math.PI * Math.Pow(Radius, 2);
        }
    }
}
