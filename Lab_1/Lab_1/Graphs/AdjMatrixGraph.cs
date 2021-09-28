using System;
using System.Linq;

namespace Lab_1.Graphs
{
    /// <summary>
    /// Adjacency Matrix Graph
    /// </summary>
    public class AdjMatrixGraph
    {
        public int Size { get; private set; }
        private int[,] _graph;
        private bool[] _visited;

        /// <summary>
        /// Create graph with <see cref="n"/> vertices.
        /// </summary>
        /// <param name="n">Number of vertices</param>
        public AdjMatrixGraph(int n)
        {
            Size = n;
            _graph = new int[n, n];
        }

        /// <summary>
        /// Checks if graph has edge between <see cref="u"/> and <see cref="v"/> vertices.
        /// </summary>
        /// <param name="u">First vertex</param>
        /// <param name="v">Second vertex</param>
        /// <returns><see langword="true"/>  if graph have edge between <see cref="u"/> and <see cref="v"/>, otherwise <see langword="false"/></returns>
        public bool HasEdge(int u, int v)
        {
            if (u >= Size || u < 0 || v >= Size || v < 0)
            {
                return false;
            }

            return _graph[u, v] == 1 || _graph[v, u] == 1;
        }

        /// <summary>
        /// Add edge to graph. Throws <c>ArgumentOutOfRangeException</c> if vertices are out of range. Throws <c>ArgumentException</c> if vertices are the same. Throws <c>InvalidOperationException</c> if edge already exists.
        /// </summary>
        /// <param name="u">First vertex</param>
        /// <param name="v">Second vertex</param>
        public void AddEdge(int u, int v)
        {
            if (u >= Size || u < 0 || v >= Size || v < 0)
            {
                throw new ArgumentOutOfRangeException("Vertex does not exists!");
            }

            if (u == v)
            {
                throw new ArgumentException("Same vertex!");
            }

            if (_graph[u, v] == 1 || _graph[v, u] == 1)
            {
                throw new InvalidOperationException("Edge already exits");
            }

            _graph[u, v] = 1;
            _graph[v, u] = 1;
        }

        /// <summary>
        /// Removes edge from graph. Throws <c>ArgumentOutOfRangeException</c> if vertices are out of range. Throws <c>ArgumentException</c> if vertices are the same. Throws <c>InvalidOperationException</c> if edge doesn't exist in graph.
        /// </summary>
        /// <param name="u">First vertex</param>
        /// <param name="v">Second vertex</param>
        public void RemoveEdge(int u, int v)
        {
            if (u >= Size || u < 0 || v >= Size || v < 0)
            {
                throw new ArgumentOutOfRangeException("Vertex does not exists!");
            }

            if (u == v)
            {
                throw new ArgumentException("Same vertex!");
            }

            if (_graph[u, v] == 0 || _graph[v, u] == 0)
            {
                throw new InvalidOperationException("Edge does not exits");
            }

            _graph[u, v] = 0;
            _graph[v, u] = 0;
        }

        /// <summary>
        /// Prints graph.
        /// </summary>
        public void PrintGraph()
        {
            Console.Write("\n\n Adjacency Matrix:");

            for (var i = 0; i < Size; ++i)
            {
                Console.WriteLine();
                for (var j = 0; j < Size; ++j)
                {
                    Console.Write(" " + _graph[i, j]);
                }
            }
        }

        /// <summary>
        /// Adds vertex to graph.
        /// </summary>
        public void AddVertex()
        {
            var newGraph = new int[Size + 1, Size + 1];
            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    newGraph[i,j] = _graph[i,j];
                }
            }

            Size++;
            _graph = newGraph;
            Array.Resize(ref _visited, Size);
        }

        /// <summary>
        /// Removes vertex <see cref="x"/> from the graph. Throws <c>ArgumentOutOfRangeException</c> if vertex doesn't exist in graph.
        /// </summary>
        /// <param name="x">Vertex that is removed</param>
        public void RemoveVertex(int x)
        {
            if (x >= Size || x < 0)
            {
                throw new ArgumentOutOfRangeException("Vertex not present!");
            }

            while (x < Size - 1)
            {

                for (var i = 0; i < Size; ++i)
                {
                    _graph[i, x] = _graph[i, x + 1];
                }

                for (var i = 0; i < Size; ++i)
                {
                    _graph[x, i] = _graph[x + 1, i];
                }
                x++;
            }

            for (var i = 0; i < Size; ++i)
            {
                _graph[i, Size-1] = 0;
                _graph[Size - 1, i] = 0;
            }

            Size--;
            Array.Resize(ref _visited, Size);
        }

        /// <summary>
        /// Checks if graph is connected
        /// </summary>
        /// <returns><see langword="true"/> if graph is connected, otherwise <see langword="false"/></returns>
        public bool IsConnected()
        {
            _visited = new bool[Size];
            Dfs(0);
            return _visited.All(v => v);
        }

        private void Dfs(int start)
        {
            if (start >= Size || start < 0)
            {
                throw new ArgumentOutOfRangeException("Vertex not present!");
            }
            _visited[start] = true;

            for (var i = 0; i < Size; i++)
            {
                if (_graph[start, i] != 1 || _visited[i]) continue;

                Dfs(i);
            }
        }

        /// <summary>
        /// Gets min distance between vertices <see cref="u"/> and <see cref="v"/>
        /// </summary>
        /// <param name="u">First vertex</param>
        /// <param name="v">Last vertex</param>
        /// <returns>Min distance between vertices <see cref="u"/> and <see cref="v"/></returns>
        public int GetMinDistance(int u, int v)
        {
            _visited = new bool[Size];
            return Dfs(u, v);
        }

        private int Dfs(int start, int end)
        {
            if (start == end)
            {
                return 0;
            }
            if (start >= Size || end >= Size || start < 0 || end < 0)
            {
                throw new ArgumentException("Vertex not present!");
            }
            _visited[start] = true;

            var ans = -1;

            for (var i = 0; i < Size; i++)
            {
                if (_graph[start, i] != 1 || _visited[i]) continue;

                var res = Dfs(i, end);
                if (res == -1)
                {
                    continue;
                }

                ans = (ans == -1 ? res : Math.Min(ans, res)) + 1;
            }

            _visited[start] = false;
            return ans;
        }
    }
}