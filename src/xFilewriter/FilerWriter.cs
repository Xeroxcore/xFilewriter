using System;
using System.IO;
using System.Text;
using xFilewriter.Interface;

namespace xFilewriter
{
    public class FileWriter : IFileWriter
    {
        public void EnsureThatFilePathExists(string direcrotyPath, string fileName)
        {
            if (!Validation.DirecortyPathExists(direcrotyPath))
                CreateDirectoryPath(direcrotyPath);
            if (!Validation.FilePathExists($"{direcrotyPath}/{fileName}"))
                CreateFile($"{direcrotyPath}/{fileName}");
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

        public void AppendTextToFile(string text, string filePath, FileMode fileMode)
        {
            try
            {
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
