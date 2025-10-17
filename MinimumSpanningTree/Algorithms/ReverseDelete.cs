using MinimumSpanningTree.Models;

namespace MinimumSpanningTree.Algorithms;

public static class ReverseDelete
{
    public static void Run(int[,] graph, int verticesCount)
    {
        List<Edge> edges = new();

        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = i + 1; j < verticesCount; j++)
            {
                if (graph[i, j] < int.MaxValue)
                    edges.Add(new Edge(i, j, graph[i, j]));
            }
        }

        edges.Sort((a, b) => b.Weight.CompareTo(a.Weight));

        List<Edge> mst = new(edges);

        foreach (var edge in edges)
        {
            mst.Remove(edge);

            if (!IsConnected(mst, verticesCount))
                mst.Add(edge);
        }

        foreach (var edge in mst)
        {
            char u = (char)('A' + edge.Source);
            char v = (char)('A' + edge.Destination);
            Console.WriteLine($"{u} - {v} \t {edge.Weight}");
        }
    }

    static bool IsConnected(List<Edge> edges, int vertices)
    {
        if (vertices == 0) return true;

        var visited = new bool[vertices];
        DFS(edges, 0, visited);

        for (int i = 0; i < vertices; i++)
            if (!visited[i]) return false;

        return true;
    }

    static void DFS(List<Edge> edges, int node, bool[] visited)
    {
        visited[node] = true;
        foreach (var edge in edges)
        {
            if (edge.Source == node && !visited[edge.Destination])
                DFS(edges, edge.Destination, visited);
            else if (edge.Destination == node && !visited[edge.Source])
                DFS(edges, edge.Source, visited);
        }
    }
}