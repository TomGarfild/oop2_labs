using System;
using Lab_1.DataStructures;
using NUnit.Framework;

namespace TestProject.Lab_1.Queue
{
    public class EnqueueTests
    {
        [Test]
        public void EnqueueOk()
        {
            // Arrange
            var queue = new Queue<int>(5);

            // Act
            for (var i = 0; i < 10; i++)
            {
                queue.EnqueueWithResize(i);
            }

            // Assert
            Assert.AreEqual(10, queue.Count);
            if (!queue.TryPeek(out var value))
            {
                Assert.Fail("Queue is empty!");
            }
            Assert.AreEqual(0, value);
        }

        [Test]
        public void EnqueueThrows()
        {
            // Arrange
            var queue = new Queue<int>();

            // Act && Arrange
            Assert.Throws<InvalidOperationException>(() => queue.Enqueue(0));
        }
    }
}