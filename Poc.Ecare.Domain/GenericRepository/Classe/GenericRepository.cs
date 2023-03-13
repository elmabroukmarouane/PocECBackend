using Poc.Ecare.Domain.GenericRepository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Poc.Ecare.Infrastructure.Models.Classes;

namespace Poc.Ecare.Domain.GenericRepository.Classe
{
	public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
	{
        #region ATTRIBUTES
        protected readonly DbContext _dbContext;
		protected readonly DbSet<TEntity> _dbSet;
        #endregion

        #region CONSTROCTOR
        public GenericRepository(DbContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
			_dbSet = _dbContext.Set<TEntity>();
		}
		#endregion

		#region READ
		public virtual async Task<TEntity> GetById(object keyValue) => await _dbSet.FindAsync(keyValue);

		public virtual async Task<TEntity> GetFirstOrDefault(
			Expression<Func<TEntity, bool>> predicate = null,
			string includes = null,
			bool disableTracking = true
		)
		{
			IQueryable<TEntity> query = _dbSet;
			if (disableTracking)
			{
				query = query.AsNoTracking();
			}

			if (includes != null)
			{
				foreach (var include in includes.Split
					(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(include);
				}
			}
			return await query.FirstOrDefaultAsync(predicate);
		}

		public virtual async Task<IList<TEntity>> GetAll()
		{
			return await _dbSet.ToListAsync();
		}

		public virtual async Task<IList<TEntity>> GetMuliple(
			Expression<Func<TEntity, bool>> predicate = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includes = null,
			bool disableTracking = true,
			int take = 0, int offset = 0
		)
		{
			IQueryable<TEntity> query = _dbSet;

			if (disableTracking)
			{
				query = query.AsNoTracking();
			}

			if (includes != null)
			{
				foreach (var include in includes.Split
					(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(include);
				}
			}

			if (predicate != null)
			{
				query = query.Where(predicate);
			}

			if (offset > 0)
			{
				query = query.Skip(offset);
			}

			if (take > 0)
			{
				query = query.Take(take);
			}

			if (orderBy != null)
			{
				query = orderBy(query);
			}
			return await query.ToListAsync();
		}

		public virtual async Task<IList<TEntity>> FromSqlRaw(
			string sql,
			params object[] parameters
		)
		{
			return await _dbSet.FromSqlRaw(sql, parameters).ToListAsync();
		}
		#endregion

		#region CREATE
		public virtual async Task<TEntity> Add(TEntity entity)
		{
			await _dbSet.AddAsync(entity);
			return entity;
		}

		public virtual async Task Add(IList<TEntity> entities) => await _dbSet.AddRangeAsync(entities);
		#endregion

		#region UPDATE
		public virtual TEntity Update(TEntity entity)
		{
			_dbSet.Update(entity);
			return entity;
		}

		public virtual void UpdateByState(TEntity entity)
		{
			_dbContext.Entry(entity).State = EntityState.Modified;
		}

		public virtual IList<TEntity> Update(IList<TEntity> entities)
		{
			_dbSet.UpdateRange(entities);
			return entities;
		}

		public virtual async Task UpdateByState(IList<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				_dbContext.Entry(entity).State = EntityState.Modified;
				await _dbContext.SaveChangesAsync();
			}
		}
		#endregion

		#region DELETE
		public virtual async Task<TEntity> Delete(object id)
		{
			var entityToDelete = await _dbSet.FindAsync(id);

			if (entityToDelete != null)
			{
				_dbSet.Remove(entityToDelete);
			}
			return entityToDelete;
		}

		public virtual void Delete(TEntity entityToDelete)
		{
			if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
			{
				_dbSet.Attach(entityToDelete);
			}
			_dbSet.Remove(entityToDelete);
		}

		public virtual void Delete(IList<TEntity> entities) => _dbSet.RemoveRange(entities);
		#endregion

		#region OTHER
		public virtual async Task<int> Count(Expression<Func<TEntity, bool>> predicate = null)
		{
			if (predicate != null)
			{
				return await _dbSet.CountAsync(predicate);
			}
			return await _dbSet.CountAsync();
		}

		public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
		{
			if (predicate != null)
			{
				return await _dbSet.AnyAsync(predicate);
			}
			return await _dbSet.AnyAsync();
		}
		#endregion
	}
}
