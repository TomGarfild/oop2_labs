using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1.DataStructures
{
    /// <summary>
    /// Deque Data Structure
    /// </summary>
    /// <typeparam name="T">Deque's type</typeparam>
    public class Deque<T> : IEnumerable<T>
    {
        /// <summary>
        /// Node for deque ds.
        /// </summary>
        /// <typeparam name="K">Node's type</typeparam>
        public class DoubleNode<K> : IDisposable
        {
            public DoubleNode(K data)
            {
                Data = data;
            }
            public K Data { get; set; }
            public DoubleNode<K> Previous { get; set; }
            public DoubleNode<K> Next { get; set; }
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }

        private DoubleNode<T> _head;
        private DoubleNode<T> _tail;
        private int _size;

        /// <summary>
        /// Adds last element to deque.
        /// </summary>
        /// <param name="data">Element to add.</param>
        public void AddLast(T data)
        {
            var node = new DoubleNode<T>(data);

            if (_head == null)
            {
                _head = node;
            }
            else
            {
                _tail.Next = node;
                node.Previous = _tail;
            }

            _tail = node;
            _size++;
        }

        /// <summary>
        /// Adds first element to deque.
        /// </summary>
        /// <param name="data">Element to add.</param>
        public void AddFirst(T data)
        {
            var node = new DoubleNode<T>(data);
            var temp = _head;
            node.Next = temp;
            _head = node;
            if (_size == 0)
            {
                _tail = _head;
            }
            else
            {
                temp.Previous = node;
            }

            _size++;
        }

        /// <summary>
        /// Removes first element from the deque. Throws <c>InvalidOperationException</c> if deque is empty.
        /// </summary>
        /// <returns>First element(type <typeparamref name="T"/>), that was deleted</returns>
        public T RemoveFirst()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Deque is empty!");
            }
                
            var output = _head.Data;
            if (_size == 1)
            {
                _head = _tail = null;
            }
            else
            {
                _head = _head.Next;
                _head.Previous = null;
            }

            _size--;
            return output;
        }

        /// <summary>
        /// Removes first element from the deque.
        /// </summary>
        /// <param name="item">First element(type <typeparamref name="T"/>), that was deleted</param>
        /// <returns><see langword="false"/> if deque is empty, otherwise <see langword="true"/> .</returns>
        public bool TryRemoveFirst(out T item)
        {
            if (_size == 0)
            {
                item = default;
                return false;
            }

            var output = _head.Data;
            if (_size == 1)
            {
                _head = _tail = null;
            }
            else
            {
                _head = _head.Next;
                _head.Previous = null;
            }

            _size--;
            item = output;
            return true;
        }

        /// <summary>
        /// Removes last element from the deque. Throws <c>InvalidOperationException</c> if deque is empty.
        /// </summary>
        /// <returns>Last element(type <typeparamref name="T"/>), that was deleted</returns>
        public T RemoveLast()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Deque is empty!");
            }

            var output = _tail.Data;
            if (_size == 1)
            {
                _head = _tail = null;
            }
            else
            {
                _tail = _tail.Previous;
                _tail.Next = null;
            }

            _size--;
            return output;
        }

        /// <summary>
        /// Removes last element from the deque.
        /// </summary>
        /// <param name="item">Last element(type <typeparamref name="T"/>), that was deleted</param>
        /// <returns><see langword="false"/> if deque is empty, otherwise <see langword="true"/> .</returns>
        public bool TryRemoveLast(out T item)
        {
            if (_size == 0)
            {
                item = default;
                return false;
            }

            var output = _tail.Data;
            if (_size == 1)
            {
                _head = _tail = null;
            }
            else
            {
                _tail = _tail.Previous;
                _tail.Next = null;
            }

            _size--;
            item = output;
            return true;
        }

        /// <summary>
        /// Return first element in deque. Throws <c>InvalidOperationException</c>
        /// </summary>
        /// <returns>First element of type <typeparamref name="T"/></returns>
        public T PeekFirst()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Deque is empty!");
            }

            return _head.Data;
        }

        /// <summary>
        /// Return first element in deque.
        /// </summary>
        /// <param name="item">First element of type <typeparamref name="T"/></param>
        /// <returns><see langword="false"/> if deque is empty, otherwise <see langword="true"/> .</returns>
        public bool TryPeekFirst(out T item)
        {
            if (_size == 0)
            {
                item = default;
                return false;
            }

            item = _head.Data;
            return true;
        }

        /// <summary>
        /// Return first element in deque. Throws <c>InvalidOperationException</c>
        /// </summary>
        /// <returns>Last element of type <typeparamref name="T"/></returns>
        public T PeekLast()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Deque is empty!");
            }

            return _tail.Data;
        }

        /// <summary>
        /// Return last element in deque.
        /// </summary>
        /// <param name="item">Last element of type <typeparamref name="T"/></param>
        /// <returns><see langword="false"/> if deque is empty, otherwise <see langword="true"/> .</returns>
        public bool TryPeekLast(out T item)
        {
            if (_size == 0)
            {
                item = default;
                return false;
            }

            item = _tail.Data;
            return true;
        }

        /// <summary>
        /// Number of element in deque.
        /// </summary>
        public int Count => _size;

        /// <summary>
        /// Clears whole deque.
        /// </summary>
        public void Clear()
        {
            while (_head != null)
            {
                var temp = _head;
                _head = _head.Next;
                temp.Dispose();
            }
            _tail = null;
            _size = 0;
        }

        /// <summary>
        /// Checks if element is in the deque.
        /// </summary>
        /// <param name="item">Element to check</param>
        /// <returns><see langword="true"/>  if element is in the deque, otherwise <see langword="false"/></returns>
        public bool Contains(T data)
        {
            return Enumerable.Contains(this, data);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
    }
}