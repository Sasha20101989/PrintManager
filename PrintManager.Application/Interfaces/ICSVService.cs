using CsvHelper.Configuration;

namespace PrintManager.Application.Interfaces;

public interface ICSVService
{
    public IEnumerable<T> ReadCSV<T>(Stream file, CsvConfiguration configuration);
}
