using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1
{
    public class Deque<T> : IEnumerable<T>
    {
        public class DoublyNode<K> : IDisposable
        {
            public DoublyNode(K data)
            {
                Data = data;
            }
            public K Data { get; set; }
            public DoublyNode<K> Previous { get; set; }
            public DoublyNode<K> Next { get; set; }
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
        }

        private DoublyNode<T> _head;
        private DoublyNode<T> _tail;
        private int _size;

        public void AddLast(T data)
        {
            var node = new DoublyNode<T>(data);

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

        public void AddFirst(T data)
        {
            var node = new DoublyNode<T>(data);
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

        public T PeekFirst()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Deque is empty!");
            }

            return _head.Data;
        }

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

        public T PeekLast()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Deque is empty!");
            }

            return _tail.Data;
        }

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

        public int Count => _size;

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