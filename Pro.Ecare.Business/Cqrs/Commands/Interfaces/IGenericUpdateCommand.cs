using Poc.Ecare.Infrastructure.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Ecare.Business.Cqrs.Commands.Interfaces
{
    public interface IGenericUpdateCommand<TEntity> where TEntity : Entity
    {
        #region Handle
        TEntity Handle(TEntity command, CancellationToken cancellationToken);
        #endregion
    }
}
