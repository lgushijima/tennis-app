using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TennisApi.Filters.Exceptions;
using TennisApi.Interactors.Auth;
using TennisApi.Service.JWT;
using TennisApi.Shared.API;
using TennisApi.Shared.Auth;
using TennisApi.Shared.Configs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TennisApi.Shared.Crypto;

namespace TennisApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AuthConfig _config;

        public AuthController (IMediator mediator, IOptions<AuthConfig>  config)
        {
            _mediator = mediator;
            _config = config.Value;
        }


        /// <summary>
        /// Gerar um access token válido para o usuário
        /// </summary>
        /// <param name="request">Objeto com parametros de request</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("token")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Token([FromBody] Login request) //, [FromBody] AuthConfig model
        {
            var command = new Login
            {
                Email = request.Email,
                Password = new Crypto().Encrypt(request.Password)
            };

            var result = await _mediator.Send(command);
            if (result != null)
            {
                result.AccessToken = TokenService.GenerateToken(result, _config);
                var response = new ApiResponse<TokenModel>(result);
                return Ok(response);
            }

            throw new Exception("Invalid login name or password");
        }

        /// <summary>
        /// Encrypt password
        /// </summary>
        /// <param name="pwd">Password</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("teste")]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> teste(string pwd)
        {
            return Ok(new Crypto().Encrypt(pwd));
        }
    }
}
