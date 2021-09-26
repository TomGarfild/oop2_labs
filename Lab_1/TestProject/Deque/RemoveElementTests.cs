using System;
using Lab_1;
using NUnit.Framework;

namespace TestProject.Deque
{
    public class RemoveElementTests
    {
        [Test]
        public void RemoveFirstElementOk()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            for (var i = 0; i < 10; i++)
            {
                deque.AddFirst(i);
            }

            // Assert
            Assert.AreEqual(10, deque.Count);
            if (!deque.TryRemoveFirst(out var value))
            {
                Assert.Fail("Deque should not be empty");
            }
            Assert.AreEqual(9, deque.Count);
            Assert.AreEqual(9, value);
        }

        [Test]
        public void AddLastElementOk()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            for (var i = 0; i < 10; i++)
            {
                deque.AddFirst(i);
            }

            // Assert
            Assert.AreEqual(10, deque.Count);
            if (!deque.TryRemoveLast(out var value))
            {
                Assert.Fail("Deque should not be empty");
            }
            Assert.AreEqual(9, deque.Count);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void RemoveElementThrows()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act && Assert
            Assert.Throws<InvalidOperationException>(() => deque.RemoveFirst());
            Assert.Throws<InvalidOperationException>(() => deque.RemoveLast());
        }
    }
}