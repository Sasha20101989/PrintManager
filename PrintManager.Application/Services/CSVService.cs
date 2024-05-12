using CsvHelper;
using CsvHelper.Configuration;
using PrintManager.Application.Interfaces;
using System.Globalization;

namespace PrintManager.Application.Services
{
    public class CSVService : ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file)
        {
            StreamReader reader = new(file);

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true
            };

            CsvReader csv = new(reader, configuration);

            IEnumerable<T> records = csv.GetRecords<T>();

            return records;
        }
    }
}
