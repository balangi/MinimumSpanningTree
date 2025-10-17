namespace MinimumSpanningTree.Algorithms;

public static class PrimJarnik
{
    public static void Run(int[,] graph, int verticesCount)
    {
        int[] parent = new int[verticesCount];
        int[] key = new int[verticesCount];
        bool[] mstSet = new bool[verticesCount];

        for (int i = 0; i < verticesCount; i++)
        {
            key[i] = int.MaxValue;
            mstSet[i] = false;
        }

        key[0] = 0;
        parent[0] = -1;

        for (int count = 0; count < verticesCount - 1; count++)
        {
            int u = MinKey(key, mstSet, verticesCount);
            mstSet[u] = true;

            for (int v = 0; v < verticesCount; v++)
            {
                if (graph[u, v] != 0 &&
                    graph[u, v] < int.MaxValue &&
                    !mstSet[v] &&
                    graph[u, v] < key[v])
                {
                    parent[v] = u;
                    key[v] = graph[u, v];
                }
            }
        }

        Print(parent, graph, verticesCount);
    }

    static int MinKey(int[] key, bool[] mstSet, int verticesCount)
    {
        int min = int.MaxValue, minIndex = -1;

        for (int v = 0; v < verticesCount; v++)
        {
            if (!mstSet[v] && key[v] < min)
            {
                min = key[v];
                minIndex = v;
            }
        }

        return minIndex;
    }

    static void Print(int[] parent, int[,] graph, int verticesCount)
    {
        for (int i = 1; i < verticesCount; i++)
        {
            char u = (char)('A' + parent[i]);
            char v = (char)('A' + i);
            Console.WriteLine($"{u} - {v} \t {graph[i, parent[i]]}");
        }
    }
}