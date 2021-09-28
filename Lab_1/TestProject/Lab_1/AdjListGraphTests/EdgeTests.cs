using System;
using Lab_1.Graphs;
using NUnit.Framework;

namespace TestProject.Lab_1.AdjListGraphTests
{
    public class EdgeTests
    {
        [Test]
        public void AddEdgeOk()
        {
            // Arrange
            var g = new AdjListGraph(10);

            // Act
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);

            // Assert
            Assert.True(g.HasEdge(0, 1));
            Assert.True(g.HasEdge(1, 0));
            Assert.True(g.HasEdge(0, 2));
            Assert.False(g.HasEdge(1, 2));
        }

        [Test]
        public void AddEdgeThrows()
        {
            // Arrange
            var g = new AdjListGraph(10);

            // Act
            g.AddEdge(0, 1);

            // Assert
            Assert.Throws<ArgumentException>(() => g.AddEdge(0, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => g.AddEdge(0, 10));
            Assert.Throws<InvalidOperationException>(() => g.AddEdge(0, 1));
        }

        [Test]
        public void RemoveEdgeOk()
        {
            // Arrange
            var g = new AdjListGraph(10);

            // Act
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.RemoveEdge(0, 2);

            // Assert
            Assert.True(g.HasEdge(0, 1));
            Assert.True(g.HasEdge(1, 0));
            Assert.False(g.HasEdge(0, 2));
            Assert.False(g.HasEdge(1, 2));
        }

        [Test]
        public void RemoveEdgeThrows()
        {
            // Arrange
            var g = new AdjListGraph(10);

            // Act && Assert
            Assert.Throws<ArgumentException>(() => g.RemoveEdge(0, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => g.RemoveEdge(0, 10));
            Assert.Throws<InvalidOperationException>(() => g.RemoveEdge(0, 1));
        }
    }
}