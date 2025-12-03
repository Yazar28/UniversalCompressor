using System;
using System.Collections.Generic;
using System.IO;

namespace UniversalCompressor.Algorithms
{
    public class LZ78Algorithm : ICompressionAlgorithm
    {
        public string Name => "LZ78";

        public byte[] Compress(byte[] input)
        {
            if (input == null || input.Length == 0)
                return Array.Empty<byte>();

            var dict = new Dictionary<string, int>();
            dict[""] = 0;
            int nextIndex = 1;

            var encoded = new List<(int index, byte symbol)>();

            int pos = 0;
            while (pos < input.Length)
            {
                string current = "";
                int lastIndex = 0;
                int i = pos;

                while (i < input.Length)
                {
                    char c = (char)input[i];
                    string candidate = current + c;

                    if (dict.TryGetValue(candidate, out int idx))
                    {
                        current = candidate;
                        lastIndex = idx;
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (i < input.Length)
                {
                    byte nextSymbol = input[i];
                    encoded.Add((lastIndex, nextSymbol));

                    string newEntry = current + (char)nextSymbol;
                    if (!dict.ContainsKey(newEntry))
                    {
                        dict[newEntry] = nextIndex++;
                    }

                    pos = i + 1;
                }
                else
                {
                    encoded.Add((lastIndex, (byte)0));
                    pos = i;
                }
            }

            using var ms = new MemoryStream();
            using var bw = new BinaryWriter(ms);

            bw.Write(encoded.Count);
            foreach (var (idx, sym) in encoded)
            {
                bw.Write(idx);
                bw.Write(sym);
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

                int count = br.ReadInt32();
                var dict = new Dictionary<int, string>();
                dict[0] = "";

                int nextIndex = 1;
                var output = new List<byte>();

                for (int i = 0; i < count; i++)
                {
                    int idx = br.ReadInt32();
                    byte sym = br.ReadByte();

                    if (!dict.TryGetValue(idx, out string prefix))
                    {
                        throw new InvalidDataException(
                            "Los datos no coinciden con el formato esperado de LZ78. " +
                            "Probablemente el archivo no fue comprimido con este algoritmo o está dañado.");
                    }

                    if (sym == 0)
                    {
                        foreach (char ch in prefix)
                            output.Add((byte)ch);
                    }
                    else
                    {
                        string entry = prefix + (char)sym;

                        dict[nextIndex++] = entry;

                        foreach (char ch in entry)
                            output.Add((byte)ch);
                    }
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
                    "Error al descomprimir con LZ78. " +
                    "Es posible que el archivo no haya sido comprimido con este algoritmo o esté corrupto.",
                    ex);
            }
        }
    }
}
