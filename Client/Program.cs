using Grains.Interface;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;


try
{
    using (var client = await ConnectClientAsync())
    {
        await DoClientWrokAsync(client);
        Console.ReadKey();
    }
    return 0;
}
catch (Exception ex)
{
    Console.WriteLine($"\n Exceptin while trying to run client : {ex.Message}");
    Console.WriteLine("Make sure the silo the client is trying connect to is ruuing.");
    Console.WriteLine("\nPress any key to exit.");
    Console.ReadKey();
    return 1;
}


static async Task<IClusterClient> ConnectClientAsync()
{
    var client = new ClientBuilder()
        .UseLocalhostClustering()
        .Configure<ClusterOptions>(options =>
        {
            options.ClusterId = "dev";
            options.ClusterId = "OrleansBasics";
        })
        .ConfigureLogging(Logging => Logging.AddConsole())
        .Build();

    await client.Connect();

    Console.WriteLine("Client successfuly connect to the silo host! \n");

    return client;
}


static async Task DoClientWrokAsync(IClusterClient client)
{
    var friend = client.GetGrain<IHello>(0);

    var result = friend.SayHello("hello from nyma. ");

    Console.WriteLine($"\n\n {result} \n\n");
}