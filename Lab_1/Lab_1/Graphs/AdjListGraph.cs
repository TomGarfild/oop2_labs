using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1.Graphs
{
    /// <summary>
    /// Adjacency List Graph
    /// </summary>
    public class AdjListGraph
    {
        public int Size { get; private set; }
        private LinkedList<int>[] _graph;
        private bool[] _visited;

        /// <summary>
        /// Create graph with <see cref="n"/> vertices.
        /// </summary>
        /// <param name="n">Number of vertices</param>
        public AdjListGraph(int n)
        {
            Size = n;
            _graph = new LinkedList<int>[n];
            for (var i = 0; i < n; i++)
            {
                _graph[i] = new LinkedList<int>();
            }
        }

        /// <summary>
        /// Checks if graph has edge between <see cref="u"/> and <see cref="v"/> vertices.
        /// </summary>
        /// <param name="u">First vertex</param>
        /// <param name="v">Second vertex</param>
        /// <returns><see langword="true"/>  if graph have edge between <see cref="u"/> and <see cref="v"/>, otherwise <see langword="false"/></returns>
        public bool HasEdge(int u, int v)
        {
            return _graph[u].Contains(v);
        }

        /// <summary>
        /// Add edge to graph. Throws <c>ArgumentOutOfRangeException</c> if vertices are out of range. Throws <c>ArgumentException</c> if vertices are the same. Throws <c>InvalidOperationException</c> if edge already exists.
        /// </summary>
        /// <param name="u">First vertex</param>
        /// <param name="v">Second vertex</param>
        public void AddEdge(int u, int v)
        {
            if (u >= Size || v >= Size)
            {
                throw new ArgumentOutOfRangeException("Vertex does not exists!");
            }

            if (u == v)
            {
                throw new ArgumentException("Same vertex!");
            }

            if (_graph[u].Contains(v))
            {
                throw new InvalidOperationException("Edge already exits");
            }

            _graph[u].AddLast(v);
            _graph[v].AddLast(u);
        }

        /// <summary>
        /// Removes edge from graph. Throws <c>ArgumentOutOfRangeException</c> if vertices are out of range. Throws <c>ArgumentException</c> if vertices are the same. Throws <c>InvalidOperationException</c> if edge doesn't exist in graph.
        /// </summary>
        /// <param name="u">First vertex</param>
        /// <param name="v">Second vertex</param>
        public void RemoveEdge(int u, int v)
        {
            if (u >= Size || v >= Size)
            {
                throw new ArgumentOutOfRangeException("Vertex does not exists!");
            }

            if (u == v)
            {
                throw new ArgumentException("Same vertex!");
            }

            if (!_graph[u].Remove(v) || !_graph[v].Remove(u))
            {
                throw new InvalidOperationException("Edge does not exits");
            }
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
                var list = _graph[i];
                foreach (var value in list)
                {
                    Console.Write(" " + value);
                }
            }
        }

        /// <summary>
        /// Adds vertex to graph.
        /// </summary>
        public void AddVertex()
        {
            Size++;
            Array.Resize(ref _graph, Size);
            _graph[Size - 1] = new LinkedList<int>();
            Array.Resize(ref _visited, Size);
        }

        /// <summary>
        /// Removes vertex <see cref="x"/> from the graph. Throws <c>ArgumentOutOfRangeException</c> if vertex doesn't exist in graph.
        /// </summary>
        /// <param name="x">Vertex that is removed</param>
        public void RemoveVertex(int x)
        {
            if (x >= Size)
            {
                throw new ArgumentOutOfRangeException("Vertex not present!");
            }

            for (var i = 0; i < Size; i++)
            {
                _graph[i].Remove(x);
            }

            for (var i = 0; i < Size; i++)
            {
                if (i == x) continue;
                var list = _graph[i];
                var newList = new LinkedList<int>();
                foreach (var value in list) 
                {
                    if (value > x)
                    {
                        newList.AddLast(value - 1);
                    }
                    else
                    {
                        newList.AddLast(value);
                    }
                }

                _graph[i] = newList;
            }

            while (x < Size - 1)
            {
                _graph[x] = _graph[++x];
            }

            Size--;
            Array.Resize(ref _graph, Size);
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
            if (start >= Size)
            {
                throw new ArgumentOutOfRangeException("Vertex not present!");
            }
            _visited[start] = true;

            for (var i = 0; i < Size; i++)
            {
                if (!_graph[start].Contains(i) || _visited[i]) continue;

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
            if (start >= Size || end >= Size)
            {
                throw new ArgumentException("Vertex not present!");
            }
            _visited[start] = true;

            var ans = -1;

            for (var i = 0; i < Size; i++)
            {
                if (!_graph[start].Contains(i) || _visited[i]) continue;

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