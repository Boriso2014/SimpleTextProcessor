using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextProcessor.Services
{
    public sealed class FileDto
    {
        public string Name { get; set; } = default!;
        public long Size { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
