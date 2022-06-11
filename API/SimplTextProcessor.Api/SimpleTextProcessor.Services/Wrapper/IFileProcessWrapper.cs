namespace SimpleTextProcessor.Services.Wrapper
{
    public interface IFileProcessWrapper
    {
        /// <summary>
        /// Asynchronously writes a text to the stream
        /// </summary>
        /// <param name="path">Full file path to write to</param>
        /// <param name="text">TText to write</param>
        /// <param name="append">TRUE to append data to the file</param>
        Task WriteLineAsync(string path, string text, bool append);

        /// <summary>
        /// Asynchronously reads all characters from the current position to the end of the stream 
        /// </summary>
        /// <param name="path">Full file path to be read</param>
        /// <returns>The value of the Task read operation contains a string with the characters</returns>
        Task<string> ReadToEndAsync(string path);

        /// <summary>
        /// Check if the specified file exists
        /// </summary>
        /// <param name="file">FileInfo object</param>
        /// <returns>TRUE if a file exists</returns>
        bool FileExists(FileInfo file);

        /// <summary>
        /// Deletes a file
        /// </summary>
        /// <param name="file">FileInfo object</param>
        void FileDelete(FileInfo file);

        /// <summary>
        /// Moves/renames a file
        /// </summary>
        /// <param name="file">FileInfo object</param>
        /// <param name="targetFilePath">New file full path</param>
        void MoveTo(FileInfo file, string targetFilePath);

        /// <summary>
        /// Returns a file list from the target directory
        /// </summary>
        /// <param name="dirInfo">DirectoryInfo object</param>
        /// <returns> An array of FileInfo objects</returns>
        FileInfo[] GetFiles(DirectoryInfo dirInfo);
    }
}
