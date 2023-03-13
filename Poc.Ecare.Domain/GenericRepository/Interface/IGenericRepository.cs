using Poc.Ecare.Infrastructure.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Poc.Ecare.Domain.GenericRepository.Interface
{
	public interface IGenericRepository<TEntity> where TEntity : Entity
	{
		Task<TEntity> Add(TEntity entity);
		Task Add(IList<TEntity> entities);
		Task<TEntity> GetById(object keyValue);
		Task<TEntity> GetFirstOrDefault(
			Expression<Func<TEntity, bool>> predicate = null,
			string includes = null,
			bool disableTracking = true
		);
		Task<IList<TEntity>> GetAll();
		Task<IList<TEntity>> GetMuliple(
			Expression<Func<TEntity, bool>> predicate = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includes = null,
			bool disableTracking = true,
			int take = 0, int offset = 0
		);
		Task<IList<TEntity>> FromSqlRaw(string sql, params object[] parameters);
		TEntity Update(TEntity entity);
		void UpdateByState(TEntity entity);
		IList<TEntity> Update(IList<TEntity> entities);
		Task UpdateByState(IList<TEntity> entities);
		Task<TEntity> Delete(object id);
		void Delete(TEntity entityToDelete);
		void Delete(IList<TEntity> entities);
		Task<int> Count(Expression<Func<TEntity, bool>> predicate = null);
		Task<bool> Exists(Expression<Func<TEntity, bool>> predicate);
	}
}
