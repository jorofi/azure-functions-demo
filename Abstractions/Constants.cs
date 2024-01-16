namespace FunctionsDemo.Abstractions;

public static class Constants
{
    public const string EmailQueue = "email-queue";
    public const string SupportEmail = "georgi.filipov@uplink.bg";
    public const string SupportName = "Georgi Filipov";
    public const string ReceiverEmail = "georgi.p.filipov@gmail.com";

    public const string ServiceBusConnectionName = "ServiceBusConnection";
    public const string CosmosDBConnectionName = "CosmosDBConnection";
    public const string CosmosDBContainerName = "FuncDemoContainer";
    public const string CosmosDBDatabaseName = "FuncDemoOrdersDB";
    public const string CosmosDBLeaseContainerName = "leases";

    public readonly static Guid StoreId = Guid.Parse("ba1d263e-acb3-400c-b95c-e22241d83654");
}