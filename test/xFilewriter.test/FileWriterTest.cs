using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using xFilewriter.Interface;

namespace xFilewriter.test
{
    [TestClass]
    public class FileWriterTest
    {
        IFileWriter FileWriter { get; set; }
        public FileWriterTest()
        {
            FileWriter = new FileWriter();
        }

        [TestMethod]
        public void WriteTextToFile()
        {
            var directory = Directory.GetCurrentDirectory();
            var filepath = $"{directory}/log/logfile.txt";
            FileWriter.EnsureThatFilePathExists($"{directory}/log/", "logfile.txt");
            FileWriter.AppendTextToFile("Hello world", filepath, FileMode.Open);
            var text = FileWriter.ReadTextFromFile(filepath);
            Assert.AreEqual("Hello world", text);
        }

        [TestMethod]
        public void ReadTextFromFile()
        {
            var directory = Directory.GetCurrentDirectory();
            var filepath = $"{directory}/log/logfile.txt";
            var text = FileWriter.ReadTextFromFile(filepath);
            Assert.AreEqual("Hello world", text);
        }

        [TestMethod]
        public void DeleteFileile()
        {
            var directory = Directory.GetCurrentDirectory();
            var filepath = $"{directory}/log/logfile.txt";
            FileWriter.DeleteFile(filepath);
            Assert.IsFalse(Validation.FilePathExists(filepath));
        }

        [TestMethod]
        public void EnsureFilePathWithNull()
        {
            try
            {
                FileWriter.EnsureThatFilePathExists(null, "logfile.txt");
            }
            catch (Exception error)
            {
                Assert.AreEqual("Error: directory Path or filename is null or empty", error.Message);
            }
        }

        [TestMethod]
        public void EnsureFilePathThatsEmptyString()
        {
            try
            {
                FileWriter.EnsureThatFilePathExists("asd", "");
            }
            catch (Exception error)
            {
                Assert.AreEqual("Error: directory Path or filename is null or empty", error.Message);
            }
        }

        [TestMethod]
        public void FilePathNotExist()
        {
            try
            {
                var directory = Directory.GetCurrentDirectory();
                var filepath = $"{directory}/log/logfile.txt";
                FileWriter.AppendTextToFile("Hello world", filepath, FileMode.CreateNew);
            }
            catch (Exception error)
            {
                Assert.AreEqual("Error: The Given FilePath does not exist", error.Message);
            }
        }

        [TestMethod]
        public void AppendTextToFileWithNull()
        {
            var directory = Directory.GetCurrentDirectory();
            var filepath = $"{directory}/log/logfile.txt";
            try
            {
                FileWriter.EnsureThatFilePathExists($"{directory}/log/", "logfile.txt");
                FileWriter.AppendTextToFile("Hello world", filepath, FileMode.CreateNew);
            }
            catch (Exception error)
            {
                FileWriter.DeleteFile(filepath);
                Assert.AreEqual("You have entered an unsupported file mode please use Append, Open or Truncate", error.Message);

            }
        }

    }
}
