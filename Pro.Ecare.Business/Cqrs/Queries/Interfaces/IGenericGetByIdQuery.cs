using Poc.Ecare.Infrastructure.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pro.Ecare.Business.Cqrs.Queries.Interfaces
{
    public interface IGenericGetByIdQuery<TEntity> where TEntity : Entity
    {
        #region Handle
        Task<TEntity> Handle(object command, CancellationToken cancellationToken);
        #endregion
    }
}
