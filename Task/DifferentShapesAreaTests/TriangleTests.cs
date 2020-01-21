using Microsoft.VisualStudio.TestTools.UnitTesting;
using DifferentShapesArea;
using System;
using System.Collections.Generic;
using System.Text;

namespace DifferentShapesArea.Tests
{
    [TestClass()]
    public class TriangleTests
    {
        [TestMethod()]
        public void GetAreaTestSides3_4_5_Return_6()
        {
            double expectedResult = 6;
            var triangle = new Triangle(3, 4, 5);
            var result = triangle.GetArea();
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod()]
        public void IsRectangularTestSides3_4_5()
        {
            var triangle = new Triangle(3, 4, 5);
            Assert.IsTrue(triangle.IsRectangular());
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetAreaTestSides0_1_2_Return_Exception()
        {
            var triangle = new Triangle(0, 1, 2);
            triangle.GetArea();
        }
    }
}