namespace ProjectNaam.WebApi.Services
{
    public interface IObjectService
    {
        public Task<Object2D> InsertAsync(Object2D Object2D);
        public Task<IEnumerable<Object2D>> ReadAsync(Guid id);
        public Task<IEnumerable<Object2D>> ReadAsync();
        public Task UpdateAsync(Object2D object2D);
        public Task DeleteAsync(Guid id);
    }
}
