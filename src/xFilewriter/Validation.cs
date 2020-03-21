using System.IO;

namespace xFilewriter
{
    public static class Validation
    {
        public static bool DirecortyPathExists(string direcortyPath)
            => Directory.Exists(direcortyPath);

        public static bool FilePathExists(string filePath)
            => File.Exists(filePath);
    }
}