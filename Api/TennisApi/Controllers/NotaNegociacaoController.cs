using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using TennisApi.Service.JWT;
using TennisApi.Shared.API;
using TennisApi.Shared.Configs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TennisApi.Interactors.NotaNegociacao.Queries;
using TennisApi.Interactors.NotaNegociacao.Comandos;

namespace TennisApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{apiVersion}/nota-negociacao")]
    public class NotaNegociacaoController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AuthConfig _config;

        public NotaNegociacaoController(IMediator mediator, IOptions<AuthConfig>  config)
        {
            _mediator = mediator;
            _config = config.Value;
        }

        /// <summary>
        /// Listar notas de negociação
        /// </summary>
        /// <param name="anoMes">Ano/mês. Ex: 201901</param>
        /// <param name="numero">Número da nota.</param>
        /// <param name="dataReferencia">Data do pregão.</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("list")]
        [ProducesResponseType(typeof(ApiResponse<List<Data.Model.NotaNegociacao>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> List(int? anoMes, int? numero, DateTime dataReferencia )
        {
            var request = new NotaNegociacaoListRequest
            {
                AnoMes = anoMes,
                Numero = numero
            };

            var command = new NotaNegociacaoList {
                Data = request,
                User = TokenService.ReadToken(User)
            };

            var result = await _mediator.Send(command);
            var response = new ApiResponse<List<Data.Model.NotaNegociacao>>(result);
            return Ok(response);
        }


        /// <summary>
        /// Salvar notas de negociação
        /// </summary>
        /// <param name="model">NotaNegociacao</param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("save")]
        [ProducesResponseType(typeof(ApiResponse<Data.Model.NotaNegociacao>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Save([FromBody] Data.Model.NotaNegociacao model)
        {
            var command = new NotaNegociacaoSave
            {
                Data = model,
                User = TokenService.ReadToken(User)
            };

            var result = await _mediator.Send(command);
            var response = new ApiResponse<Data.Model.NotaNegociacao>(result);
            return Ok(response);
        }

        /// <summary>
        /// Excluir nota de negociação
        /// </summary>
        /// <param name="IDNotaNegociacao">IDNotaNegociacao</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{idNotaNegociacao}/delete")]
        [ProducesResponseType(typeof(ApiResponse<Unit>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<IActionResult> Delete(Guid idNotaNegociacao)
        {
            var command = new NotaNegociacaoDelete
            {
                IDNotaNegociacao = idNotaNegociacao,
                User = TokenService.ReadToken(User)
            };

            var result = await _mediator.Send(command);
            var response = new ApiResponse<Unit>(result);
            return Ok(response);
        }
    }
}
