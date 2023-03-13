using Poc.Ecare.Infrastructure.Models.Classes;
using Poc.Ecare.Infrastructure.Models.DatabaseContext;
using Pro.Ecare.Business.Cqrs.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork.Interface;

namespace Pro.Ecare.Business.Cqrs.Commands.Classes
{
    public class GenericCreateCommand<TEntity> : IGenericCreateCommand<TEntity> where TEntity : Entity
    {
        #region ATTRIBUTES
        private IUnitOfWork<DatabaseContext> _unitOfWork;
        #endregion

        #region CONSTRUCTOR
        public GenericCreateCommand(IUnitOfWork<DatabaseContext> unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentException(nameof(unitOfWork));
        }
        #endregion

        #region Handle
        public async Task<TEntity> Handle(TEntity command, CancellationToken cancellationToken)
        {
            await _unitOfWork.GetRepository<TEntity>().Add(command);
            await _unitOfWork.Save();
            return command;
        }
        #endregion
    }
}
