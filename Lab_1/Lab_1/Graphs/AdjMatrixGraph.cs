using System;
using System.Linq;

namespace Lab_1.Graphs
{
    public class AdjMatrixGraph
    {
        private int Size { get; set; }
        private int[,] _graph;
        private bool[] _visited;

        public AdjMatrixGraph(int n)
        {
            Size = n;
            _graph = new int[n, n];
        }

        public void AddEdge(int u, int v)
        {
            if (u >= Size || v >= Size)
            {
                throw new ArgumentException("Vertex does not exists!");
            }

            if (u == v)
            {
                throw new ArgumentException("Same vertex!");
            }

            _graph[u, v] = 1;
            _graph[v, u] = 1;
        }

        public void RemoveEdge(int u, int v)
        {
            if (u >= Size || v >= Size)
            {
                throw new ArgumentException("Vertex does not exists!");
            }

            if (u == v)
            {
                throw new ArgumentException("Same vertex!");
            }

            _graph[u, v] = 0;
            _graph[v, u] = 0;
        }

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
        public void RemoveVertex(int x)
        {
            if (x > Size)
            {
                throw new ArgumentException("Vertex not present!");
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

            Size--;
        }

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
                throw new ArgumentException("Vertex not present!");
            }
            _visited[start] = true;

            for (var i = 0; i < Size; i++)
            {
                if (_graph[start, i] != 1 || _visited[i]) continue;

                Dfs(i);
            }
        }

        public int GetDistance(int u, int v)
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