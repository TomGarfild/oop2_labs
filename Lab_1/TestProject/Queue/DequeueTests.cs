using System;
using Lab_1;
using NUnit.Framework;

namespace TestProject.Queue
{
    public class DequeueTests
    {
        [Test]
        public void DequeueOk()
        {
            // Arrange
            var queue = new Queue<int>(10);

            // Act
            for (var i = 0; i < 10; i++)
            {
                queue.EnqueueWithResize(i);
            }

            // Assert
            Assert.AreEqual(10, queue.Count);
            if (!queue.TryDequeue(out var value))
            {
                Assert.Fail("Queue should not be empty!");
            }
            Assert.AreEqual(9, queue.Count);
            Assert.AreEqual(0, value);
        }

        [Test]
        public void DequeueThrows()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act && Arrange
            Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        }
    }
}