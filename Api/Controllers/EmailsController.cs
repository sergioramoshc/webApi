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
    public class EmailsController : ControllerBase
    {
        private readonly IEmailsService _service;

        public EmailsController(IEmailsService service)
        {
            _service = service;
        }

        // POST: api/Emails
        [HttpPost]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblEmails))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblEmails>> Create([FromBody] TblEmails email)
        {
            if (email == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Create(email));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        // PUT: api/Emails
        [HttpPut]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblEmails))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblEmails>> Update([FromBody] TblEmails email)
        {
            if (email == null)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _service.Update(email));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        // DELETE: api/Emails/{id}
        [HttpDelete("{id}")]
        [Authorize("Bearer")]
        [ProducesResponseType(200, Type = typeof(TblEmails))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<TblEmails>> Delete([FromRoute] int id)
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
