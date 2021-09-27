using System;
using Lab_1.Graphs;
using NUnit.Framework;

namespace TestProject.AdjMatrixGraphTests
{
    public class VertexTests
    {
        [Test]
        public void AddVertexOk()
        {
            // Arrange
            var g = new AdjMatrixGraph(0);

            // Act
            g.AddVertex();

            // Assert
            Assert.AreEqual(1, g.Size);
        }

        [Test]
        public void RemoveVertexOk()
        {
            // Arrange
            var g = new AdjMatrixGraph(1);

            // Act && Assert
            Assert.AreEqual(1, g.Size);
            g.RemoveVertex(0);
            Assert.AreEqual(0, g.Size);
        }

        [Test]
        public void RemoveVertexThrows()
        {
            // Arrange
            var g = new AdjMatrixGraph(0);

            // Act && Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => g.RemoveVertex(1));
        }
    }
}