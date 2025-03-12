namespace ProjectNaam.WebApi.Services
{
    public interface IEnvironmentService
    {
        public Task<Environment2D> InsertAsync(Environment2D Environment2D);
        public Task<Environment2D?> ReadAsync(Guid id);
        public Task<IEnumerable<Environment2D>> ReadForUserAsync(Guid id);
        public Task<IEnumerable<Environment2D>> ReadAsync();
        public Task UpdateAsync(Environment2D environment);
        public Task DeleteAsync(Guid id);
    }
}
