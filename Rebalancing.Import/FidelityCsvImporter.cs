using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Rebalancing.Core;

namespace Rebalancing.Import
{
    public class FidelityCsvImporter : IFileImport
    {
        private const string _headerRow = "Run Date,Action,Symbol,Security Description,Security Type,Quantity,Price ($),Commission ($),Fees ($),Accrued Interest ($),Amount ($),Settlement Date";

        public IEnumerable<Transaction> Import(string filepath)
        {
            using (var reader = new StreamReader(filepath))
            {
                return Read(reader);
            };
        }

        public IEnumerable<Transaction> Import(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                return Read(reader);
            };
        }

        private IEnumerable<Transaction> Read(StreamReader reader)
        {
            List<Transaction> transactions = new List<Transaction>();

            using (var memoryStream = new MemoryStream())
            {
                using (var writer = new StreamWriter(memoryStream))
                {
                    bool headerFound = false;
                    bool readingBody = false;
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!headerFound && line.Contains(_headerRow))
                        {
                            writer.WriteLine(line);

                            headerFound = true;
                            readingBody = true;
                            continue;
                        }

                        if (headerFound && readingBody && !string.IsNullOrWhiteSpace(line))
                        {
                            writer.WriteLine(line);
                            continue;
                        }

                        if (headerFound && readingBody && string.IsNullOrWhiteSpace(line))
                        {
                            readingBody = false;
                            break;
                        }
                    }

                    writer.Flush();
                    memoryStream.Seek(0, SeekOrigin.Begin);

                    using (var fileBody = new StreamReader(memoryStream))
                    {
                        using (var csv = new CsvReader(fileBody, CultureInfo.InvariantCulture))
                        {
                            csv.Context.RegisterClassMap<FidelityTransactionMap>();
                            var records = csv.GetRecords<FidelityTransaction>();

                            foreach (var r in records)
                            {
                                transactions.Add(r.ToTransaction());
                            }

                            return transactions;
                        }
                    }
                }
            }
        }
    }
}