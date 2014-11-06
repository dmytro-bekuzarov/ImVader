// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MinPQ.cs" company="Sigma">
//   It's a totally free software
// </copyright>
// <summary>
//   Defines the MinPq type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ImVader.Utils
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
        /// Stores element at indixes from 1 to newCapacity.
        /// </summary>
        private T[] queue;

        /// <summary>
        /// Number of elements in the queue.
        /// </summary>
        private int capacity;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinPq{T}"/> class. 
        /// Sets inititial newCapacity of the heap to hold given number of elements.
        /// </summary>
        /// <param name="maxN">
        /// The max number of elements in the queue.
        /// </param>
        public MinPq(int maxN)
        {
            queue = new T[maxN + 1];
            capacity = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinPq{T}"/> class.
        /// Sets inititial newCapacity of heap to hold zero elements.
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
        public int Capacity
        {
            get
            {
                return capacity;
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
            return capacity == 0;
        }

        /// <summary>
        /// Returns the smallest element in the queue.
        /// </summary>
        /// <returns>
        /// The smallest element in the queue.
        /// </returns>
        public T Min()
        {
            return queue[1];
        }

        /// <summary>
        /// Add a new element to the priority queue.
        /// </summary>
        /// <param name="x">
        /// The value to add.
        /// </param>
        public void Insert(T x)
        {
            if (capacity >= queue.Length - 1)
            {
                Resize(2 * queue.Length);
            }

            queue[++capacity] = x;
            Swim(capacity);
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
            if (capacity == 0)
            {
                throw new IndexOutOfRangeException("Priority queue underflow");
            }

            Exch(1, capacity);
            T min = queue[capacity--];
            Sink(1);
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
            return queue.Where(t => !Equals(t, default(T)));
        }

        /// <summary>
        /// Helper method to double the capacity of the heap array.
        /// </summary>
        /// <param name="newCapacity">
        /// New capacity of the heap array.
        /// </param>
        private void Resize(int newCapacity)
        {
            var temp = new T[newCapacity];
            for (int i = 0; i <= capacity; i++)
            {
                temp[i] = queue[i];
            }

            queue = temp;
        }

        /// <summary>
        /// Swim the element in the queue.
        /// </summary>
        /// <param name="k">
        /// The element in the queue.
        /// </param>
        private void Swim(int k)
        {
            while (k > 1 && Greater(k / 2, k))
            {
                Exch(k, k / 2);
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
            while (2 * k <= capacity)
            {
                int j = 2 * k;
                if (j < capacity && Greater(j, j + 1)) j++;
                if (!Greater(k, j)) break;
                Exch(k, j);
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
            return queue[i].CompareTo(queue[j]) > 0;
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
            var swap = queue[i];
            queue[i] = queue[j];
            queue[j] = swap;
        }
    }
}