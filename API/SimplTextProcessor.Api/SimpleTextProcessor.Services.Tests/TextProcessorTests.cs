using Moq;
using SimpleTextProcessor.Services.Converter;
using SimpleTextProcessor.Services.Wrapper;

namespace SimpleTextProcessor.Services.Tests
{
    [TestFixture]
    public sealed class TextProcessorTests
    {
        private TextProcessor _processor;
        private readonly Mock<IFileProcessWrapper> _wrapper;
        private readonly string _testFolder = @"c:\test";
        private readonly string _file1 = "f1";
        private readonly string _file2 = "f2";
        private string _path1;
        private string _path2;

        public TextProcessorTests()
        {
            _wrapper = new Mock<IFileProcessWrapper>();
            var converter = new FileInfoConverter();
            _processor = new TextProcessor(converter, _wrapper.Object);
            _file1 = Path.ChangeExtension(_file1, ".txt");
            _file2 = Path.ChangeExtension(_file2, ".txt");
            _path1 = Path.Combine(_testFolder, _file1);
            _path2 = Path.Combine(_testFolder, _file2);
        }

        [OneTimeSetUp]
        public void Setup()
        {
            using (File.Create(_path1)) { };
            using (File.Create(_path2)) { };
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            File.Delete(_path1);
            File.Delete(_path2);
        }

        [Test]
        public void should_return_files()
        {
            // Arrange
            var fi1 = new FileInfo(_path1);
            var fi2 = new FileInfo(_path2);
            var fileInfos = new FileInfo[] { fi1, fi2 };
            _wrapper.Setup(x => x.GetFiles(It.IsAny<DirectoryInfo>())).Returns(fileInfos);

            // Act
            var result = _processor.ExecuteGetFiles(_testFolder).ToList();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Count.EqualTo(fileInfos.Length));
            Assert.Multiple(() =>
            {
                Assert.That(result[0].Name, Is.EqualTo(Path.ChangeExtension(fileInfos[0].Name, null)));
                Assert.That(result[1].Name, Is.EqualTo(Path.ChangeExtension(fileInfos[1].Name, null)));
            });
        }

        [TestCase(5, false)]
        [TestCase(10, true)]
        public async Task should_execute_download(int chunkSize, bool isLastChunk)
        {
            // Arrange
            var start = 0;
            var content = "0123456789";
            var fi1 = new FileInfo(_path1);
            var fi2 = new FileInfo(_path2);
            var fileInfos = new FileInfo[] { fi1, fi2 };
            _wrapper.Setup(x => x.GetFiles(It.IsAny<DirectoryInfo>())).Returns(fileInfos);
            _wrapper.Setup(x => x.ReadToEndAsync(It.IsAny<string>())).ReturnsAsync(content);

            // Act
            var result = await _processor.ExecuteDownloadAsync(_file1,start, chunkSize, _testFolder);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.Name, Is.EqualTo(fi1.Name));
                Assert.That(result.Text, Is.EqualTo(content[start..(start + chunkSize)]));
                Assert.That(result.IsLastChunk, Is.EqualTo(isLastChunk));
            });
        }
    }
}