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
        /// Checks if the specified file exists
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

        /// <summary>
        /// Changes file extension
        /// </summary>
        /// <param name="path">Path to a file</param>
        /// <param name="ext">New extension. NULL will remove an existing extension</param>
        /// <returns>The modified path</returns>
        string ChangeExtension(string path, string ext);

        /// <summary>
        /// Combines two path strings into a single path
        /// </summary>
        /// <param name="path1">First path to be combined</param>
        /// <param name="path2">Second path to be combined</param>
        /// <returns>The combined paths</returns>
        string PathCombine(string path1, string path2);

        /// <summary>
        /// Checks if the specified directory exists
        /// </summary>
        /// <param name="dirPath">Directory path</param>
        /// <returns>TRUE if a directory exists</returns>
        bool DirectoryExists(string dirPath);

        /// <summary>
        /// Creates all directories and subdirectories in the specified directory path unless they already exist.
        /// </summary>
        /// <param name="dirPath">Directory to be created</param>
        void CreateDirectory(string dirPath);
    }
}
