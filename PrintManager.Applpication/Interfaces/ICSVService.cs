using PrintManager.Logic.Models;

namespace PrintManager.Applpication.Interfaces;

public interface ICSVService
{
    public IEnumerable<T> ReadCSV<T>(Stream file);
}
