using System;

namespace DifferentShapesArea
{
    public class Triangle : IShape
    {
        private double SideA { get; set; } = 0;
        private double SideB { get; set; } = 0;
        private double SideC { get; set; } = 0;

        /// <summary>
        /// Вычисляет полупериметр треугольника
        /// </summary>
        private double HalfPerim
        {
            get
            {
                return ((SideA + SideB + SideC) / 2);
            }
        }

        /// <summary>
        /// Сначала описывает стороны треугольника, если такой не существует-выкидывает исключение
        /// </summary>
        /// <param name="sideA"></param>
        /// <param name="sideB"></param>
        /// <param name="sideC"></param>
        public Triangle(double sideA, double sideB, double sideC)
        {
            SideA = sideA;
            SideB = sideB;
            SideC = sideC;
            if (!IsExistingTriangle())
                throw new ArgumentException("Triangle with this sides is not existing");
        }

        public double GetArea()
        {
            return Math.Sqrt(HalfPerim * (HalfPerim - SideA) * (HalfPerim - SideB) * (HalfPerim - SideC));
        }
        /// <summary>
        /// Проверка на прямоугольность
        /// </summary>
        /// <returns></returns>
        public bool IsRectangular()
        {
            return (Math.Pow(SideC, 2) == Math.Pow(SideA, 2) * Math.Pow(SideB, 2)
                        || Math.Pow(SideA, 2) == Math.Pow(SideC, 2) * Math.Pow(SideB, 2)
                        || Math.Pow(SideB, 2) == Math.Pow(SideA, 2) * Math.Pow(SideC, 2));
        }

        /// <summary>
        /// Проверяет существование треугольника с заданными сторонами
        /// Треугольник с нулевыми сторонами не существует по этому признаку
        /// </summary>
        /// <returns></returns>
        private bool IsExistingTriangle()
        {
            return ((SideA + SideB > SideC) && (SideA + SideC > SideB) && (SideB + SideC > SideA));
        }
    }
}
