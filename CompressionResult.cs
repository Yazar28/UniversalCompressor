namespace UniversalCompressor.Models
{
    public class CompressionResult
    {
        // Nombre del algoritmo usado (Huffman, LZ77, LZ78)
        public string AlgorithmName { get; set; } = string.Empty;

        // Rutas de archivo
        public string InputFilePath { get; set; } = string.Empty;
        public string OutputFilePath { get; set; } = string.Empty;

        // Tamaños de archivo en bytes
        public long OriginalSizeBytes { get; set; }
        public long CompressedSizeBytes { get; set; }

        // Tiempo que duró la operación
        public TimeSpan ElapsedTime { get; set; }

        // Memoria usada (en bytes)
        public long MemoryUsedBytes { get; set; }

        // Tasa de compresión (ej: 0.35 = 35%)
        public double CompressionRatio
        {
            get
            {
                if (OriginalSizeBytes == 0) return 0;
                return (double)CompressedSizeBytes / OriginalSizeBytes;
            }
        }

        // Por si quieres saber si salió bien o hubo error
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
