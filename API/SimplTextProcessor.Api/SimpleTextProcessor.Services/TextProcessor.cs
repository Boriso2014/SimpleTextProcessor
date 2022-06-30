using SimpleTextProcessor.Infrastructure.Converter;
using SimpleTextProcessor.Infrastructure.Dto;
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
            throw new Exception("EXCEPTION in ExecuteGetFiles");
            var files = _fileProcessWrapper.GetFiles(uploadsFolder);
            var fileDtos = files.Select(f => _fileInfoConverter.Convert(f));

            return fileDtos;
        }

        public async Task ExecuteUploadAsync(TextDto dto, string folder)
        {
            var tempFileName = dto.Name;
            var tempFullPath = _fileProcessWrapper.PathCombine(folder, tempFileName);
            await _fileProcessWrapper.AppendTextAsync(tempFullPath, dto.Text).ConfigureAwait(false);

            // If upload completed delete the existing file (if it exists) and rename the temp file
            if (dto.IsLastChunk)
            {
                // The temp file name pattern is <file name w/o extension>_<GUID>
                var fileName = _fileProcessWrapper.ChangeExtension(tempFileName.Split('_').First(), ".txt");
                var fullPath = _fileProcessWrapper.PathCombine(folder, fileName);
                if (_fileProcessWrapper.FileExists(fullPath))
                {
                    _fileProcessWrapper.FileDelete(fullPath);
                }
                
                _fileProcessWrapper.MoveTo(tempFullPath, fullPath);
            }
        }

        public async Task<TextDto?> ExecuteDownloadAsync(string name, int start, int chunkSize, string folder)
        {
            name = _fileProcessWrapper.ChangeExtension(name, ".txt");
            var isLastChunk = false;
            var files = _fileProcessWrapper.GetFiles(folder);
            var file = files.FirstOrDefault(f => f.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (file == null)
            {
                return null;
            }
            var fullPath = _fileProcessWrapper.PathCombine(folder, name);
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
            name = _fileProcessWrapper.ChangeExtension(name, ".txt");
            var message = "Delete successful";
            var fullPath = _fileProcessWrapper.PathCombine(folder, name);
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
