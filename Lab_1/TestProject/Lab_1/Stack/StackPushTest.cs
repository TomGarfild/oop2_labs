using System;
using Lab_1.DataStructures;
using NUnit.Framework;

namespace TestProject.Lab_1.Stack
{
    public class StackPushTest
    {
        [Test]
        public void StackPushOk()
        {
            // Arrange
            var stack = new Stack<int>(5);

            // Act
            for (var i = 0; i < 10; i++)
            {
                stack.PushWithResize(i);
            }

            // Assert
            Assert.AreEqual(10, stack.Count);
            if (!stack.TryPeek(out var value))
            {
                Assert.Fail("Stack is empty!");
            }
            Assert.AreEqual(9, value);
        }

        [Test]
        public void StackPushThrow()
        {
            // Arrange
            var stack = new Stack<int>();

            // Act && Assert
            Assert.Throws<InvalidOperationException>(() => stack.Push(0));
        }
    }
}