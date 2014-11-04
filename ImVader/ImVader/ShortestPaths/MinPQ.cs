// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinPQ.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the MinPq type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.ShortestPaths
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a minimum-priority queue.
    /// </summary>
    /// <typeparam name="T">
    /// Type of the onjects stored in queue.
    /// </typeparam>
    public class MinPq<T> where T : IComparable
    {
        /// <summary>
        /// Stores element at indixes from 1 to n.
        /// </summary>
        private T[] pq;

        /// <summary>
        /// Number of elements in the queue.
        /// </summary>
        private int n;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinPq{T}"/> class. 
        /// Sets inititial capacity of the heap to hold given number of elements.
        /// </summary>
        /// <param name="maxN">
        /// The max number of elements in the queue.
        /// </param>
        public MinPq(int maxN)
        {
            pq = new T[maxN + 1];
            this.n = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinPq{T}"/> class.
        /// Sets inititial capacity of heap to hold zero elements.
        /// </summary>
        public MinPq()
            : this(0)
        {
        }

        /// <summary>
        /// Gets the number of elements in queue.
        /// </summary>
        /// <value>
        /// The number of elements in queue.
        /// </value>
        public int Size
        {
            get
            {
                return this.n;
            }
        }

        /// <summary>
        /// Defines whether the queue is empty.
        /// </summary>
        /// <returns>
        ///  True, if the queue is empty, false otherwise.
        /// </returns>
        public bool IsEmpty()
        {
            return this.n == 0;
        }

        /// <summary>
        /// Returns the smallest element in the queue.
        /// </summary>
        /// <returns>
        /// The smallest element in the queue.
        /// </returns>
        public T Min()
        {
            return pq[1];
        }

        /// <summary>
        /// Add a new element to the priority queue.
        /// </summary>
        /// <param name="x">
        /// The value to add.
        /// </param>
        public void Insert(T x)
        {
            if (this.n >= pq.Length - 1)
                this.Resize(2 * pq.Length);
            pq[++this.n] = x;
            this.Swim(this.n);
        }

        /// <summary>
        /// Deletes and returns the minimum element, restoring the heap-order invariant.
        /// </summary>
        /// <returns>
        /// Gets the top of the queue.
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// Returns exception if the queue is empty.
        /// </exception>
        public T DelMin()
        {
            if (this.n == 0)
                throw new IndexOutOfRangeException("Priority queue underflow");
            this.Exch(1, this.n);
            T min = pq[this.n--];
            this.Sink(1);
            return min;
        }

        /// <summary>
        /// Returns the queue as enumerable.
        /// </summary>
        /// <returns>
        /// The queue as enumerable.
        /// </returns>
        public IEnumerable<T> GetAsEnumerable()
        {
            return this.pq.Where(t => !Equals(t, default(T)));
        }

        /// <summary>
        /// Helper method to double the size of the heap array.
        /// </summary>
        /// <param name="capacity">
        /// The capacity.
        /// </param>
        private void Resize(int capacity)
        {
            var temp = new T[capacity];
            for (int i = 0; i <= this.n; i++) temp[i] = pq[i];
            pq = temp;
        }

        /// <summary>
        /// Swim the element in the queue.
        /// </summary>
        /// <param name="k">
        /// The element in the queue.
        /// </param>
        private void Swim(int k)
        {
            while (k > 1 && this.Greater(k / 2, k))
            {
                this.Exch(k, k / 2);
                k = k / 2;
            }
        }

        /// <summary>
        /// Sink the element in the queue.
        /// </summary>
        /// <param name="k">
        /// The element in the queue.
        /// </param>
        private void Sink(int k)
        {
            while (2 * k <= this.n)
            {
                int j = 2 * k;
                if (j < this.n && this.Greater(j, j + 1)) j++;
                if (!this.Greater(k, j)) break;
                this.Exch(k, j);
                k = j;
            }
        }

        /// <summary>
        /// Compares two objects in the queue with index i and j.
        /// </summary>
        /// <param name="i">
        /// The index of the first object.
        /// </param>
        /// <param name="j">
        /// The index of the second object.
        /// </param>
        /// <returns>
        /// The difference between two objects in the queue.
        /// </returns>
        private bool Greater(int i, int j)
        {
            return pq[i].CompareTo(pq[j]) > 0;
        }

        /// <summary>
        /// Exchanges two objects in the queue with index i and j.
        /// </summary>
        /// <param name="i">
        /// The index of the first object.
        /// </param>
        /// <param name="j">
        /// The index of the second object.
        /// </param>
        private void Exch(int i, int j)
        {
            var swap = pq[i];
            pq[i] = pq[j];
            pq[j] = swap;
        }
    }
}