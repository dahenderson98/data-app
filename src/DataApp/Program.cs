namespace DataApp;

public class Program
{
    public static void Main(string[] args)
    {
        var dataManager = new DataManager();

        // Queue up all data task threads
        var threads = new List<Thread>();
        for (int i = 0; i <= 10; i++)
        {
            var fileCounter = i;
            Thread thread = new Thread(() =>
            {
                Console.WriteLine($"Consolidated dataId {fileCounter}. Result=" + dataManager.ConsolidateDataFromSources(fileCounter));
            });
            threads.Add(thread);
        }

        Thread thread27 = new Thread(() =>
        {
            Console.WriteLine("Consolidated dataId 27. Result=" + dataManager.ConsolidateDataFromSources(27));
        });
        threads.Add(thread27);

        // Start all data task threads
        foreach (var thread in threads)
        {
            thread.Start();
        }

        // Block main thread until all threads complete
        foreach (var thread in threads)
        {
            thread.Join();
        }
        Console.WriteLine("Completed");
    }
}