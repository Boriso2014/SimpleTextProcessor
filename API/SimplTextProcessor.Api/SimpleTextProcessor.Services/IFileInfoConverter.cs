using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTextProcessor.Services
{
    public interface IFileInfoConverter
    {
        FileDto Convert(FileInfo fileInfo);
    }
}
