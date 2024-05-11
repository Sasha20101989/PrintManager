using CsvHelper;
using CsvHelper.Configuration;
using PrintManager.Applpication.Interfaces;
using System.Globalization;

namespace PrintManager.Applpication.Services
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
