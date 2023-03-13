using Pro.Ecare.Business.Services.Interfaces;
using Poc.Ecare.Infrastructure.Models.Classes;
using Poc.Ecare.Infrastructure.Models.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork.Interface;
using Pro.Ecare.Business.Cqrs.Commands.Interfaces;
using Pro.Ecare.Business.Cqrs.Queries.Interfaces;
using System.Threading;

namespace Pro.Ecare.Business.Services.Classes
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        private IUnitOfWork<DatabaseContext> _unitOfWork;
        private IGenericGetAllQuery<TEntity> _genericGetAllQuery;
        private IGenericGetByIdQuery<TEntity> _genericGetByIdQuery;
        private IGenericCreateCommand<TEntity> _genericCreateCommand;
        private IGenericUpdateCommand<TEntity> _genericUpdateCommand;
        private IGenericDeleteQuery<TEntity> _genericDeleteQuery;
        #endregion

        #region CONSTRUCTOR
        public GenericService(
            IUnitOfWork<DatabaseContext> unitOfWork,
            IGenericGetAllQuery<TEntity> genericGetAllQuery,
            IGenericGetByIdQuery<TEntity> genericGetByIdQuery,
            IGenericCreateCommand<TEntity> genericCreateCommand,
            IGenericUpdateCommand<TEntity> genericUpdateCommand,
            IGenericDeleteQuery<TEntity> genericDeleteQuery
            )
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
            _genericGetAllQuery = genericGetAllQuery ?? throw new ArgumentException(nameof(genericGetAllQuery));
            _genericGetByIdQuery = genericGetByIdQuery ?? throw new ArgumentException(nameof(genericGetByIdQuery));
            _genericCreateCommand = genericCreateCommand ?? throw new ArgumentException(nameof(genericCreateCommand));
            _genericUpdateCommand = genericUpdateCommand ?? throw new ArgumentException(nameof(genericUpdateCommand));
            _genericDeleteQuery = genericDeleteQuery ?? throw new ArgumentException(nameof(genericDeleteQuery));
        }
        #endregion

        #region READ
        public async Task<IList<TEntity>> GetAllTEntitys()
        {
            //return await _unitOfWork.GetRepository<TEntity>().GetAll();
            return await _genericGetAllQuery.Handle(CancellationToken.None);
        }

        public async Task<IList<TEntity>> GetTEntitys(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includes = "", bool disableTracking = true,
            int take = 0, int offset = 0)
        {
            return await _unitOfWork.GetRepository<TEntity>().GetMuliple(
                predicate: predicate, orderBy: orderBy,
                includes: includes, disableTracking: disableTracking,
                take: take, offset: offset);
        }

        public async Task<TEntity> GetTEntityById(object id)
        {
            //return await _unitOfWork.GetRepository<TEntity>().GetById(id);
            return await _genericGetByIdQuery.Handle(id, CancellationToken.None);
        }

        public async Task<TEntity> GetFirstOrDefaultTEntity(
            Expression<Func<TEntity, bool>> predicate = null, 
            string includes = "", bool disableTracking = true)
        {
            return await _unitOfWork.GetRepository<TEntity>().GetFirstOrDefault(
                predicate: predicate, includes: includes,
                disableTracking: disableTracking);
        }

        public Task<IList<TEntity>> GetFromSqlRaw(
            string sql, params object[] parameters)
        {
            return _unitOfWork.GetRepository<TEntity>().FromSqlRaw(sql: sql, parameters: parameters);
        }
        #endregion

        #region ADD

        public async Task<TEntity> InsertTEntity(TEntity TEntity)
        {
            //await _unitOfWork.GetRepository<TEntity>().Add(TEntity);
            //await _unitOfWork.Save();
            //return TEntity;
            return await _genericCreateCommand.Handle(TEntity, CancellationToken.None);
        }

        public async Task<IList<TEntity>> InsertTEntity(IList<TEntity> TEntitys)
        {
            await _unitOfWork.GetRepository<TEntity>().Add(TEntitys);
            await _unitOfWork.Save();
            return TEntitys;
        }
        #endregion

        #region UPDATE

        public TEntity UpdateByStateTEntity(TEntity TEntity)
        {
            _unitOfWork.GetRepository<TEntity>().UpdateByState(TEntity);
            _unitOfWork.Save();
            return TEntity;
        }

        public TEntity UpdateTEntity(TEntity TEntity)
        {
            //_unitOfWork.GetRepository<TEntity>().Update(TEntity);
            //_unitOfWork.Save();
            //return TEntity;
            return _genericUpdateCommand.Handle(TEntity, CancellationToken.None);
        }

        public IList<TEntity> UpdateTEntity(IList<TEntity> TEntitys)
        {
            _unitOfWork.GetRepository<TEntity>().Update(TEntitys);
            _unitOfWork.Save();
            return TEntitys;
        }

        public async Task<IList<TEntity>> UpdateByStateTEntity(IList<TEntity> TEntitys)
        {
            await _unitOfWork.GetRepository<TEntity>().UpdateByState(TEntitys);
            return TEntitys;
        }
        #endregion

        #region DELETE

#pragma warning disable CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        public async Task<TEntity?> DeleteTEntity(object id)
#pragma warning restore CS8632 // L'annotation pour les types référence Nullable doit être utilisée uniquement dans le code au sein d'un contexte d'annotations '#nullable'.
        {
            //if (id is null) return null;
            //var deletedEntity = await _unitOfWork.GetRepository<TEntity>().Delete(id);
            //await _unitOfWork.Save();
            //return deletedEntity;
            return await _genericDeleteQuery.Handle(id, CancellationToken.None);
        }

        public void DeleteTEntity(TEntity TEntity)
        {
            _unitOfWork.GetRepository<TEntity>().Delete(TEntity);
            _unitOfWork.Save();
        }

        public void DeleteTEntity(IList<TEntity> TEntitys)
        {
            _unitOfWork.GetRepository<TEntity>().Delete(TEntitys);
            _unitOfWork.Save();
        }
        #endregion

        #region OTHER
        public async Task<bool> TEntitysExist(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _unitOfWork.GetRepository<TEntity>().Exists(predicate: predicate);
        }

        public async Task<int> CountTEntitys(Expression<Func<TEntity, bool>> predicate = null)
        {
            return await _unitOfWork.GetRepository<TEntity>().Count(predicate: predicate);
        }
        #endregion
    }
}
