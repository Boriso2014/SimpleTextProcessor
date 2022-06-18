using System.IO.Abstractions;

namespace SimpleTextProcessor.Services.Wrapper
{
    public interface IFileProcessWrapper
    {
        /// <summary>
        /// Asynchronously appends a text to the stream
        /// </summary>
        /// <param name="path">Full file path to write to</param>
        /// <param name="text">Text to write</param>
        Task AppendTextAsync(string path, string text);

        /// <summary>
        /// Asynchronously reads all characters from the current position to the end of the stream 
        /// </summary>
        /// <param name="path">Full file path to be read</param>
        /// <returns>The value of the Task read operation contains a string with the characters</returns>
        Task<string> ReadToEndAsync(string path);

        /// <summary>
        /// Check if the specified file exists
        /// </summary>
        /// <param name="path">Path to a file</param>
        /// <returns>TRUE if a file exists</returns>
        bool FileExists(string path);

        /// <summary>
        /// Deletes a file
        /// </summary>
        /// <param name="path">Path to a file</param>
        void FileDelete(string path);

        /// <summary>
        /// Moves/renames a file
        /// </summary>
        /// <param name="sourceFilePath">Path to a source file</param>
        /// <param name="targetFilePath">Path to a target file</param>
        void MoveTo(string sourceFilePath, string targetFilePath);

        /// <summary>
        /// Returns a file list from the target directory
        /// </summary>
        /// <param name="dirPath">Directory path</param>
        /// <returns> An array of IFileInfo objects</returns>
        IFileInfo[] GetFiles(string dirPath);
    }
}
