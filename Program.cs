using Microsoft.Azure.Cosmos;

public static class Program
{
    private static readonly string EndPointUri = "documentEndPoint";
    private static readonly string PrimaryKey = "Primary Key";
    private static CosmosClient _cosmosClient;
    private static Database _database;
    private static Container _container;


    private static string _databaseId = "az204Database";
    private static string _containerId = "az204Container";
    private static string _partitionKeyPath = "/LastName";

    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Beginning operations...\n");
            await CosmosAsync();
        }
        catch (CosmosException cosmosException)
        {
            Exception baseException = cosmosException.GetBaseException();
            Console.WriteLine("{0} error occurred: {1}", cosmosException.StatusCode, cosmosException);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }
        finally
        {
            Console.WriteLine("End of program, press any key to exit.");
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Manipulate Azure Cosmos Db
    /// </summary>
    /// <returns></returns>
    private static async Task CosmosAsync()
    {
        _cosmosClient = new CosmosClient(EndPointUri, PrimaryKey);

        await CreateDatabaseAsync();

        await CreateContainerAsync();
    }

    /// <summary>
    /// Create Azure Cosomos Db
    /// </summary>
    /// <returns></returns>
    private static async Task CreateDatabaseAsync()
    {
        // Create a new database using the cosmosClient
        _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);
        Console.WriteLine("Created Database: {0}\n", _database.Id);
    }

    /// <summary>
    /// Create Container
    /// </summary>
    /// <returns></returns>
    private static async Task CreateContainerAsync()
    {
        _container = await _database.CreateContainerIfNotExistsAsync(_containerId, _partitionKeyPath);
        Console.WriteLine("Created Container: {0}\n", _container.Id);
    }

}


