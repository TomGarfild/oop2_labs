using Lab_1.Graphs;
using NUnit.Framework;

namespace TestProject.Lab_1.AdjMatrixGraphTests
{
    public class DfsAlgoTests
    {
        [Test]
        public void GraphIsConnectedWorksCorrectly()
        {
            // Arrange
            var g = new AdjMatrixGraph(4);

            // Act
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(1, 2);
            g.AddEdge(1, 3);

            // Assert
            Assert.True(g.IsConnected());
            g.RemoveEdge(1, 3);
            Assert.False(g.IsConnected());
        }

        [Test]
        public void GraphGetDistanceWorksCorrectly()
        {
            // Arrange
            var g = new AdjMatrixGraph(5);

            // Act
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);

            // Assert
            Assert.AreEqual(2, g.GetMinDistance(0, 3));
            Assert.AreEqual(-1, g.GetMinDistance(0, 4));
        }
    }
}