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
        private readonly List<ICompressionAlgorithm> _algorithms;

        public CompressionService()
        {
            _algorithms = new List<ICompressionAlgorithm>
            {
                new HuffmanAlgorithm(),
                new LZ77Algorithm(),
                new LZ78Algorithm()
            };
        }

        public IEnumerable<ICompressionAlgorithm> GetAlgorithms()
        {
            return _algorithms;
        }

        private ICompressionAlgorithm? FindAlgorithm(string name)
        {
            foreach (var alg in _algorithms)
            {
                if (string.Equals(alg.Name, name, StringComparison.OrdinalIgnoreCase))
                    return alg;
            }
            return null;
        }

        public CompressionResult Compress(string algorithmName, string inputPath, string outputPath)
        {
            var result = new CompressionResult
            {
                AlgorithmName = algorithmName,
                InputFilePath = inputPath,
                OutputFilePath = outputPath
            };

            try
            {
                var algorithm = FindAlgorithm(algorithmName);
                if (algorithm == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "Algoritmo no encontrado.";
                    return result;
                }

                long originalSize = new FileInfo(inputPath).Length;
                result.OriginalSizeBytes = originalSize;

                long memBefore = GC.GetTotalMemory(true);
                var sw = Stopwatch.StartNew();

                algorithm.Compress(inputPath, outputPath);

                sw.Stop();
                long memAfter = GC.GetTotalMemory(false);

                result.CompressedSizeBytes = new FileInfo(outputPath).Length;
                result.ElapsedTime = sw.Elapsed;
                result.MemoryUsedBytes = memAfter - memBefore;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public CompressionResult Decompress(string algorithmName, string inputPath, string outputPath)
        {
            var result = new CompressionResult
            {
                AlgorithmName = algorithmName,
                InputFilePath = inputPath,
                OutputFilePath = outputPath
            };

            try
            {
                var algorithm = FindAlgorithm(algorithmName);
                if (algorithm == null)
                {
                    result.Success = false;
                    result.ErrorMessage = "Algoritmo no encontrado.";
                    return result;
                }

                long compressedSize = new FileInfo(inputPath).Length;
                result.CompressedSizeBytes = compressedSize;

                long memBefore = GC.GetTotalMemory(true);
                var sw = Stopwatch.StartNew();

                algorithm.Decompress(inputPath, outputPath);

                sw.Stop();
                long memAfter = GC.GetTotalMemory(false);

                result.OriginalSizeBytes = new FileInfo(outputPath).Length;
                result.ElapsedTime = sw.Elapsed;
                result.MemoryUsedBytes = memAfter - memBefore;
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}