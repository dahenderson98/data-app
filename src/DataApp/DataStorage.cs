namespace DataApp;

public class DataStorage : IDataStorage
{
    // Implementation locked - do not change
    public void StoreData(int dataId, string data)
    {
        File.AppendAllText($"\\\\NETDATASTORAGE\\{dataId}.dat", data);
    }
    // End of locked implementation
}

public interface IDataStorage
{
    void StoreData(int dataId, string data);
}