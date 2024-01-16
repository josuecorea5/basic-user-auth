namespace FirewoodAPI.Services
{
	public interface ICommonService<T, TI, TU>
	{
		Task<IEnumerable<T>> GetAll();
		Task<T> GetById(Guid id);
		Task<T> Add(TI item);
		Task<T> Update(Guid id, TU item);
		Task<T> DeleteById(Guid id);
	}
}
