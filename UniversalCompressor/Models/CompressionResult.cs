using System;

namespace UniversalCompressor.Models
{
    public class CompressionResult
    {
        public bool Success { get; set; }
        public string AlgorithmName { get; set; } = string.Empty;
        public string InputFilePath { get; set; } = string.Empty;
        public string OutputFilePath { get; set; } = string.Empty;

        public long OriginalSizeBytes { get; set; }
        public long CompressedSizeBytes { get; set; }
        public double CompressionRatio { get; set; } 
        public TimeSpan ElapsedTime { get; set; }
        public long MemoryUsedBytes { get; set; }

        public string ErrorMessage { get; set; } = string.Empty;

        public static CompressionResult SuccessResult(
            string algorithmName,
            string inputPath,
            string outputPath,
            long originalSize,
            long compressedSize,
            double ratio,
            TimeSpan elapsed,
            long memoryUsed)
        {
            return new CompressionResult
            {
                Success = true,
                AlgorithmName = algorithmName,
                InputFilePath = inputPath,
                OutputFilePath = outputPath,
                OriginalSizeBytes = originalSize,
                CompressedSizeBytes = compressedSize,
                CompressionRatio = ratio,
                ElapsedTime = elapsed,
                MemoryUsedBytes = memoryUsed
            };
        }

        public static CompressionResult ErrorResult(
            string algorithmName,
            string inputPath,
            string outputPath,
            string error)
        {
            return new CompressionResult
            {
                Success = false,
                AlgorithmName = algorithmName,
                InputFilePath = inputPath,
                OutputFilePath = outputPath,
                ErrorMessage = error
            };
        }
    }
}
