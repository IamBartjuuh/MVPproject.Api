using Dapper;
using Microsoft.Data.SqlClient;

namespace ProjectNaam.WebApi.Repository
{
    public class Enviroment2DRepository
    {
        private readonly string sqlConnectionString;

        public Enviroment2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Enviroment2D> InsertAsync(Enviroment2D Enviroment2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var environmentId = await sqlConnection.ExecuteAsync("INSERT INTO [Enviroment2D] (Id, Name, OwnerUserId, MaxLength, MaxHeight) VALUES (@Id, @Name, @OwnerUserId, @MaxLength, @MaxHeight)", Enviroment2D);
                return Enviroment2D;
            }
        }

        public async Task<Enviroment2D?> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Enviroment2D>("SELECT * FROM [Enviroment2D] WHERE Id = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Enviroment2D>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Enviroment2D>("SELECT * FROM [Enviroment2D]");
            }
        }

        public async Task UpdateAsync(Enviroment2D environment)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Enviroment2D] SET " +
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
                await sqlConnection.ExecuteAsync("DELETE FROM [Enviroment2D] WHERE Id = @Id", new { id });
            }
        }

    }
}