using SimpleTextProcessor.Services.Converter;
using SimpleTextProcessor.Services.Dto;
using SimpleTextProcessor.Services.Wrapper;

namespace SimpleTextProcessor.Services
{
    public sealed class TextProcessor: ITextProcessor
    {
        private readonly IFileInfoConverter _fileInfoConverter;
        private readonly IFileProcessWrapper _fileProcessWrapper;

        public TextProcessor(IFileInfoConverter fileInfoConverter, IFileProcessWrapper fileProcessWrapper)
        {
            _fileInfoConverter = fileInfoConverter;
            _fileProcessWrapper = fileProcessWrapper;
        }

        public IEnumerable<FileDto> ExecuteGetFiles(string uploadsFolder)
        {
            var files = _fileProcessWrapper.GetFiles(uploadsFolder);
            var fileDtos = files.Select(f => _fileInfoConverter.Convert(f));

            return fileDtos;
        }

        public async Task ExecuteUploadAsync(TextDto dto, string folder)
        {
            var tempFileName = dto.Name;
            var tempFullPath = Path.Combine(folder, tempFileName);
            await _fileProcessWrapper.AppendTextAsync(tempFullPath, dto.Text).ConfigureAwait(false);

            // If upload completed delete the existing file (if it exists) and rename the temp file
            if (dto.IsLastChunk)
            {
                // The temp file name pattern is <file name w/o extension>_<GUID>
                var fileName = Path.ChangeExtension(tempFileName.Split('_').First(), ".txt");
                var fullPath = Path.Combine(folder, fileName);
                if (_fileProcessWrapper.FileExists(fullPath))
                {
                    _fileProcessWrapper.FileDelete(fullPath);
                }
                
                _fileProcessWrapper.MoveTo(tempFullPath, fullPath);
            }
        }

        public async Task<TextDto?> ExecuteDownloadAsync(string name, int start, int chunkSize, string folder)
        {
            name = Path.ChangeExtension(name, ".txt");
            var isLastChunk = false;
            var files = _fileProcessWrapper.GetFiles(folder);
            var file = files.FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (file == null)
            {
                return null;
            }
            var fullPath = Path.Combine(folder, name);
            var content = await _fileProcessWrapper.ReadToEndAsync(fullPath).ConfigureAwait(false);
            var totalContentToRead = content.Length - start;
            var chunk = string.Empty;
            if (totalContentToRead > chunkSize)
            {
                chunk = content[start..(start + chunkSize)];
            }
            else
            {
                chunk = content[start..(start + totalContentToRead)];
                isLastChunk = true;
            }
            var textDto = new TextDto()
            {
                Name = name,
                Text = chunk,
                IsLastChunk = isLastChunk
            };

            return textDto;
        }

        public string ExecuteDeleteFile(string name, string folder)
        {
            name = Path.ChangeExtension(name, ".txt");
            var message = "Delete successful";
            var fullPath = Path.Combine(folder, name);
            if (_fileProcessWrapper.FileExists(fullPath))
            {
                _fileProcessWrapper.FileDelete(fullPath);
            }
            else
            {
                message = "File not found";
            }

            return message;
        }
    }
}
