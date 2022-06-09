using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextProcessor.Services
{
    public sealed class FileInfoConverter : IFileInfoConverter
    {
        public FileDto Convert(FileInfo fileInfo)
        {
            return new FileDto()
            {
                Name = fileInfo.Name,
                Size = fileInfo.Length,
                CreatedOn = fileInfo.CreationTimeUtc,
            };
        }
    }
}
