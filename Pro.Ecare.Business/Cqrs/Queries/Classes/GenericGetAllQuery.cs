using Poc.Ecare.Infrastructure.Models.Classes;
using Poc.Ecare.Infrastructure.Models.DatabaseContext;
using Pro.Ecare.Business.Cqrs.Commands.Interfaces;
using Pro.Ecare.Business.Cqrs.Queries.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork.Interface;

namespace Pro.Ecare.Business.Cqrs.Queries.Classes
{
    public class GenericGetAllQuery<TEntity> : IGenericGetAllQuery<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        private IUnitOfWork<DatabaseContext> _unitOfWork;
        #endregion

        #region CONSTRUCTOR
        public GenericGetAllQuery(IUnitOfWork<DatabaseContext> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }
        #endregion

        #region Handle
        public async Task<IList<TEntity>> Handle(CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetRepository<TEntity>().GetAll();
        }
        #endregion
    }
}
