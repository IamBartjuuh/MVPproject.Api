using Dapper;
using Microsoft.Data.SqlClient;
using ProjectNaam.WebApi.Services;

namespace ProjectNaam.WebApi.Repository
{
    public class Environment2DRepository
    {
        private readonly string sqlConnectionString;

        public Environment2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Environment2D> InsertAsync(Environment2D Environment2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var environmentId = await sqlConnection.ExecuteAsync("INSERT INTO [Environment2D] (Id, Name, OwnerUserId, MaxLength, MaxHeight) VALUES (@Id, @Name, @OwnerUserId, @MaxLength, @MaxHeight)", Environment2D);
                return Environment2D;
            }
        }

        public async Task<Environment2D?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Environment2D>("SELECT * FROM [Environment2D] WHERE Id = @Id", new { id });
            }
        }
        public async Task<Environment2D?> ReadForUserAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Environment2D>("SELECT * FROM [Environment2D] WHERE OwnerUserId = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Environment2D>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Environment2D>("SELECT * FROM [Environment2D]");
            }
        }

        public async Task UpdateAsync(Environment2D environment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Environment2D] SET " +
                                                 "Name = @Name, " +
                                                 "OwnerUserId = @OwnerUserId, " +
                                                 "MaxLength = @MaxLength, " +
                                                 "MaxHeight = @MaxHeight, " +
                                                 "WHERE Id = @Id"
                                                 , environment);

            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Environment2D] WHERE Id = @Id", new { id });
            }
        }

    }
}