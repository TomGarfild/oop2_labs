using System;
using Lab_1;
using NUnit.Framework;

namespace TestProject.Stack
{
    public class StackPopTest
    {
        [Test]
        public void StackPopOk()
        {
            // Arrange
            var stack = new Stack<int>(10);

            // Act
            for (var i = 0; i < 10; i++)
            {
                stack.Push(i);
            }

            // Assert
            Assert.AreEqual(10, stack.Count);
            if (!stack.TryPop(out var value))
            {
                Assert.Fail("Stack should not be empty!");
            }

            Assert.AreEqual(9, value);
        }

        [Test]
        public void StackPopThrow()
        {
            // Arrange
            var stack = new Stack<int>();

            // Act && Assert
            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }
    }
}