using Poc.Ecare.Infrastructure.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Ecare.Business.Services.Interfaces
{
    public interface IGenericService<TEntity> where TEntity : Entity
	{
        #region READ
        Task<IList<TEntity>> GetAllTEntitys();
		Task<IList<TEntity>> GetTEntitys(
			Expression<Func<TEntity, bool>> predicate = null,
			Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
			string includes = "",
			bool disableTracking = true,
			int take = 0, int offset = 0);
		Task<TEntity> GetTEntityById(object id);
		Task<TEntity> GetFirstOrDefaultTEntity(
			Expression<Func<TEntity, bool>> predicate = null,
			string includes = "",
			bool disableTracking = true);
		Task<IList<TEntity>> GetFromSqlRaw(string sql,
			params object[] parameters);
		#endregion

		#region CREATE
		Task<TEntity> InsertTEntity(TEntity TEntity);
		Task<IList<TEntity>> InsertTEntity(IList<TEntity> TEntitys);
		#endregion

		#region UPDATE
		TEntity UpdateTEntity(TEntity TEntity);
		TEntity UpdateByStateTEntity(TEntity TEntity);
		IList<TEntity> UpdateTEntity(IList<TEntity> TEntitys);
		Task<IList<TEntity>> UpdateByStateTEntity(IList<TEntity> TEntitys);
        #endregion

        #region DELETE
#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        Task<TEntity?> DeleteTEntity(object id);
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        void DeleteTEntity(TEntity TEntity);
		void DeleteTEntity(IList<TEntity> TEntitys);
		#endregion

		#region OTHER
		Task<int> CountTEntitys(Expression<Func<TEntity, bool>> predicate = null);
		Task<bool> TEntitysExist(Expression<Func<TEntity, bool>> predicate = null);
		#endregion
	}
}
