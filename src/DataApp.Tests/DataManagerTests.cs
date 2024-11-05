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
        var dataManager = GetDataManager(stringInFile: "valid data");

        // Act
        var result = dataManager.ConsolidateDataFromSources(dataId);

        // Assert
        Assert.Equal(0, result);
    }

    private DataManager GetDataManager(string stringInFile)
    {
        // Mock DataFetcher        
        var mockedFetcher = new Mock<IDataFetcher>();
        mockedFetcher.Setup(x => x.FetchData(It.IsAny<int>())).Returns(stringInFile);

        // Mock DataGetter
        var mockedStorage = new Mock<IDataStorage>();
        mockedStorage.Setup(x => x.StoreData(It.IsAny<int>(), It.IsAny<string>())).Verifiable();

        return new DataManager()
        {
            DataFetcher = mockedFetcher.Object,
            DataStorage = mockedStorage.Object
        };
    }
}
