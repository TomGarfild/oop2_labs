using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Lab_1
{
    public class Queue<T> : IEnumerable<T>
    {
        private T[] _elements;
        private int _head;
        private int _tail;
        private int _size;

        public Queue()
        {
            _elements = Array.Empty<T>();
        }

        public Queue(int size)
        {
            _elements = new T[size];
        }

        public int Count => _size;

        public void Clear()
        {
            if (_size != 0)
            {
                if (_head < _tail)
                {
                    Array.Clear(_elements, _head, _size);
                }
                else
                {
                    Array.Clear(_elements, _head, _elements.Length - _head);
                    Array.Clear(_elements, 0, _tail);
                }

                _size = 0;
            }

            _head = 0;
            _tail = 0;
        }

        public void Enqueue(T item)
        {
            if (_size == _elements.Length)
            {
                throw new InvalidOperationException("Size of queue is exceeded");
            }

            _elements[_tail] = item;
            MoveNext(ref _tail);
            _size++;
        }

        public void EnqueueWithResize(T item)
        {
            if (_size == _elements.Length)
            {
                Resize(_size + 1);
            }

            _elements[_tail] = item;
            MoveNext(ref _tail);
            _size++;
        }

        public Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public T Dequeue()
        {
            var head = _head;
            var array = _elements;

            if (_size == 0)
            {
                throw new InvalidOperationException("Queue is empty!");
            }

            var removed = array[head];
            MoveNext(ref _head);
            _size--;
            return removed;
        }

        public bool TryDequeue([MaybeNullWhen(false)] out T result)
        {
            var head = _head;
            var array = _elements;

            if (_size == 0)
            {
                result = default!;
                return false;
            }

            result = array[head];
            if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
            {
                array[head] = default!;
            }
            MoveNext(ref _head);
            _size--;
            return true;
        }

        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Queue is empty!");
            }

            return _elements[_head];
        }

        public bool TryPeek([MaybeNullWhen(false)] out T result)
        {
            if (_size == 0)
            {
                result = default!;
                return false;
            }

            result = _elements[_head];
            return true;
        }

        public bool Contains(T item)
        {
            if (_size == 0)
            {
                return false;
            }

            if (_head < _tail)
            {
                return Array.IndexOf(_elements, item, _head, _size) >= 0;
            }

            return
                Array.IndexOf(_elements, item, _head, _elements.Length - _head) >= 0 ||
                Array.IndexOf(_elements, item, 0, _tail) >= 0;
        }

        private void Resize(int size)
        {
            var newArray = new T[size];
            if (_size > 0)
            {
                if (_head < _tail)
                {
                    Array.Copy(_elements, _head, newArray, 0, _size);
                }
                else
                {
                    Array.Copy(_elements, _head, newArray, 0, _elements.Length - _head);
                    Array.Copy(_elements, 0, newArray, _elements.Length - _head, _tail);
                }
            }

            _elements = newArray;
            _head = 0;
            _tail = (_size == size) ? 0 : _size;
        }

        private void MoveNext(ref int index)
        {
            index++;
            if (index == _elements.Length)
            {
                index = 0;
            }
        }

        public struct Enumerator : IEnumerator<T>,
            System.Collections.IEnumerator
        {
            private readonly Queue<T> _queue;
            private int _index;   // -1 = not started, -2 = ended/disposed
            private T? _currentElement;

            internal Enumerator(Queue<T> queue)
            {
                _queue = queue;
                _index = -1;
                _currentElement = default;
            }

            public void Dispose()
            {
                _index = -2;
                _currentElement = default;
            }

            public bool MoveNext()
            {

                if (_index == -2)
                {
                    return false;
                }

                _index++;

                if (_index == _queue._size)
                {
                    _index = -2;
                    _currentElement = default;
                    return false;
                }

                var array = _queue._elements;
                var size = array.Length;

                var arrayIndex = _queue._head + _index;
                if (arrayIndex >= size)
                {
                    arrayIndex -= size;
                }

                _currentElement = array[arrayIndex];
                return true;
            }

            public T Current
            {
                get
                {
                    if (_index < 0)
                    {
                        throw new InvalidOperationException(_index == -2 ? "Enumeration have not started" : "Enumeration have ended");
                    }

                    return _currentElement!;
                }
            }

            object? IEnumerator.Current => Current;

            void IEnumerator.Reset()
            {
                _index = -1;
                _currentElement = default;
            }
        }
    }
}
