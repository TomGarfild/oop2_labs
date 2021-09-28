using Lab_1;
using Lab_1.DataStructures;
using NUnit.Framework;

namespace TestProject.Deque
{
    public class AddElementTests
    {
        [Test]
        public void AddFirstElementOk()
        {
            // Arrange
            var deque = new Deque<int>();
            
            // Act
            for (var i = 0; i < 10; i++)
            {
                deque.AddFirst(i);
            }

            // Assert
            var val = 10;
            foreach (var value in deque)
            {
                Assert.AreEqual(--val, value);
            }
        }

        [Test]
        public void AddLastElementOk()
        {
            // Arrange
            var deque = new Deque<int>();

            // Act
            for (var i = 0; i < 10; i++)
            {
                deque.AddLast(i);
            }

            // Assert
            var val = 0;
            foreach (var value in deque)
            {
                Assert.AreEqual(val++, value);
            }
        }
    }
}