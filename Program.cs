using System;
using System.Collections.Generic;

public class KruskalAlgorithm
{
    private int[] parent;

    private int Find(int i)
    {
        while (parent[i] != i)
        {
            i = parent[i];
        }

        return i;
    }

    private void Union(int i, int j)
    {
        int iParent = Find(i);
        int jParent = Find(j);

        parent[iParent] = jParent;
    }

    public List<Edge> Kruskal(List<Edge> edges, int numVertices)
    {
        parent = new int[numVertices];
        List<Edge> mst = new List<Edge>();

        for (int i = 0; i < numVertices; i++)
        {
            parent[i] = i;
        }

        edges.Sort();

        foreach (Edge edge in edges)
        {
            int u = edge.StartVertex;
            int v = edge.EndVertex;

            if (Find(u) != Find(v))
            {
                mst.Add(edge);
                Union(u, v);
            }
        }

        return mst;
    }
}

public class PrimAlgorithm
{
    public List<Edge> Prim(List<List<Edge>> adjacencyList, int numVertices)
    {
        List<Edge> mst = new List<Edge>();
        bool[] visited = new bool[numVertices];
        int[] key = new int[numVertices];
        int[] parent = new int[numVertices];

        for (int i = 0; i < numVertices; i++)
        {
            key[i] = int.MaxValue;
            visited[i] = false;
        }
        key[0] = 0;
        parent[0] = -1;

        for (int i = 0; i < numVertices - 1; i++)
        {
            int u = MinimumKeyVertex(key, visited);
            visited[u] = true;

            foreach (Edge edge in adjacencyList[u])
            {
                int v = edge.EndVertex;

                if (!visited[v] && edge.Weight < key[v])
                {
                    parent[v] = u;
                    key[v] = edge.Weight;
                }
            }
        }
        for (int i = 1; i < numVertices; i++)
        {
            mst.Add(new Edge(parent[i], i, key[i]));
        }

        return mst;
    }
    private int MinimumKeyVertex(int[] key, bool[] visited)
    {
        int min = int.MaxValue;
        int minIndex = -1;

        for (int i = 0; i < key.Length; i++)
        {
            if (!visited[i] && key[i] < min)
            {
                min = key[i];
                minIndex = i;
            }
        }

        return minIndex;
    }
}
public class Edge : IComparable<Edge>
{
    public int StartVertex { get; set; }
    public int EndVertex { get; set; }
    public int Weight { get; set; }

    public Edge(int startVertex, int endVertex, int weight)
    {
        StartVertex = startVertex;
        EndVertex = endVertex;
        Weight = weight;
    }

    public int CompareTo(Edge other)
    {
        return Weight.CompareTo(other.Weight);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Алгоритм Краскала");
        List<Edge> edges = new List<Edge>();
        edges.Add(new Edge(1, 2, 6));
        edges.Add(new Edge(2, 3, 9));
        edges.Add(new Edge(3, 4, 12));
        edges.Add(new Edge(1, 5, 2));
        edges.Add(new Edge(2, 6, 5));
        edges.Add(new Edge(3, 7, 8));
        edges.Add(new Edge(4, 8, 4));
        edges.Add(new Edge(2, 5, 1));
        edges.Add(new Edge(2, 7, 3));
        edges.Add(new Edge(4, 7, 3));
        edges.Add(new Edge(5, 6, 7));
        edges.Add(new Edge(6, 7, 4));
        edges.Add(new Edge(7, 8, 16));

        foreach (Edge edge in edges)
        {
            edge.StartVertex = edge.StartVertex - 1;
            edge.EndVertex = edge.EndVertex - 1;
        }

        KruskalAlgorithm kruskal = new KruskalAlgorithm();
        List<Edge> mst = kruskal.Kruskal(edges, 8);

        foreach (Edge edge in mst)
        {
            Console.WriteLine("{0} - {1}: {2}", edge.StartVertex + 1, edge.EndVertex + 1, edge.Weight);
        }
        Console.ReadLine();


        Console.WriteLine("Алгоритм Прима");
        List<List<Edge>> adjacencyList = new List<List<Edge>>();

        for (int i = 0; i < 8; i++)
        {
            adjacencyList.Add(new List<Edge>());
        }
        adjacencyList[0].Add(new Edge(1, 2, 6));
        adjacencyList[0].Add(new Edge(1, 5, 2));
        adjacencyList[1].Add(new Edge(2, 1, 6));
        adjacencyList[1].Add(new Edge(2, 3, 9));
        adjacencyList[1].Add(new Edge(2, 5, 1));
        adjacencyList[1].Add(new Edge(2, 6, 5));
        adjacencyList[1].Add(new Edge(2, 7, 3));
        adjacencyList[2].Add(new Edge(3, 2, 9));
        adjacencyList[2].Add(new Edge(3, 4, 12));
        adjacencyList[2].Add(new Edge(3, 7, 8));
        adjacencyList[3].Add(new Edge(4, 3, 12));
        adjacencyList[3].Add(new Edge(4, 7, 3));
        adjacencyList[3].Add(new Edge(4, 8, 4));
        adjacencyList[4].Add(new Edge(5, 1, 2));
        adjacencyList[4].Add(new Edge(5, 2, 1));
        adjacencyList[4].Add(new Edge(5, 6, 7));
        adjacencyList[5].Add(new Edge(6, 5, 7));
        adjacencyList[5].Add(new Edge(6, 2, 5));
        adjacencyList[5].Add(new Edge(6, 7, 4));
        adjacencyList[6].Add(new Edge(7, 6, 4));
        adjacencyList[6].Add(new Edge(7, 2, 3));
        adjacencyList[6].Add(new Edge(7, 3, 8));
        adjacencyList[6].Add(new Edge(7, 4, 3));
        adjacencyList[6].Add(new Edge(7, 8, 16));
        adjacencyList[7].Add(new Edge(8, 7, 16));
        adjacencyList[7].Add(new Edge(8, 4, 4));

        foreach (List<Edge> adjacency in adjacencyList)
        {
            foreach (Edge adj in adjacency)
            {
                adj.StartVertex = adj.StartVertex - 1;
                adj.EndVertex = adj.EndVertex - 1;
            }
        }

        PrimAlgorithm prim = new PrimAlgorithm();
        List<Edge> mst2 = prim.Prim(adjacencyList, 8);
        foreach (Edge edge in mst2)
        {
            Console.WriteLine("{0} - {1}: {2}", edge.StartVertex + 1, edge.EndVertex + 1, edge.Weight);
        }

        Console.ReadLine();
    }
}
