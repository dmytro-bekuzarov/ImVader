// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SquareMatrix.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the SquareMatrix type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Utils
{
    /// <summary>
    /// The square matrix used in matrix-based graph.
    /// </summary>
    /// <typeparam name="T">
    /// A class with public constructor without parameters.
    /// </typeparam>
    public class SquareMatrix<T> where T : new()
    {
        /// <summary>
        /// The values.
        /// </summary>
        private T[,] values;

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareMatrix{T}"/> class.
        /// </summary>
        /// <param name="size">
        /// The inital size of the matrix.
        /// </param>
        public SquareMatrix(int size = 0)
        {
            Size = size;
            values = new T[Size, Size];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    values[i, j] = new T();
                }
            }
        }

        /// <summary>
        /// Gets the size of the matrix.
        /// </summary>
        /// <value>
        /// The size of the matrix.
        /// </value>
        public int Size { get; private set; }

        /// <summary>
        /// Gets or sets the value in the specified cell.
        /// </summary>
        /// <param name="i">
        /// The row index of the cell.
        /// </param>
        /// <param name="j">
        /// The column index of the cell.
        /// </param>
        /// <returns>
        /// The value stored in the cell.
        /// </returns>
        public T this[int i, int j]
        {
            get { return values[i, j]; }
            set { values[i, j] = value; }
        }

        /// <summary>
        /// Adds a new row and a new column to the matrix.
        /// </summary>
        public void Add()
        {
            int newSize = Size + 1;
            var arr = new T[newSize, newSize];
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    arr[i, j] = values[i, j];
                }
            }

            for (int i = 0; i < Size; i++)
            {
                arr[Size, i] = new T();
                arr[i, Size] = new T();
            }

            values = arr;
            Size = newSize;
        }

        /// <summary>
        /// Removes the row and the column with specified index.
        /// </summary>
        /// <param name="index">
        /// The index of the row and column.
        /// </param>
        public void Remove(int index)
        {
            int newSize = Size - 1;
            var arr = new T[newSize, newSize];
            for (int i = 0; i < index; i++)
            {
                for (int j = 0; j < index; j++)
                {
                    arr[i, j] = values[i, j];
                }
            }

            for (int i = index; i < newSize; i++)
            {
                for (int j = index; j < newSize; j++)
                {
                    arr[i, j] = values[i + 1, j + 1];
                }
            }

            values = arr;
            Size = newSize;
        }
    }
}
