using Moq;

namespace DataApp.Tests;

public class DataManagerTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    public void TestInvalidDataId(int dataId)
    {
        // Arrange
        var dataManager = GetDataManager(stringInFile: "");

        // Act
        var result = dataManager.ConsolidateDataFromSources(dataId);

        // Assert
        Assert.Equal(-1, result);
    }

    [Theory]
    [InlineData(30, "")]
    [InlineData(40, "                   ")]
    public void TestEmptyOrWhitespaceFiles(int dataId, string stringInFile)
    {
        // Arrange
        var dataManager = GetDataManager(stringInFile: stringInFile);

        // Act
        var result = dataManager.ConsolidateDataFromSources(dataId);

        // Assert
        Assert.Equal(-2, result);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(10)]
    [InlineData(100)]
    public void TestValidDataId(int dataId)
    {
        // Arrange
        Mock<IDataStorage> storageVerifiable = null;
        var dataManager = GetDataManager(stringInFile: "valid data", ref storageVerifiable);

        // Act
        var result = dataManager.ConsolidateDataFromSources(dataId);

        // Assert
        Assert.Equal(0, result);
        storageVerifiable.Verify();
    }

    private DataManager GetDataManager(string stringInFile)
    {
        Mock<IDataStorage> storageVerifiable = null;
        return GetDataManager(stringInFile, ref storageVerifiable);
    }

    private DataManager GetDataManager(string stringInFile, ref Mock<IDataStorage> storageVerifiable)
    {
        // Mock DataFetcher        
        var mockedFetcher = new Mock<IDataFetcher>();
        mockedFetcher.Setup(x => x.FetchData(It.IsAny<int>())).Returns(stringInFile);

        // Mock DataStorage
        storageVerifiable = new Mock<IDataStorage>();
        storageVerifiable.Setup(x => x.StoreData(It.IsAny<int>(), It.IsAny<string>())).Verifiable();

        // Inject dependencies
        return new DataManager()
        {
            DataFetcher = mockedFetcher.Object,
            DataStorage = storageVerifiable.Object
        };
    }
}
