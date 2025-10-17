using MinimumSpanningTree.Algorithms;


int INF = int.MaxValue;

int[,] graph = {
                { 0,   4,   INF, INF, INF, INF, INF, 7,   INF, INF },
                { 4,   0,   8,   INF, INF, INF, INF, 11,  INF, INF },
                { INF, 8,   0,   7,   4,   INF, INF, 2,   INF, INF },
                { INF, INF, 7,   0,   9,   14,  INF, INF, INF, INF },
                { INF, INF, 4,   9,   0,   10,  INF, 2,   INF, 6   },
                { INF, INF, INF, 14, 10,  0,   2,   INF, 1,   7   },
                { INF, INF, INF, INF, INF, 2,   0,   1,   INF, INF },
                { 7,   11,  2,   INF, 2,   INF, 1,   0,   6,   7   },
                { INF, INF, INF, INF, INF, 1,   INF, 6,   0,   7   },
                { INF, INF, INF, INF, 6,   7,   INF, 7,   7,   0   }
            };

int verticesCount = 10;
string choice;

do
{
    Console.WriteLine("\n--== Minimum Spanning Tree Menu ==--");
    Console.WriteLine("1. Print Prim-Jarnik MST");
    Console.WriteLine("2. Print Kruskal MST");
    Console.WriteLine("3. Print Reverse-Delete MST");
    Console.WriteLine("Q. Exit");
    Console.Write("Enter your choice: ");

    choice = Console.ReadLine()?.Trim().ToUpper();

    switch (choice)
    {
        case "1":
            Console.WriteLine("\n=== Prim-Jarnik MST ===");
            PrimJarnik.Run(graph, verticesCount);
            break;
        case "2":
            Console.WriteLine("\n=== Kruskal MST ===");
            Kruskal.Run(graph, verticesCount);
            break;
        case "3":
            Console.WriteLine("\n=== Reverse-Delete MST ===");
            ReverseDelete.Run(graph, verticesCount);
            break;
        case "Q":
            Console.WriteLine("Exiting program...");
            break;
        default:
            Console.WriteLine("Invalid choice. Please try again.");
            break;
    }

} while (choice != "Q");