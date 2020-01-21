using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DifferentShapesArea.Tests
{
    [TestClass()]
    public class CircleTests
    {
        /// <summary>
        /// Тут или прописывать в expectedResult формулу вычисления площади, тогда они будут точно сходиться 
        /// без 
        /// </summary>
        [TestMethod()]
        public void GetAreaTestTakeRadius_1DividedSqrtPI_Result_1()
        {
            double radius = 1/1.77245385091;
            double expectedResult = 1;
            var circle = new Circle(radius);
            double result = circle.GetArea();
            Assert.AreEqual(expectedResult, Math.Round(result));
        }


        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAreaTestReturnException_RadiusEqualsZero()
        {
            var circle=new Circle(0);
            circle.GetArea();
        }
    }
}