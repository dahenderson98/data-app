namespace DataApp;

public class DataFetcher : IDataFetcher
{
    // Implementation locked - do not change
    public string FetchData(int dataId)
    {
        return File.ReadAllText($"\\\\NETDATASOURCES\\{dataId}.dat");
    }
    // End of locked implementation
}

public interface IDataFetcher
{
    string FetchData(int dataId);
}