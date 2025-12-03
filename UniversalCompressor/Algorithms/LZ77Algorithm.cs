using System;
using System.Collections.Generic;
using System.IO;

namespace UniversalCompressor.Algorithms
{
    public class LZ77Algorithm : ICompressionAlgorithm
    {
        public string Name => "LZ77";

        private const int WINDOW_SIZE = 4096; 

        public byte[] Compress(byte[] input)
        {
            if (input == null || input.Length == 0)
                return Array.Empty<byte>();

            List<(int offset, int length, byte next)> encoded = new();

            int pos = 0;

            while (pos < input.Length)
            {
                int bestOffset = 0;
                int bestLength = 0;

                int startSearch = Math.Max(0, pos - WINDOW_SIZE);

                for (int searchPos = startSearch; searchPos < pos; searchPos++)
                {
                    int length = 0;

                    while (pos + length < input.Length &&
                           input[searchPos + length] == input[pos + length])
                    {
                        length++;
                        if (searchPos + length >= pos) break;
                    }

                    if (length > bestLength)
                    {
                        bestLength = length;
                        bestOffset = pos - searchPos;
                    }
                }

                byte nextSymbol = (pos + bestLength < input.Length)
                    ? input[pos + bestLength]
                    : (byte)0;

                encoded.Add((bestOffset, bestLength, nextSymbol));
                pos += bestLength + 1;
            }

            using MemoryStream ms = new();
            using BinaryWriter bw = new(ms);

            bw.Write(encoded.Count);

            foreach (var (offset, length, next) in encoded)
            {
                bw.Write(offset);
                bw.Write(length);
                bw.Write(next);
            }

            return ms.ToArray();
        }

        public byte[] Decompress(byte[] input)
        {
            if (input == null || input.Length == 0)
                return Array.Empty<byte>();

            try
            {
                using MemoryStream ms = new(input);
                using BinaryReader br = new(ms);

                int count = br.ReadInt32();
                List<byte> output = new();

                for (int i = 0; i < count; i++)
                {
                    int offset = br.ReadInt32();
                    int length = br.ReadInt32();
                    byte nextSymbol = br.ReadByte();

                    if (offset == 0 && length == 0)
                    {
                        output.Add(nextSymbol);
                    }
                    else
                    {
                        int startCopy = output.Count - offset;

                        if (startCopy < 0 || startCopy + length > output.Count)
                        {
                            throw new InvalidDataException(
                                "Los datos no coinciden con el formato esperado de LZ77. " +
                                "Probablemente el archivo no fue comprimido con este algoritmo o está dañado.");
                        }

                        for (int j = 0; j < length; j++)
                        {
                            output.Add(output[startCopy + j]);
                        }

                        output.Add(nextSymbol);
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
                    "Error al descomprimir con LZ77. " +
                    "Es posible que el archivo no haya sido comprimido con este algoritmo o esté corrupto.",
                    ex);
            }
        }

    }
}
