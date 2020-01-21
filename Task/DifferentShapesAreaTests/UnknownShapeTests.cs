using Microsoft.VisualStudio.TestTools.UnitTesting;
using DifferentShapesArea;
using System;
using System.Collections.Generic;
using System.Text;

namespace DifferentShapesArea.Tests
{
    [TestClass()]
    public class UnknownShapeTests
    {
        [TestMethod()]
        public void GetAreaTestForUnknownShape()
        {
            double expectedResult = 6;
            var unknownShape = new UnknownShape(3, 4, 5);
            var result = unknownShape.GetArea();
            Assert.AreEqual(expectedResult, result);
        }
    }
}