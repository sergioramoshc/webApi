using Api.Models;
using Api.Security;
using Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _service;

        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<object>> Login([FromBody] CredentialsModel credentials)
        {
            if (credentials == null)
            {
                return BadRequest();
            }

            try
            {
                return Ok(await _service.Login(credentials));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }

        [AllowAnonymous]
        [HttpPost("NovaSenha")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<object>> NovaSenha([FromQuery] QpNovaSenha parametros)
        {
            if (parametros.Documento == null || parametros.DataNascimento == null || parametros.Senha == null)
            {
                return BadRequest();
            }

            try
            {
                return Ok(await _service.NovaSenha(parametros));
            }
            catch (Exception exception)
            {
                return BadRequest(exception);
            }
        }
    }
}