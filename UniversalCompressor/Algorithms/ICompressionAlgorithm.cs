namespace UniversalCompressor.Algorithms
{
    public interface ICompressionAlgorithm
    {
        // Comprime el archivo de entrada y guarda el resultado en outputPath
        void Compress(string inputPath, string outputPath);

        // Descomprime el archivo de entrada y guarda el resultado en outputPath
        void Decompress(string inputPath, string outputPath);

        // Nombre del algoritmo (ej: "Huffman", "LZ77", etc.)
        string Name { get; }
    }
}
