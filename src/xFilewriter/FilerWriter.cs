using System;
using System.IO;
using System.Text;
using xFilewriter.Interface;

namespace xFilewriter
{
    public class FileWriter : IFileWriter
    {
        public void EnsureThatFilePathExists(string directoryPath, string fileName)
        {
            if (Validation.StringIsNullOrEmpty(directoryPath) || Validation.StringIsNullOrEmpty(fileName))
                throw new Exception("Error: directory Path or filename is null or empty");

            if (!Validation.DirecortyPathExists(directoryPath))
                CreateDirectoryPath(directoryPath);
            if (!Validation.FilePathExists($"{directoryPath}/{fileName}"))
                CreateFile($"{directoryPath}/{fileName}");
        }

        public void DeleteFile(string filePath)
        {
            if (Validation.FilePathExists(filePath))
                File.Delete(filePath);
        }

        public void DeleteDirectory(string directoryPath)
        {
            if (Validation.DirecortyPathExists(directoryPath))
                Directory.Delete(directoryPath);
        }

        public void CreateDirectoryPath(string directoryPath)
        {
            if (!Validation.DirecortyPathExists(directoryPath))
                Directory.CreateDirectory(directoryPath);
        }

        public void CreateFile(string filePath)
        {
            if (!Validation.FilePathExists(filePath))
            {
                var file = File.Create(filePath);
                file.Close();
            }
        }

        private void AddTextToFile(FileStream fs, string text)
        {
            byte[] byteArray = new UTF8Encoding(true).GetBytes(text);
            fs.Write(byteArray, 0, byteArray.Length);
        }

        public bool IsSupportedFileModes(FileMode fileMode)
            => FileMode.Open == fileMode || FileMode.Append == fileMode || FileMode.Truncate == fileMode;

        public void AppendTextToFile(string text, string filePath, FileMode fileMode)
        {
            try
            {
                if (!Validation.FilePathExists(filePath))
                    throw new Exception("Error: The Given FilePath does not exist");

                if (!IsSupportedFileModes(fileMode))
                    throw new Exception("You have entered an unsupported file mode please use Append, Open or Truncate");

                using (var fs = new FileStream(filePath, fileMode, FileAccess.Write))
                {
                    if (fileMode == FileMode.Append)
                        text += Environment.NewLine;
                    AddTextToFile(fs, text);
                }



            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ReadTextFromFile(string filePath)
        {
            try
            {
                if (!Validation.FilePathExists(filePath))
                    throw new Exception("Error: The Given FilePath is does not exist");

                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = new StreamReader(fs))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
