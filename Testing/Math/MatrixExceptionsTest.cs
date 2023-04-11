using System;
using NUnit.Framework;
using Math;

namespace Testing.Math
{
    [TestFixture]
    public class MatrixExceptionsTest
    {
        [Test]
        public void ImpossibleShape()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var matrix = new Matrix(-1, 2);
            });
        }

        [Test]
        public void OutOfBoundsRowGet()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var matrix = new Matrix(2, 2);
                matrix.GetElement(2, 1);
            });
        }
        
        [Test]
        public void OutOfBoundsColumnGet()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var matrix = new Matrix(2, 2);
                matrix.GetElement(1, 2);
            });
        }

        [Test]
        public void OutOfBoundsRowSet()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var matrix = new Matrix(2, 2);
                matrix.SetElement(2, 1, 1f);
            });
        }
        
        [Test]
        public void OutOfBoundsColumnSet()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var matrix = new Matrix(2, 2);
                matrix.SetElement(1, 2, 1f);
            });
        }

        [Test]
        public void IncompatibleMatrices()
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                double[,] m1Data = {{1, 2, 3}};
                double[,] m2Data = {{1} , {2}};

                var m1 = new Matrix(m1Data);
                var m2 = new Matrix(m2Data);

                var m3 = m1 * m2;
            });
        }
    }
}