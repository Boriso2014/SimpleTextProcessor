using SimpleTextProcessor.Services.Dto;

namespace SimpleTextProcessor.Services.Converter
{
    public sealed class FileInfoConverter : IFileInfoConverter
    {
        public FileDto Convert(FileInfo fileInfo)
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
