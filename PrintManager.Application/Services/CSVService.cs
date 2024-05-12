using CsvHelper;
using CsvHelper.Configuration;
using PrintManager.Application.Interfaces;

namespace PrintManager.Application.Services
{
    public class CSVService : ICSVService
    {
        public IEnumerable<T> ReadCSV<T>(Stream file, CsvConfiguration configuration)
        {
            StreamReader reader = new(file);

            CsvReader csv = new(reader, configuration);

            IEnumerable<T> records = csv.GetRecords<T>();

            return records;
        }
    }
}
