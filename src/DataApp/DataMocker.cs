namespace DataApp
{
    public static class DataMocker
    {
        public const string MOCKED_SOURCES_PATH = "C:\\NETDATASOURCES";
        public const string MOCKED_STORAGE_PATH = "C:\\NETDATASTORAGE";

        public static void MockDataStorage()
        {
            // Create source directory and empty files, adding text to each file
            Directory.CreateDirectory(MOCKED_SOURCES_PATH);
            for (int i = 0; i < 32; i++)
            {
                if (i == 10)
                {
                    using StreamWriter outputFile = new StreamWriter(Path.Combine(MOCKED_SOURCES_PATH, $"{i}.dat"));
                    outputFile.WriteLine("              ");
                }
                else if (i == 27)
                {
                    using StreamWriter outputFile = new StreamWriter(Path.Combine(MOCKED_SOURCES_PATH, $"{i}.dat"));
                    outputFile.WriteLine("");
                }
                else if (i == 31)
                {
                    using StreamWriter outputFile = new StreamWriter(Path.Combine(MOCKED_SOURCES_PATH, $"{i}.dat"));
                    outputFile.WriteLine("                           ");
                }
                else
                {
                    using StreamWriter outputFile = new StreamWriter(Path.Combine(MOCKED_SOURCES_PATH, $"{i}.dat"));
                    outputFile.WriteLine($"Text from file {i}");
                }
            }

            // Create storage directory and empty files
            Directory.CreateDirectory(MOCKED_STORAGE_PATH);
            for (int i = 0; i < 32; i++)
            {
                File.Create(Path.Combine(MOCKED_STORAGE_PATH, $"{i}.dat")).Dispose();
            }
        }

        public static void CleanUpDataStorage()
        {
            // Delete all source files and directory
            DirectoryInfo sourcesDirectoryInfo = new DirectoryInfo(MOCKED_SOURCES_PATH);
            foreach (FileInfo file in sourcesDirectoryInfo.GetFiles())
            {
                file.Delete();
            }
            Directory.Delete(MOCKED_SOURCES_PATH);

            // Delete all storage files and directory
            DirectoryInfo storageDirectoryInfo = new DirectoryInfo(MOCKED_STORAGE_PATH);
            foreach (FileInfo file in storageDirectoryInfo.GetFiles())
            {
                file.Delete();
            }
            Directory.Delete(MOCKED_STORAGE_PATH);
        }
    }
}
