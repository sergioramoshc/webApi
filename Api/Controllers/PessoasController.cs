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
    public class PessoasController : ControllerBase
    {
        private readonly IPessoasService _service;

        public PessoasController(IPessoasService service)
        {
            _service = service;
        }

        // GET: api/Pessoas/{id}
        [HttpGet("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblPessoas))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblPessoas>> GetById([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.GetById(id));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        // POST: api/Pessoas
        [HttpPost]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblPessoas))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblPessoas>> Create([FromBody] TblPessoas pessoa)
        {
            if (pessoa == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Create(pessoa));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        // PUT: api/Pessoas
        [HttpPut]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblPessoas))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblPessoas>> Update([FromBody] TblPessoas pessoa)
        {
            if (pessoa == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Update(pessoa));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        // DELETE: api/Pessoas/{id}
        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblPessoas))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblPessoas>> Delete([FromRoute] int id)
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