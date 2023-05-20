using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess.DbAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration configuration)
    {
        _config = configuration;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>(string sql, U parametrs, string connectionId = "Default")
    {

        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        return await connection.QueryAsync<T>(sql, parametrs);
    }


    public async Task SaveData<T>(string sql, T parametrs, string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(sql, parametrs);
    }


}
