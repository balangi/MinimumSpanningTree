namespace MinimumSpanningTree.Models;

public class Edge
{
    public int Source { get; set; }
    public int Destination { get; set; }
    public int Weight { get; set; }

    public Edge(int source, int dest, int weight)
    {
        Source = source;
        Destination = dest;
        Weight = weight;
    }
}