namespace FirewoodAPI.Repositories
{
	public interface IRepository<TEntity> : IReadRepository<TEntity>
	{
		Task Add(TEntity entity);
		void Update(TEntity entity);
		void Delete(TEntity entity);
		Task Save();
	}

	public interface IReadRepository<TEntity>
	{
		Task<IEnumerable<TEntity>> GetAll();
		Task<TEntity> GetById<T>(T id);
	}
}
