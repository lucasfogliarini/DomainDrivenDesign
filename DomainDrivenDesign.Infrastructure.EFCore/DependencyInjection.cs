using DomainDrivenDesign;
using DomainDrivenDesign.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DependencyInjection
	{
        public static void AddEFCoreDatabase(this IServiceCollection serviceCollection, EFCoreProvider efCoreProvider)
        {
            int retries = 3;
            int waitInSeconds = 1;
            for (int i = 1; i <= retries; i++)
            {
                try
                {
                    Console.WriteLine($"Adding {nameof(DatabaseContext)} ...");
                    Console.WriteLine($"Using {efCoreProvider} Provider");

                    serviceCollection.AddBoraDbContext(efCoreProvider);

                    serviceCollection.AddScoped<IDatabase, EFCoreDatabase>();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Successful connecting to the provider!");
                    return;
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error connecting to the provider! ({ex.Message})");
                    Thread.Sleep(waitInSeconds * 1000);
                    if(retries == 3)
                        throw;
                }
                finally
                {
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
        }

        private static void AddBoraDbContext(this IServiceCollection serviceCollection, EFCoreProvider efCoreProvider)
        {
            switch (efCoreProvider)
            {
                case EFCoreProvider.InMemory:
                    serviceCollection.AddDbContext<DatabaseContext>(options => options.UseInMemoryDatabase(nameof(DatabaseContext)));
                    break;
            }
        }
	}

    public enum EFCoreProvider
    {
        SqlServer,
        InMemory,
    }
}
