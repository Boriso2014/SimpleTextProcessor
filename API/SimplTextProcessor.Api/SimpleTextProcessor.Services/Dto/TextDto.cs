namespace SimpleTextProcessor.Services.Dto
{
    public sealed class TextDto
    {
        public string Name { get; set; } = default!;
        public string Text { get; set; } = default!;
        public bool IsLastChunk { get; set; }
    }
}