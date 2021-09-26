using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab_1
{
    public class Stack<T> : IEnumerable<T>
    {
        private T[] _elements;
        private int _size;

        public Stack()
        {
            _elements = Array.Empty<T>();
            _size = 0;
        }

        public Stack(int size)
        {
            _elements = new T[size];
            _size = 0;
        }

        public int Count => _size;

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Clear()
        {
            Array.Clear(_elements, 0, _size);
            _size = 0;
        }

        public void Push(T item)
        {
            if (_size == _elements.Length)
            {
                throw new InvalidOperationException("Stack Overflow!");
            }

            _elements[_size++] = item;
        }

        public void PushWithResize(T item)
        {
            if (_size == _elements.Length)
            {
                Resize(_size + 1);
            }

            _elements[_size++] = item;
        }

        public void Resize(int size)
        {
            Array.Resize(ref _elements, size);
        }

        public T Pop()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            return _elements[--_size];
        }

        public bool TryPop(out T item)
        {
            if (_size == 0)
            {
                item = default;
                return false;
            }

            item = _elements[--_size];
            return true;
        }

        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            return _elements[_size - 1];
        }

        public bool TryPeek(out T item)
        {
            if (_size == 0)
            {
                item = default;
                return false;
            }

            item = _elements[_size - 1];
            return true;
        }

        public void Print()
        {
            if (_size == 0)
            {
                Console.WriteLine("Stack is empty!");
            }
        }

        public bool Contains(T item)
        {
            for (var i = 0; i < _size; i++)
            {
                if (_elements[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }
        public struct Enumerator : IEnumerator<T>, System.Collections.IEnumerator
        {
            private readonly Stack<T> _stack;
            private int _index;
            private T? _currentElement;

            internal Enumerator(Stack<T> stack)
            {
                _stack = stack;
                _index = -2;
                _currentElement = default;
            }

            public void Dispose()
            {
                _index = -1;
            }

            public bool MoveNext()
            {
                bool canGetValue;
                if (_index == -2)
                {
                    _index = _stack._size - 1;
                    canGetValue = (_index >= 0);
                    if (canGetValue)
                    {
                        _currentElement = _stack._elements[_index];
                    }

                    return canGetValue;
                }
                if (_index == -1)
                {
                    return false;
                }

                canGetValue = (--_index >= 0);
                _currentElement = canGetValue ? _stack._elements[_index] : default;

                return canGetValue;
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

            object? System.Collections.IEnumerator.Current => Current;

            void IEnumerator.Reset()
            {
                _index = -2;
                _currentElement = default;
            }
        }
    }
}