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

        public CompressionResult CompressMultiple(string algorithmName, IReadOnlyList<string> inputFilePaths, string outputFilePath)
        {
            if (inputFilePaths == null || inputFilePaths.Count == 0)
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    "",
                    outputFilePath,
                    "Debes seleccionar al menos un archivo de entrada.");
            }

            if (!TryGetAlgorithm(algorithmName, out var algorithm))
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    string.Join("; ", inputFilePaths),
                    outputFilePath,
                    "Algoritmo de compresión no encontrado.");
            }

            try
            {
                long totalOriginalSize = 0;
                long totalCompressedSize = 0;

                var stopwatch = Stopwatch.StartNew();
                long memoryBefore = GC.GetTotalMemory(true);

                using (var fs = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(inputFilePaths.Count);

                    foreach (var path in inputFilePaths)
                    {
                        if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
                        {
                            throw new FileNotFoundException("No se encontró uno de los archivos de entrada.", path ?? "");
                        }

                        byte[] originalBytes = File.ReadAllBytes(path);
                        totalOriginalSize += originalBytes.LongLength;

                        byte[] compressedBytes = algorithm.Compress(originalBytes);
                        totalCompressedSize += compressedBytes.LongLength;

                        string fileName = Path.GetFileName(path);
                        byte[] nameBytes = System.Text.Encoding.UTF8.GetBytes(fileName);

                        bw.Write(nameBytes.Length);
                        bw.Write(nameBytes);
                        bw.Write(compressedBytes.Length);
                        bw.Write(compressedBytes);
                    }
                }

                long memoryAfter = GC.GetTotalMemory(false);
                stopwatch.Stop();

                double ratio = totalOriginalSize == 0
                    ? 1.0
                    : (double)totalCompressedSize / totalOriginalSize;
                long memoryUsed = Math.Max(0, memoryAfter - memoryBefore);

                return CompressionResult.SuccessResult(
                    algorithmName,
                    string.Join("; ", inputFilePaths),
                    outputFilePath,
                    totalOriginalSize,
                    totalCompressedSize,
                    ratio,
                    stopwatch.Elapsed,
                    memoryUsed);
            }
            catch (Exception ex)
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    string.Join("; ", inputFilePaths),
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

        public CompressionResult DecompressMultiple(string algorithmName, string inputArchivePath, string outputDirectory)
        {
            if (string.IsNullOrWhiteSpace(inputArchivePath) || !File.Exists(inputArchivePath))
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    inputArchivePath,
                    outputDirectory,
                    "El archivo .myzip de entrada no existe.");
            }

            if (string.IsNullOrWhiteSpace(outputDirectory))
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    inputArchivePath,
                    outputDirectory,
                    "Debes especificar una carpeta de salida.");
            }

            if (!TryGetAlgorithm(algorithmName, out var algorithm))
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    inputArchivePath,
                    outputDirectory,
                    "Algoritmo de descompresión no encontrado.");
            }

            try
            {
                Directory.CreateDirectory(outputDirectory);

                long totalOriginalSize = 0;
                long totalCompressedSize = new FileInfo(inputArchivePath).Length;

                var stopwatch = Stopwatch.StartNew();
                long memoryBefore = GC.GetTotalMemory(true);

                using (var fs = new FileStream(inputArchivePath, FileMode.Open, FileAccess.Read))
                using (var br = new BinaryReader(fs))
                {
                    int fileCount = br.ReadInt32();

                    for (int i = 0; i < fileCount; i++)
                    {
                        int nameLength = br.ReadInt32();
                        byte[] nameBytes = br.ReadBytes(nameLength);
                        string fileName = System.Text.Encoding.UTF8.GetString(nameBytes);

                        int compressedLength = br.ReadInt32();
                        byte[] compressedBytes = br.ReadBytes(compressedLength);

                        byte[] decompressedBytes = algorithm.Decompress(compressedBytes);
                        totalOriginalSize += decompressedBytes.LongLength;

                        string outputPath = Path.Combine(outputDirectory, fileName);
                        File.WriteAllBytes(outputPath, decompressedBytes);
                    }
                }

                long memoryAfter = GC.GetTotalMemory(false);
                stopwatch.Stop();

                double ratio = totalOriginalSize == 0
                    ? 1.0
                    : (double)totalCompressedSize / totalOriginalSize;
                long memoryUsed = Math.Max(0, memoryAfter - memoryBefore);

                return CompressionResult.SuccessResult(
                    algorithmName,
                    inputArchivePath,
                    outputDirectory,
                    totalOriginalSize,
                    totalCompressedSize,
                    ratio,
                    stopwatch.Elapsed,
                    memoryUsed);
            }
            catch (Exception ex)
            {
                return CompressionResult.ErrorResult(
                    algorithmName,
                    inputArchivePath,
                    outputDirectory,
                    ex.Message);
            }
        }
    }
}
