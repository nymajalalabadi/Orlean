using Grains;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging.Configuration;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans;
using Microsoft.Extensions.Logging;

try
{
    var host = await StartSiloAsync();

    Console.WriteLine("\n\n Press Enter To Terminate....");
    Console.ReadLine();

    await host.StopAsync();
    return 0;
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    return 1;
}

static async Task<IHost> StartSiloAsync()
{
    var builder = new HostBuilder()
        .UseOrleans(c =>
        {
            c.UseLocalhostClustering()
            .Configure<ClusterOptions>(options =>
            {
                options.ClusterId = "dev";
                options.ClusterId = "OrleansBasics";
            })
            .ConfigureApplicationParts(part => part.AddApplicationPart(typeof(HelloGrain).Assembly).WithReferences())
            .ConfigureLogging(Logging => Logging.AddConsole());
        });

    var host = builder.Build();

    await host.StartAsync();

    return host;
}

