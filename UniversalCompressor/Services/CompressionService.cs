using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UniversalCompressor.Algorithms;
using UniversalCompressor.Models;

namespace UniversalCompressor.Services
{
    public class CompressionService
    {
        private readonly Dictionary<string, ICompressionAlgorithm> _algorithms;

        public CompressionService()
        {
            _algorithms = new Dictionary<string, ICompressionAlgorithm>(StringComparer.OrdinalIgnoreCase)
            {
                ["Huffman"] = new HuffmanAlgorithm(),
                ["LZ77"] = new LZ77Algorithm(),
                ["LZ78"] = new LZ78Algorithm()
            };
        }

        public IEnumerable<ICompressionAlgorithm> GetAlgorithms()
        {
            return _algorithms.Values;
        }

        private bool TryGetAlgorithm(string name, out ICompressionAlgorithm algorithm)
        {
            return _algorithms.TryGetValue(name, out algorithm!);
        }

        public CompressionResult Compress(string algorithmName, string inputFilePath, string outputFilePath)
        {
            if (!TryGetAlgorithm(algorithmName, out var algorithm))
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    inputFilePath,
                    outputFilePath,
                    "Algoritmo de compresión no encontrado.");
            }

            try
            {
                var originalBytes = File.ReadAllBytes(inputFilePath);
                long originalSize = originalBytes.LongLength;

                var stopwatch = Stopwatch.StartNew();
                long memoryBefore = GC.GetTotalMemory(true);

                var compressedBytes = algorithm.Compress(originalBytes);

                long memoryAfter = GC.GetTotalMemory(false);
                stopwatch.Stop();

                File.WriteAllBytes(outputFilePath, compressedBytes);

                long compressedSize = compressedBytes.LongLength;
                double ratio = originalSize == 0 ? 1.0 : (double)compressedSize / originalSize;
                long memoryUsed = Math.Max(0, memoryAfter - memoryBefore);

                return CompressionResult.SuccessResult(
                    algorithmName,
                    inputFilePath,
                    outputFilePath,
                    originalSize,
                    compressedSize,
                    ratio,
                    stopwatch.Elapsed,
                    memoryUsed);
            }
            catch (Exception ex)
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    inputFilePath,
                    outputFilePath,
                    ex.Message);
            }
        }

        public CompressionResult Decompress(string algorithmName, string inputFilePath, string outputFilePath)
        {
            if (!TryGetAlgorithm(algorithmName, out var algorithm))
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    inputFilePath,
                    outputFilePath,
                    "Algoritmo de descompresión no encontrado.");
            }

            try
            {
                var compressedBytes = File.ReadAllBytes(inputFilePath);
                long compressedSize = compressedBytes.LongLength;

                var stopwatch = Stopwatch.StartNew();
                long memoryBefore = GC.GetTotalMemory(true);

                var decompressedBytes = algorithm.Decompress(compressedBytes);

                long memoryAfter = GC.GetTotalMemory(false);
                stopwatch.Stop();

                File.WriteAllBytes(outputFilePath, decompressedBytes);

                long originalSize = decompressedBytes.LongLength;
                double ratio = originalSize == 0 ? 1.0 : (double)compressedSize / originalSize;
                long memoryUsed = Math.Max(0, memoryAfter - memoryBefore);

                return CompressionResult.SuccessResult(
                    algorithmName,
                    inputFilePath,
                    outputFilePath,
                    originalSize,
                    compressedSize,
                    ratio,
                    stopwatch.Elapsed,
                    memoryUsed);
            }
            catch (Exception ex)
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    inputFilePath,
                    outputFilePath,
                    ex.Message);
            }
        }
    }
}
