

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PriorityQueue
{

    /// <summary>
    /// IContianer interface for PQueue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IContainer<T>
    {
        /// <summary>
        /// Clears this instance.
        /// </summary>
        void Clear();

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        bool IsEmpty();

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        int Count { get; set; }
    }

    /// <summary>
    /// Interface for PriorityQueue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="PriorityQueue.IContainer{T}" />
    public interface IPriorityQueue<T> : IContainer<T>
                            where T : IComparable
    {
        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Enqueue(T item);

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        void Dequeue();


        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns></returns>
        T Peek();
    }


    /// <summary>
    /// Node<T> class for PQUeue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T> where T : IComparable
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public T Item { get; set; }

        /// <summary>
        /// Gets or sets the next.
        /// </summary>
        /// <value>
        /// The next.
        /// </value>
        public Node<T> Next { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Node{T}"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="link">The link.</param>
        public Node(T value, Node<T> link)
        {
            Item = value;
            Next = link;
        }

    }

    /// <summary>
    /// Starts a new PriorityQueue class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="PriorityQueue.IPriorityQueue{T}" />
    public class PriorityQueue<T> : IPriorityQueue<T>
        where T : IComparable
    {

        private Node<T> top;

        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }


        /// <summary>
        /// Enqueues the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Enqueue(T item)
        {

            if (Count == 0)
            {
                top = new Node<T>(item, null);
            }
            else
            {
                Node<T> current = top;
                Node<T> previous = null;

                while (current != null && current.Item.CompareTo(item) >= 0)
                {
                    previous = current;
                    current = current.Next;

                }

                Node<T> newNode = new Node<T>(item, current);

                if (previous != null)
                {
                    previous.Next = newNode;
                }
                else
                {
                    top = newNode;
                }


            }
            Count++;

        }

        /// <summary>
        /// Dequeues this instance.
        /// </summary>
        /// <exception cref="InvalidOperationException">Cannot remove from empty queue.</exception>
        public void Dequeue()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Cannot remove from empty queue.");
            }
            else
            {
                Node<T> oldNode = top;
                top = top.Next;
                Count--;
                oldNode = null;
            }

        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            top = null;
            Count = 0;
        }

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Cannot obtain top of empty priority queue.</exception>
        public T Peek()
        {
            if (!IsEmpty())
            {
                return top.Item;
            }
            else
            {
                throw new InvalidOperationException("Cannot obtain top of empty priority queue.");
            }
        }

        /// <summary>
        /// Determines whether this instance is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </returns>
        public bool IsEmpty()
        {
            return Count == 0;
        }

    }

}
