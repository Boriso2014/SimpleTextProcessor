namespace SimpleTextProcessor.Services.Dto
{
    public sealed class FileDto
    {
        public string Name { get; set; } = default!;
        public long Size { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
