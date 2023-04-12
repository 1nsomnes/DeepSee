using System;
using Math;
using NUnit.Framework;

namespace Testing.Math
{
    [TestFixture]
    public class MatrixTests
    {
        private Matrix m1;
        private Matrix m2;
        
        [SetUp]
        public void Setup()
        {
            double[,] m1Data = {{1, 2}, {3, 4}};
            m1 = new Matrix( m1Data );
            
            double[,] m2Data = {{4, 3}, {2, 1}};
            m2 = new Matrix(m2Data);
        }

        [Test]
        public void MatrixMultiplication()
        {
            var m3 = m1 * m2;

            double[,] data1 = {{8, 5}, {20, 13}};
            var resultant1 = new Matrix(data1);
            Assert.That(resultant1 == m3);

            var m4 = m2 * m1;

            double[,] data2 = {{13, 20}, {5, 8}};
            var resultant2 = new Matrix(data2);
            Assert.That(m4 == resultant2);
        }

        [Test]
        public void MatrixScalarMultiplication()
        {
            double factor = 2f;

            var m3 = factor * m1;
            double[,] data1 = {{2, 4}, {6, 8}};
            var resultant1 = new Matrix(data1);
            Assert.That(resultant1 == m3);
            

            var m4 = factor * m2;
            double[,] data2 = {{8, 6}, {4, 2}};
            var resultant2 = new Matrix(data2);
            Assert.That(m4 == resultant2);
        }

        [Test]
        public void MatrixAddition()
        {
            var m3 = m1 + m2;
            double[,] data1 = {{5, 5}, {5, 5}};
            var resultant1 = new Matrix(data1);
            Assert.That(resultant1 == m3);
        }

        [Test]
        public void MatrixSubtraction()
        {
            var m3 = m1 - m2;
            double[,] data1 = {{-3, -1}, {1, 3}};
            var resultant1 = new Matrix(data1);
            Assert.That(resultant1 == m3);
        }
    }
}