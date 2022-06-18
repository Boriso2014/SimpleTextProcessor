using System.IO.Abstractions;
using SimpleTextProcessor.Infrastructure.Dto;

namespace SimpleTextProcessor.Infrastructure.Converter
{
    public sealed class FileInfoConverter : IFileInfoConverter
    {
        public FileDto Convert(IFileInfo fileInfo)
        {
            return new FileDto()
            {
                Name = Path.ChangeExtension(fileInfo.Name, null),
                Size = fileInfo.Length,
                CreatedOn = fileInfo.CreationTimeUtc,
                ModifiedOn = fileInfo.LastWriteTimeUtc
            };

        }
    }
}
