using Poc.Ecare.Domain.GenericRepository.Interface;
using Microsoft.EntityFrameworkCore;
using Poc.Ecare.Infrastructure.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.UnitOfWork.Interface
{
	public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
	{
		TContext DbContext { get; }
		IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : Entity;
		Task<int> Save();
	}
}
