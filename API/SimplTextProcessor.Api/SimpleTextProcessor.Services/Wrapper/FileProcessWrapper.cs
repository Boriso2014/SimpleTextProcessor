using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextProcessor.Services.Wrapper
{
    public sealed class FileProcessWrapper: IFileProcessWrapper
    {
        public async Task WriteLineAsync(string path, string text, bool append)
        {
            using var writer = new StreamWriter(path, append);
            await writer.WriteLineAsync(text);
        }

        public async Task<string> ReadToEndAsync(string path)
        {
            using var reader = new StreamReader(path);
            var content = await reader.ReadToEndAsync();
            return content;
        }

        public bool FileExists(FileInfo file)
        {
            return file.Exists;
        }

        public void FileDelete(FileInfo file)
        {
            file.Delete();
        }

        public void MoveTo(FileInfo file, string targetFilePath)
        {
            file.MoveTo(targetFilePath);
        }

        public FileInfo[] GetFiles(DirectoryInfo dirInfo)
        {
            var files = dirInfo.GetFiles();
            return files;
        }
    }
}
