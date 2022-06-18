using System.IO.Abstractions;

namespace SimpleTextProcessor.Services.Wrapper
{
    public sealed class FileProcessWrapper: IFileProcessWrapper
    {
        private readonly IFileSystem _fileSystem;

        public FileProcessWrapper(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task AppendTextAsync(string path, string text)
        {
            using var writer = _fileSystem.File.AppendText(path);
            await writer.WriteAsync(text);
            await writer.FlushAsync();
        }

        public async Task<string> ReadToEndAsync(string path)
        {
            using var reader = _fileSystem.File.OpenText(path);
            var content = await reader.ReadToEndAsync();
            return content;
        }

        public bool FileExists(string path)
        {
            return _fileSystem.File.Exists(path);
        }

        public void FileDelete(string path)
        {
            _fileSystem.File.Delete(path);
        }

        public void MoveTo(string sourceFilePath, string targetFilePath)
        {
            _fileSystem.File.Move(sourceFilePath, targetFilePath);
        }

        public IFileInfo[] GetFiles(string dirPath)
        {
            var files = _fileSystem.DirectoryInfo.FromDirectoryName(dirPath).GetFiles();
            return files;
        }
    }
}
