using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab_1.DataStructures
{
    /// <summary>
    /// Stack Data Structure
    /// </summary>
    /// <typeparam name="T">Stack's type</typeparam>
    public class Stack<T> : IEnumerable<T>
    {
        private T[] _elements;
        private int _size;

        /// <summary>
        /// Constructor that initializes stack with size 0.
        /// </summary>
        public Stack()
        {
            _elements = Array.Empty<T>();
            _size = 0;
        }

        /// <summary>
        /// Constructor that initializes stack with parameter <see cref="size"/>
        /// </summary>
        /// <param name="size"></param>
        public Stack(int size)
        {
            _elements = new T[size];
            _size = 0;
        }

        /// <summary>
        /// Number of element in stack.
        /// </summary>
        public int Count => _size;

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Clears whole stack.
        /// </summary>
        public void Clear()
        {
            Array.Clear(_elements, 0, _size);
            _size = 0;
        }

        /// <summary>
        /// Adds <see cref="item"/> at the top of stack. Throws <c>InvalidOperationException</c> if current size of stack is at the limit.
        /// </summary>
        /// <param name="item">Pushed item</param>
        public void Push(T item)
        {
            if (_size == _elements.Length)
            {
                throw new InvalidOperationException("Stack Overflow!");
            }

            _elements[_size++] = item;
        }

        /// <summary>
        /// Adds <see cref="item"/> at the top of stack. If Current size of stack is at the limit resizes stack.
        /// </summary>
        /// <param name="item">Pushed item</param>
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

        /// <summary>
        /// Remove top element and return it.  Throws <c>InvalidOperationException</c> if stack i s empty.
        /// </summary>
        /// <returns>Popped element of type <typeparamref name="T"/></returns>
        public T Pop()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            return _elements[--_size];
        }

        /// <summary>
        /// Remove top element.
        /// </summary>
        /// <param name="item">Popped element. Default value if stack is empty.</param>
        /// <returns><c>false</c> if stack is empty, otherwise <c>true</c>.</returns>
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

        /// <summary>
        /// Return top element.  Throws <c>InvalidOperationException</c> if stack i s empty.
        /// </summary>
        /// <returns>Top element of the stack of type <typeparamref name="T"/></returns>
        public T Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("Stack is empty!");
            }

            return _elements[_size - 1];
        }

        /// <summary>
        /// Return top element.
        /// </summary>
        /// <param name="item">Top element of the stack</param>
        /// <returns><c>false</c> if stack is empty, otherwise <c>true</c>.</returns>
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

        /// <summary>
        /// Prints stack.
        /// </summary>
        public void Print()
        {
            if (_size == 0)
            {
                Console.WriteLine("Stack is empty!");
            }
        }

        /// <summary>
        /// Checks if element is in the stack.
        /// </summary>
        /// <param name="item">Element to check</param>
        /// <returns><c>true</c> if element is in the stack, otherwise <c>false</c></returns>
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
        #nullable enable
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