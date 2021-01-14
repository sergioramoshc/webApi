using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Api.Models;
using Api.Services.Interfaces;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenhasController : ControllerBase
    {
        private readonly ISenhasService _service;

        public SenhasController(ISenhasService service)
        {
            _service = service;
        }

        // POST: api/Senhas
        [HttpPost]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblSenhas))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblSenhas>> Create([FromBody] TblSenhas senha)
        {
            if (senha == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Create(senha));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        // PUT: api/Senhas
        [HttpPut]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<TblSenhas>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblSenhas>> Update([FromBody] TblSenhas senha)
        {
            if (senha == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Update(senha));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        // DELETE: api/Senhas/{id}
        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblSenhas))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblSenhas>> Delete([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Delete(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}
