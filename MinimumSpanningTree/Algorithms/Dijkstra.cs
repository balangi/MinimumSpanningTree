namespace MinimumSpanningTree.Algorithms;

public static class Dijkstra
{
    public static void Run()
    {
        Console.WriteLine("\n=== Dijkstra Shortest Path ===");
        Console.Write("Enter number of nodes: ");
        int nodeCount = int.Parse(Console.ReadLine() ?? "0");

        // Create graph
        var graph = new Dictionary<int, List<(int target, int weight)>>();

        for (int i = 1; i <= nodeCount; i++)
        {
            graph[i] = new List<(int, int)>();
        }

        Console.WriteLine("\nEnter edges in the format: source destination weight");
        Console.WriteLine("Type 'done' when finished.\n");

        while (true)
        {
            Console.Write("Edge: ");
            string? input = Console.ReadLine()?.Trim();

            if (string.Equals(input, "done", StringComparison.OrdinalIgnoreCase))
                break;

            var parts = input?.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts == null || parts.Length != 3)
            {
                Console.WriteLine("Invalid format. Example: 1 2 5");
                continue;
            }

            if (int.TryParse(parts[0], out int source) &&
                int.TryParse(parts[1], out int dest) &&
                int.TryParse(parts[2], out int weight))
            {
                if (graph.ContainsKey(source))
                    graph[source].Add((dest, weight));
                else
                    graph[source] = new List<(int, int)> { (dest, weight) };
            }
            else
            {
                Console.WriteLine("Invalid numbers. Try again.");
            }
        }

        Console.Write("\nEnter start node: ");
        int startNode = int.Parse(Console.ReadLine() ?? "1");

        if (!graph.ContainsKey(startNode))
        {
            Console.WriteLine("Start node not found in graph!");
            return;
        }

        var (distances, previous) = RunDijkstra(graph, startNode);

        Console.WriteLine("\n=== Shortest Paths from Node " + startNode + " ===");
        foreach (var node in graph.Keys.OrderBy(k => k))
        {
            if (node == startNode) continue;
            var path = GetPath(previous, startNode, node);
            if (path.Count > 1)
                Console.WriteLine($"To {node}: {string.Join(" -> ", path)}");
            else
                Console.WriteLine($"To {node}: No path");
        }
    }

    private static (Dictionary<int, int> dist, Dictionary<int, int> prev)
        RunDijkstra(Dictionary<int, List<(int target, int weight)>> graph, int start)
    {
        var dist = new Dictionary<int, int>();
        var prev = new Dictionary<int, int>();
        var q = new HashSet<int>(graph.Keys);

        foreach (var v in graph.Keys)
            dist[v] = int.MaxValue;
        dist[start] = 0;

        while (q.Count > 0)
        {
            int u = q.OrderBy(v => dist[v]).First();
            q.Remove(u);

            foreach (var (v, weight) in graph[u])
            {
                int alt = dist[u] + weight;
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        return (dist, prev);
    }

    private static List<int> GetPath(Dictionary<int, int> prev, int start, int target)
    {
        var path = new List<int>();
        int current = target;

        while (prev.ContainsKey(current))
        {
            path.Insert(0, current);
            current = prev[current];
        }

        if (current == start)
            path.Insert(0, start);

        return path;
    }
}
