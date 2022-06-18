using System.IO.Abstractions.TestingHelpers;
using SimpleTextProcessor.Services.Converter;
using SimpleTextProcessor.Services.Dto;
using SimpleTextProcessor.Services.Wrapper;

namespace SimpleTextProcessor.Services.Tests
{
    [TestFixture]
    public sealed class TextProcessorTests
    {
        private readonly TextProcessor _processor;
        private readonly IFileProcessWrapper _wrapper;
        private readonly MockFileSystem _fileSystem;

        private readonly string _testFolder = "test";
        private readonly string _content = "0123456789";
        private readonly string _file1 = "f1";
        private readonly string _file2 = "f2";
        private string _path1;
        private string _path2;

        public TextProcessorTests()
        {
            var converter = new FileInfoConverter();
            _fileSystem = new MockFileSystem();
            _wrapper = new FileProcessWrapper(_fileSystem);
            _processor = new TextProcessor(converter, _wrapper);
        }

        [SetUp]
        public void Setup()
        {
            var fileData = new MockFileData(_content);
            _path1 = Path.Combine(_testFolder, Path.ChangeExtension(_file1, ".txt"));
            _path2 = Path.Combine(_testFolder, Path.ChangeExtension(_file2, ".txt"));
            _fileSystem.AddDirectory(_testFolder);
            _fileSystem.AddFile(_path1, fileData);
            _fileSystem.AddFile(_path2, string.Empty);
        }

        [Test]
        public void should_return_files()
        {
            // Arrange
            var fi1 = _fileSystem.FileInfo.FromFileName(_path1);
            var fi2 = _fileSystem.FileInfo.FromFileName(_path2);
            var count = _fileSystem.Directory.GetFiles(_testFolder).Length;

            // Act
            var result = _processor.ExecuteGetFiles(_testFolder).ToList();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(count));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo(Path.ChangeExtension(fi1.Name, null)));
                Assert.That(result[1].Name, Is.EqualTo(Path.ChangeExtension(fi2.Name, null)));
            });
        }

        [TestCase(5, false)]
        [TestCase(10, true)]
        public async Task should_execute_download(int chunkSize, bool isLastChunk)
        {
            // Arrange
            var start = 0;
            var fi1 = _fileSystem.FileInfo.FromFileName(_path1);

            // Act
            var result = await _processor.ExecuteDownloadAsync(_file1, start, chunkSize, _testFolder);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(fi1.Name));
                Assert.That(result.Text, Is.EqualTo(_content[start..(start + chunkSize)]));
                Assert.That(result.IsLastChunk, Is.EqualTo(isLastChunk));
            });
        }

        [Test]
        public async Task should_execute_upload()
        {
            // Arrange
            var tempFileName = "file3_123";
            var dto = new TextDto()
            {
                Name = tempFileName,
                Text = _content,
                IsLastChunk= true
            };
            var expectedFileName = _fileSystem.Path.ChangeExtension(dto.Name.Split('_').First(), ".txt");
            var expectedFilePath = _fileSystem.Path.Combine(_testFolder, expectedFileName);

            // Act
            await _processor.ExecuteUploadAsync(dto, _testFolder);

            // Assert
            var fileExist =_fileSystem.FileExists(expectedFilePath);
            Assert.That(fileExist, Is.True);
            var content = _fileSystem.GetFile(expectedFilePath).TextContents;
            Assert.That(content, Is.EqualTo(dto.Text));
        }

        [TestCase("f1", "Delete successful")]
        [TestCase("fn", "File not found")]

        public void should_delete_file(string fileName, string expectedResult)
        {
            // Act
            var result = _processor.ExecuteDeleteFile(fileName, _testFolder);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
