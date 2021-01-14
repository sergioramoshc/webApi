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
    public class DocumentosController : ControllerBase
    {
        private readonly IDocumentosService _service;

        public DocumentosController(IDocumentosService service)
        {
            _service = service;
        }

        // POST: api/Documentos
        [HttpPost]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblDocumentos))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblDocumentos>> Create([FromBody] TblDocumentos documento)
        {
            if (documento == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Create(documento));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        // PUT: api/Documentos
        [HttpPut]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblDocumentos))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblDocumentos>> Update([FromBody] TblDocumentos documento)
        {
            if (documento == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Update(documento));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        // DELETE: api/Documentos/{id}
        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblDocumentos))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblDocumentos>> Delete([FromRoute] int id)
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
