using SimpleTextProcessor.Services.Dto;

namespace SimpleTextProcessor.Services.Converter
{
    public interface IFileInfoConverter
    {
        /// <summary>
        /// Converts FileInfo to FileDto object
        /// </summary>
        /// <param name="FileInfo">FileInfo object</param>
        /// <returns>FileDto object</returns>
        FileDto Convert(FileInfo fileInfo);
    }
}
