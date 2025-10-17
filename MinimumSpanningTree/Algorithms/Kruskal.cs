using MinimumSpanningTree.Models;

namespace MinimumSpanningTree.Algorithms;

public static class Kruskal
{
    class Subset
    {
        public int Parent;
        public int Rank;
    }

    static int Find(Subset[] subsets, int i)
    {
        if (subsets[i].Parent != i)
            subsets[i].Parent = Find(subsets, subsets[i].Parent);
        return subsets[i].Parent;
    }

    static void Union(Subset[] subsets, int x, int y)
    {
        int xroot = Find(subsets, x);
        int yroot = Find(subsets, y);

        if (subsets[xroot].Rank < subsets[yroot].Rank)
            subsets[xroot].Parent = yroot;
        else if (subsets[xroot].Rank > subsets[yroot].Rank)
            subsets[yroot].Parent = xroot;
        else
        {
            subsets[yroot].Parent = xroot;
            subsets[xroot].Rank++;
        }
    }

    public static void Run(int[,] graph, int verticesCount)
    {
        List<Edge> edges = new();
        for (int i = 0; i < verticesCount; i++)
        {
            for (int j = i + 1; j < verticesCount; j++)
            {
                if (graph[i, j] < int.MaxValue)
                {
                    edges.Add(new Edge(i, j, graph[i, j]));
                }
            }
        }

        edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

        Subset[] subsets = new Subset[verticesCount];
        for (int i = 0; i < verticesCount; i++)
        {
            subsets[i] = new Subset { Parent = i, Rank = 0 };
        }

        int e = 0, index = 0;
        while (e < verticesCount - 1 && index < edges.Count)
        {
            Edge next = edges[index++];
            int x = Find(subsets, next.Source);
            int y = Find(subsets, next.Destination);

            if (x != y)
            {
                char u = (char)('A' + next.Source);
                char v = (char)('A' + next.Destination);
                Console.WriteLine($"{u} - {v} \t {next.Weight}");
                Union(subsets, x, y);
                e++;
            }
        }
    }
}