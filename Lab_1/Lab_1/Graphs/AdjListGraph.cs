﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab_1.Graphs
{
    public class AdjListGraph
    {
        public int Size { get; private set; }
        private LinkedList<int>[] _graph;
        private bool[] _visited;

        public AdjListGraph(int n)
        {
            Size = n;
            _graph = new LinkedList<int>[n];
            for (var i = 0; i < n; i++)
            {
                _graph[i] = new LinkedList<int>();
            }
        }

        public bool HasEdge(int u, int v)
        {
            return _graph[u].Contains(v);
        }

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

        public void AddVertex()
        {
            Size++;
            Array.Resize(ref _graph, Size);
            _graph[Size - 1] = new LinkedList<int>();
            Array.Resize(ref _visited, Size);
        }
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