using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.Net;
using Microsoft.Extensions.Hosting;
using Poc.Ecare.Infrastructure.Models.Classes;
using Pro.Ecare.Business.Services.Interfaces;
using AutoMapper;
using Pro.Ecare.Business.Cqrs.Commands.Interfaces;
using Pro.Ecare.Business.Cqrs.Queries.Interfaces;

namespace Poc.Ecare.Server.GenericController
{
    /// <summary>
    /// This is the generic Controller for all the controllers
    /// </summary>
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class GenericController<TEntity, TEntityViewModel> : Controller
        where TEntity : Entity
        where TEntityViewModel : Entity
    {
        #region ATTRIBUTES
        protected readonly IGenericService<TEntity> _genericService;
        protected readonly IMapper _mapper;
        #endregion

        #region CONTRUCTOR
        public GenericController(
            IGenericService<TEntity> genericService,
            IMapper mapper)
        {
            _genericService = genericService ?? throw new ArgumentException(nameof(genericService));
            _mapper = mapper ?? throw new ArgumentException(nameof(mapper));
        }
        #endregion

        #region READ
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            var list = await _genericService.GetAllTEntitys();
            if (list == null)
            {
                return NotFound(new 
                { 
                    Message = "List not found !",
                    StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                });
            }
            return Ok(_mapper.Map<IList<TEntityViewModel>>(list));
        }

        [Route("{id:int:min(1)}")]
        [HttpGet]
        public virtual async Task<IActionResult> Get(int id)
        {
            var row = await _genericService.GetTEntityById(id);
            if (row == null)
            {
                return NotFound(new
                {
                    Message = "Item not found !",
                    StatusCode = (int)HttpStatusCode.NotFound + " - " + HttpStatusCode.NotFound.ToString()
                });
            }
            return Ok(_mapper.Map<TEntityViewModel>(row));
        }
        #endregion

        #region ADD
        [HttpPost]
        public virtual async Task<IActionResult> Add([FromBody] TEntity entity)
        {
            try
            {
                var row = await _genericService.InsertTEntity(entity);
                return Ok(new 
                {
                    Entity = _mapper.Map<TEntityViewModel>(row),
                    Message = "Successfully added !"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Add failed !",
                    Exception = ex.Message,
                    ex.InnerException
                });
            }
        }
        #endregion

        #region UPDATE
        [HttpPut]
        public virtual IActionResult Update([FromBody] TEntity entity)
        {
            try
            {
                var row = _genericService.UpdateTEntity(entity);
                return Ok(new
                {
                    Entity = _mapper.Map<TEntityViewModel>(row),
                    Message = "Successfully updated !"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Update failed !",
                    Exception = ex.Message,
                    ex.InnerException
                });
            }
        }
        #endregion

        #region DELETE
        [Route("{id:int:min(1)}")]
        [HttpDelete]
        public virtual async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deletedRow = await _genericService.DeleteTEntity(id);
                return Ok(new
                {
                    Entity = _mapper.Map<TEntityViewModel>(deletedRow),
                    Message = "Successfully deleted !"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Delete failed !",
                    Exception = ex.Message,
                    ex.InnerException
                });
            }
        }
        #endregion
    }
}