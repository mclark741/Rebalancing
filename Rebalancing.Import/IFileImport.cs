using System.Collections.Generic;
using System.IO;
using System.Text;
using Rebalancing.Core;

namespace Rebalancing.Import
{
    public interface IFileImport
    {
        IEnumerable<Transaction> Import(string filePath);
        IEnumerable<Transaction> Import(Stream stream);
    }
}