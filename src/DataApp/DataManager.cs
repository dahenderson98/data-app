namespace DataApp;

public class DataManager
{
    public IDataFetcher DataFetcher { get; set; }
    public IDataStorage DataStorage { get; set; }
    
    protected object _writeLock;

    public DataManager()
    {
        DataFetcher = new DataFetcher();
        DataStorage = new DataStorage();
        _writeLock = new();
    }

    /// <summary>
    /// Consolidate the data from the DataFetcher sources into a centralized data store.
    /// </summary>
    /// <returns>-1 if an invalid dataId (less than 0) is provided, -2 if the data fetched from the supplied ID was null or whitespace,
    /// or 0 if we successfully consolidated.
    /// </returns>
    public int ConsolidateDataFromSources(int dataId)
    {
        if (dataId < 0)
        {
            return -1;
        }

        var data = DataFetcher.FetchData(dataId);
        if (string.IsNullOrWhiteSpace(data))
        {
            return -2;
        }

        lock (_writeLock)
        {
            DataStorage.StoreData(dataId, data);
        }

        return 0;
    }
}
