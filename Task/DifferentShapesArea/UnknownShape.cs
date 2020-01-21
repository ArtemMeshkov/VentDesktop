using System;
using System.Collections.Generic;
using System.Text;

namespace DifferentShapesArea
{
    public class UnknownShape : IShape
    {
        private IShape _shape;
        public double GetArea()
        {
            return _shape.GetArea();
        }

       
        // Перегружаем конструктор для двух случаев
        
        public UnknownShape(double radius)
        {
            _shape = new Circle(radius);
        }

        public UnknownShape(double sideA, double sideB, double sideC)
        {
            _shape = new Triangle(sideA, sideB, sideC);
        }
    }
}
