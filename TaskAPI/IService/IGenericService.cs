namespace TaskAPI.IService
{
    public interface IGenericService<T, TRequest>
    {
        List<T> GetAll();
        List<T> GetAllPaginated(string? query = null, int currentPage = 0, int itemsPerPage = 10);
        T GetById(int id);
        List<T> Insert(TRequest item);
        List<T> Delete(int id);
        List<T> Update(int id, TRequest item);
    }
}
