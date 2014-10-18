namespace ImVader.ShortestPaths
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The min pq.
    /// </summary>
    /// <typeparam name="T">
    /// type of the queue param
    /// </typeparam>
    public class MinPq<T>
    where T : IComparable
    {
        /// <summary>
        /// store element at indices 1 to N
        /// </summary>
        private T[] pq;

        /// <summary>
        /// number of elements on priority queue
        /// </summary>
        private int n;

        /// <summary>
        /// Initializes a new instance of the <see cref="MinPq{T}"/> class. 
        /// set inititial capacity of heap to hold given number of elements
        /// </summary>
        /// <param name="maxN">
        /// The max n.
        /// </param>
        public MinPq(int maxN)
        {
            pq = new T[maxN + 1];
            this.n = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MinPq{T}"/> class.
        /// set inititial capacity of heap to hold 0 elements
        /// </summary>
        public MinPq()
            : this(0)
        {
        }

        /// <summary>
        /// is the PQ empty?
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsEmpty()
        {
            return this.n == 0;
        }

        /// <summary>
        /// # elements on PQ
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Size()
        {
            return this.n;
        }

        /// <summary>
        /// smallest element
        /// </summary>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Min()
        {
            return pq[1];
        }

        /// <summary>
        /// add a new element to the priority queue
        /// </summary>
        /// <param name="x">
        /// The x.
        /// </param>
        public void Insert(T x)
        {
            // double size of array if necessary
            if (this.n >= pq.Length - 1) this.Resize(2 * pq.Length);

            // add x, and percolate it up to maintain heap invariant
            pq[++this.n] = x;
            this.Swim(this.n);
        }

        /// <summary>
        /// delete and return the minimum element, restoring the heap-order invariant
        /// </summary>
        /// <returns>
        /// gets the top of the minPQ
        /// The <see cref="T"/>.
        /// </returns>
        /// <exception cref="IndexOutOfRangeException">
        /// returns exception ,if the queue is empty
        /// </exception>
        public T DelMin()
        {
            if (this.n == 0) throw new IndexOutOfRangeException("Priority queue underflow");
            this.Exch(1, this.n);
            T min = pq[this.n--];
            this.Sink(1);
            return min;
        }

        /// <summary>
        /// The get as enumerable.
        /// </summary>
        /// <returns>
        /// The <see>
        ///         <cref>IEnumerable</cref>
        ///     </see>
        ///     .
        /// </returns>
        public IEnumerable<T> GetAsEnumerable()
        {
            return this.pq.Where(t => !Equals(t, default(T)));
        }

        /// <summary>
        /// helper function to double the size of the heap array
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

        /***********************************************************************
         * Helper functions to restore the heap invariant.
         **********************************************************************/

        /// <summary>
        /// The swim.
        /// </summary>
        /// <param name="k">
        /// The k.
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
        /// The sink.
        /// </summary>
        /// <param name="k">
        /// The k.
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

        /***********************************************************************
         * Helper functions for comparisons and swaps.
         **********************************************************************/

        /// <summary>
        /// The greater.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <param name="j">
        /// The j.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private bool Greater(int i, int j)
        {
            return pq[i].CompareTo(pq[j]) > 0;
        }

        /// <summary>
        /// The exch.
        /// </summary>
        /// <param name="i">
        /// The i.
        /// </param>
        /// <param name="j">
        /// The j.
        /// </param>
        private void Exch(int i, int j)
        {
            var swap = pq[i];
            pq[i] = pq[j];
            pq[j] = swap;
        }
    }
}