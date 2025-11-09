namespace MinimumSpanningTree.Algorithms
{
    public static class HuffmanCoding
    {
        private class Node
        {
            public char? Symbol { get; set; }
            public int Frequency { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }
        }

        //  Huffman tree and codes 
        public static void Run()
        {
            // Console.WriteLine("\n=== Huffman Encoding / Decoding ===");
            Console.Write("Enter your message: ");
            string message = Console.ReadLine() ?? string.Empty;

            // Count frequencies
            var frequency = message
                .GroupBy(ch => ch)
                .ToDictionary(g => g.Key, g => g.Count());

            Console.WriteLine("\nCharacter frequencies:");
            foreach (var kv in frequency)
                Console.WriteLine($"'{kv.Key}' : {kv.Value}");

            // Build Huffman tree
            var root = BuildHuffmanTree(frequency);

            // Generate Huffman codes
            var codes = new Dictionary<char, string>();
            GenerateCodes(root, "", codes);

            Console.WriteLine("\nHuffman Codes:");
            foreach (var kv in codes.OrderBy(k => k.Key))
                Console.WriteLine($"'{kv.Key}' => {kv.Value}");

            // Encode
            string encoded = string.Concat(message.Select(ch => codes[ch]));
            Console.WriteLine($"\nEncoded Message:\n{encoded}");

            // Decode
            string decoded = Decode(encoded, root);
            Console.WriteLine($"\nDecoded Message:\n{decoded}");

            // Compute minimal total length
            int minLength = frequency.Sum(f => f.Value * codes[f.Key].Length);
            Console.WriteLine($"\nMinimum Encoded Length = {minLength} bits");

            double avgLength = frequency.Sum(f => f.Value * codes[f.Key].Length) / (double)message.Length;
            Console.WriteLine($"Average bits per symbol = {avgLength:F2}");
        }

        // Huffman tree 
        private static Node BuildHuffmanTree(Dictionary<char, int> frequencies)
        {
            var pq = new List<Node>(
                frequencies.Select(kv => new Node { Symbol = kv.Key, Frequency = kv.Value })
            );

            // build tree
            while (pq.Count > 1)
            {
                pq = pq.OrderBy(n => n.Frequency).ToList();

                var left = pq[0];
                var right = pq[1];

                var parent = new Node
                {
                    Symbol = null,
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };

                pq.RemoveRange(0, 2);
                pq.Add(parent);
            }

            return pq[0];
        }

        // Recursive code generator 
        private static void GenerateCodes(Node node, string prefix, Dictionary<char, string> codes)
        {
            if (node == null)
                return;

            if (node.Symbol != null)
            {
                codes[node.Symbol.Value] = prefix.Length > 0 ? prefix : "0"; // single-node case
            }
            else
            {
                GenerateCodes(node.Left!, prefix + "0", codes);
                GenerateCodes(node.Right!, prefix + "1", codes);
            }
        }

        //  Decode Huffman binary string 
        private static string Decode(string encoded, Node root)
        {
            var result = new List<char>();
            var current = root;

            foreach (char bit in encoded)
            {
                current = bit == '0' ? current.Left! : current.Right!;

                if (current.Symbol != null)
                {
                    result.Add(current.Symbol.Value);
                    current = root;
                }
            }

            return new string(result.ToArray());
        }
    }
}
