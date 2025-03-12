using Dapper;
using Microsoft.Data.SqlClient;
using ProjectNaam.WebApi.Services;

namespace ProjectNaam.WebApi.Repository
{
    public class Object2DRepository : IObjectService
    {
        private readonly string sqlConnectionString;

        public Object2DRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task<Object2D> InsertAsync(Object2D Object2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var object2ds = await sqlConnection.ExecuteAsync("INSERT INTO [Object2D] (Id, EnvironmentId, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer) VALUES (@Id, @EnvironmentId, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer)", Object2D);
                return Object2D;
            }
        }

        public async Task<IEnumerable<Object2D>> ReadAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Object2D>("SELECT * FROM [Object2D] WHERE EnvironmentId = @Id", new { id });
            }
        }

        public async Task<IEnumerable<Object2D>> ReadAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<Object2D>("SELECT * FROM [Object2D]");
            }
        }

        public async Task UpdateAsync(Object2D object2D)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Object2D] SET " +
                                                 "PositionX = @PositionX, " +
                                                 "PositionY = @PositionY " +
                                                 "WHERE Id = @Id"
                                                 , object2D);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Object2D] WHERE Id = @Id", new { id });
            }
        }

    }
}