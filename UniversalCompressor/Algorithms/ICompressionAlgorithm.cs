using System;

namespace UniversalCompressor.Algorithms
{
    public interface ICompressionAlgorithm
    {
        string Name { get; }

        /// <summary>
        /// Recibe los bytes originales y devuelve los bytes comprimidos.
        /// </summary>
        byte[] Compress(byte[] input);

        /// <summary>
        /// Recibe bytes comprimidos y devuelve los bytes originales.
        /// </summary>
        byte[] Decompress(byte[] input);
    }
}
