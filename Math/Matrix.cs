using System;
using System.Text;
using System.Linq;

namespace Math
{
    public class Matrix
    {
        public int Rows { get; }
        public int Columns { get; }
        
        private double[,] data;

        public Matrix(int rows, int columns)
        {
            IsAtLeastOneByOne(rows, columns);
            
            Rows = rows;
            Columns = columns;

            data = new double[rows, columns];
        }

        private void IsAtLeastOneByOne(int rows, int columns)
        {
            if (rows < 1 || columns < 1)
            {
                throw new ArgumentException("The matrix must be at least 1x1");
            } 
        }
        
        public Matrix(double[,] value)
        {
            Rows = value.GetLength(0);
            Columns = value.GetLength(1);

            data = value;
        }

        public double GetElement(int row, int column)
        {
            IsInBounds(row, column);

            return data[row, column];
        }

        public void SetElement(int row, int column, double value)
        {
            IsInBounds(row, column);

            data[row, column] = value;
        }

        private void IsInBounds(int row, int column)
        {
            if (row < 0 || row >= Rows)
            {
                throw new ArgumentOutOfRangeException(nameof(row), 
                    "Row must be within the size of the matrix");
            }
            if (column < 0 || column >= Columns)
            {
                throw new ArgumentOutOfRangeException(nameof(column), 
                    "Column must be within the size of the matrix");
            }
        }

        public double[,] Get()
        {
            return data;
        }

        public string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int row = 0; row < data.GetLength(0); row++)
            {
                for (int column = 0; column < data.GetLength(1); column++) {
                    result.Append($"{data[row, column]} ");
                }

                result.Append("\n");
            }

            return result.ToString();
        }

        public static Matrix operator *(Matrix m1, Matrix m2)
        {
            AreMatricesMultiplicable(m1, m2);
            
            var m1Data = m1.Get();
            var m2Data = m2.Get();

            var resultant = new Matrix(m1.Rows, m2.Columns);

            for (int row = 0; row < resultant.Rows; row++)
            {
                for (int column = 0; column < resultant.Columns; column++)
                {
                    double sum = 0;
                    
                    for (int count = 0; count < m1.Columns; count++)
                    {
                        sum += m1Data[row, count] * m2Data[count, column];
                    }

                    resultant.SetElement(row, column, sum);
                }
            }

            return resultant;
        }

        private static void AreMatricesMultiplicable(Matrix m1, Matrix m2)
        {
            if (m1.Columns != m2.Rows)
            {
                throw new InvalidOperationException(
                    "Number of columns from the first vector must equal number of rows from the second vector in order to multiply.");
            }
        }
        
        //TODO: make the operator associative! 
        public static Matrix operator *(double factor, Matrix m2)
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Matrix m1, Matrix m2)
        {
            return m1.ToString().Equals(m2.ToString());
        }
        
        public static bool operator !=(Matrix m1, Matrix m2)
        {
            return !m1.ToString().Equals(m2.ToString());
        }
        
        public static Matrix operator +(Matrix m1, Matrix m2)
        {
            throw new NotImplementedException();
        }
    }
}