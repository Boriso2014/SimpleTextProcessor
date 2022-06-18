using SimpleTextProcessor.Infrastructure.Dto;

namespace SimpleTextProcessor.Services
{
    public interface ITextProcessor
    {
        /// <summary>
        /// Returns files from the specified folder
        /// </summary>
        /// <param name="uploadsFolder">Target folder</param>
        /// <returns>List of FileDto objects</returns>
        IEnumerable<FileDto> ExecuteGetFiles(string uploadsFolder);
        
        /// <summary>
        /// Upload a file or a file chunk
        /// </summary>
        /// <param name="dto">Data to be uploaded</param>
        /// <param name="uploadsFolder">Target folder</param>
        Task ExecuteUploadAsync(TextDto dto, string uploadsFolder);

        /// <summary>
        /// Downloads a file or a file chunk 
        /// </summary>
        /// <param name="name">File name w/o an extension</param>
        /// <param name="start">Start download position</param>
        /// <param name="chunkSize">Size of a file chunk</param>
        /// <param name="folder">Source folder</param>
        /// <returns>TextDto object</returns>
        Task<TextDto?> ExecuteDownloadAsync(string name, int start, int chunkSize, string folder);

        /// <summary>
        /// Deletes a file
        /// </summary>
        /// <param name="name">File name</param>
        /// <param name="folder">Target folder</param>
        /// <returns>Notification</returns>
        string ExecuteDeleteFile(string name, string folder);
    }
}
