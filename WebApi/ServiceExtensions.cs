using System.Data;
using Data.Repositories;
using Data.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using Services;
using Services.Interfaces;

namespace WebApi;

public static class ServiceExtensions
{
    private static string GetEnvironmentVariableOrThrow(IConfiguration configuration, string key)
    {
        var value = configuration.GetValue<string>(key);
        if (string.IsNullOrWhiteSpace(value))
        {   
            throw new InvalidOperationException($"Environment variable '{key}' is required but was not found.");
        }
        return value;
    }
    
    private static string BuildConnectionString(IConfiguration configuration, IWebHostEnvironment environment)
    {
        string server = GetEnvironmentVariableOrThrow(configuration,"DB_SERVER");
        string database = GetEnvironmentVariableOrThrow(configuration,"DB_DATABASE");
        string user = GetEnvironmentVariableOrThrow(configuration,"DB_USER");
        string password = GetEnvironmentVariableOrThrow(configuration,"DB_PASSWORD");
        
        string encrypt = environment.IsDevelopment() ? "False" : "True";
        string trustCertificate = environment.IsDevelopment() ? "True" : "False";

        return $"Server={server};Database={database};User Id={user};Password={password};Encrypt={encrypt};TrustServerCertificate={trustCertificate};";
    }
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        string connectionString = BuildConnectionString(configuration, environment);
        services.AddScoped<IDbConnection>(sc => new SqlConnection(connectionString));
        return services;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        return services;
    }
    
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ISupplierService, SupplierService>();
        return services;
    }
}
