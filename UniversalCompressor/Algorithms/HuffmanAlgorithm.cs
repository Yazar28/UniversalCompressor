using System;
using System.Collections.Generic;
using System.IO;

namespace UniversalCompressor.Algorithms
{
    public class HuffmanAlgorithm : ICompressionAlgorithm
    {
        public string Name => "Huffman";

        private class Node : IComparable<Node>
        {
            public byte? Symbol { get; set; }
            public int Frequency { get; set; }
            public Node? Left { get; set; }
            public Node? Right { get; set; }

            public bool IsLeaf => Symbol.HasValue;

            public int CompareTo(Node? other)
            {
                if (other == null) return 1;
                int cmp = Frequency.CompareTo(other.Frequency);
                if (cmp != 0) return cmp;
                return (Symbol ?? 0).CompareTo(other.Symbol ?? 0);
            }
        }

        public byte[] Compress(byte[] input)
        {
            if (input == null || input.Length == 0)
                return Array.Empty<byte>();

            int[] freq = new int[256];
            foreach (byte b in input)
                freq[b]++;

            Node root = BuildTree(freq);

            var codes = new Dictionary<byte, string>();
            BuildCodes(root, "", codes);

            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            for (int i = 0; i < 256; i++)
                bw.Write(freq[i]);

            byte currentByte = 0;
            int bitCount = 0;

            foreach (byte b in input)
            {
                string code = codes[b];
                foreach (char bit in code)
                {
                    if (bit == '1')
                    {
                        currentByte |= (byte)(1 << (7 - bitCount));
                    }

                    bitCount++;

                    if (bitCount == 8)
                    {
                        bw.Write(currentByte);
                        currentByte = 0;
                        bitCount = 0;
                    }
                }
            }

            if (bitCount > 0)
            {
                bw.Write(currentByte);
            }

            bw.Flush();
            return ms.ToArray();
        }

        public byte[] Decompress(byte[] input)
        {
            if (input == null || input.Length == 0)
                return Array.Empty<byte>();

            try
            {
                using var ms = new MemoryStream(input);
                using var br = new BinaryReader(ms);

                int[] freq = new int[256];
                long totalSymbols = 0;
                for (int i = 0; i < 256; i++)
                {
                    freq[i] = br.ReadInt32();
                    totalSymbols += freq[i];
                }

                if (totalSymbols == 0)
                    return Array.Empty<byte>();

                Node root = BuildTree(freq);

                if (root.IsLeaf)
                {
                    byte symbol = root.Symbol!.Value;
                    byte[] single = new byte[totalSymbols];
                    for (long i = 0; i < totalSymbols; i++)
                        single[i] = symbol;
                    return single;
                }

                var output = new List<byte>((int)totalSymbols);
                Node current = root;

                while (output.Count < totalSymbols && ms.Position < ms.Length)
                {
                    byte b = br.ReadByte();

                    for (int i = 0; i < 8; i++)
                    {
                        int bit = (b & (1 << (7 - i))) != 0 ? 1 : 0;

                        current = bit == 0
                            ? current.Left!
                            : current.Right!;

                        if (current.IsLeaf)
                        {
                            output.Add(current.Symbol!.Value);
                            current = root;

                            if (output.Count >= totalSymbols)
                                break;
                        }
                    }
                }

                if (output.Count != totalSymbols)
                {
                    throw new InvalidDataException(
                        "Los datos no coinciden con el formato esperado de Huffman. " +
                        "Probablemente el archivo no fue comprimido con este algoritmo o está dañado.");
                }

                return output.ToArray();
            }
            catch (InvalidDataException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidDataException(
                    "Error al descomprimir con Huffman. " +
                    "Es posible que el archivo no haya sido comprimido con este algoritmo o esté corrupto.",
                    ex);
            }
        }

        private static Node BuildTree(int[] freq)
        {
            var pq = new PriorityQueue<Node, int>();

            for (int i = 0; i < 256; i++)
            {
                if (freq[i] > 0)
                {
                    var node = new Node
                    {
                        Symbol = (byte)i,
                        Frequency = freq[i]
                    };
                    pq.Enqueue(node, node.Frequency);
                }
            }

            if (pq.Count == 1)
            {
                pq.TryDequeue(out var only, out _);
                var rootSingle = new Node
                {
                    Symbol = null,
                    Frequency = only!.Frequency,
                    Left = only,
                    Right = null
                };
                return rootSingle;
            }

            while (pq.Count > 1)
            {
                pq.TryDequeue(out var left, out _);
                pq.TryDequeue(out var right, out _);

                var parent = new Node
                {
                    Symbol = null,
                    Frequency = left!.Frequency + right!.Frequency,
                    Left = left,
                    Right = right
                };

                pq.Enqueue(parent, parent.Frequency);
            }

            pq.TryDequeue(out var root, out _);
            return root!;
        }

        private static void BuildCodes(Node node, string prefix, Dictionary<byte, string> codes)
        {
            if (node.IsLeaf && node.Symbol.HasValue)
            {
                if (prefix == "")
                    prefix = "0";

                codes[node.Symbol.Value] = prefix;
                return;
            }

            if (node.Left != null)
                BuildCodes(node.Left, prefix + "0", codes);

            if (node.Right != null)
                BuildCodes(node.Right, prefix + "1", codes);
        }
    }
}
