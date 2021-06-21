using FluentValidation;
using TennisApi.Data.Model;
using TennisApi.Shared.Auth;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TennisApi.Interactors.NotaNegociacao.Comandos
{

    public class NotaNegociacaoSave : IRequest<Data.Model.NotaNegociacao>
    {
        public Data.Model.NotaNegociacao Data { get; set; }
        public TokenModel User { get; set; }
    }

    public class NotaNegociacaoSaveHandler : IRequestHandler<NotaNegociacaoSave, Data.Model.NotaNegociacao>
    {
        private readonly IMediator _mediator;
        private innoFinDBContext _dbContext;
        public NotaNegociacaoSaveHandler(IMediator mediator, innoFinDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<Data.Model.NotaNegociacao> Handle(NotaNegociacaoSave request, CancellationToken cancellationToken)
        {
            var nota = (from n in _dbContext.NotaNegociacao
                         where
                           n.IDNotaNegociacao == request.Data.IDNotaNegociacao &&
                           n.IDLogin == request.User.IDLogin
                         select n).FirstOrDefault();

            if(nota == null)
            {
                request.Data.IDNotaNegociacao = Guid.NewGuid();
                request.Data.DataCadastro = DateTime.UtcNow;
                request.Data.DataAlteracao = DateTime.UtcNow;
                request.Data.IDLogin = request.User.IDLogin;

                _dbContext.NotaNegociacao.Add(request.Data);
                await _dbContext.SaveChangesAsync();

                return request.Data;
            }
            else
            {
                nota.DataAlteracao = DateTime.UtcNow;
                nota.NomeCorretora = request.Data.NomeCorretora;
                nota.Numero = request.Data.Numero;
                nota.DataReferencia = request.Data.DataReferencia;
                nota.Tipo = request.Data.Tipo;

                nota.ValorLiquidoOperacoes = request.Data.ValorLiquidoOperacoes;
                nota.TaxaLiquidacao = request.Data.TaxaLiquidacao;
                nota.TaxaRegistro = request.Data.TaxaRegistro;
                
                nota.TaxaTermoOpcao = request.Data.TaxaTermoOpcao;
                nota.TaxaANA = request.Data.TaxaANA;
                nota.Emolumentos = request.Data.Emolumentos;

                nota.TaxaOperacional = request.Data.TaxaOperacional;
                nota.Execucao = request.Data.Execucao;
                nota.TaxaCustodia = request.Data.TaxaCustodia;
                nota.Impostos = request.Data.Impostos;
                nota.IRRF = request.Data.IRRF;
                nota.Outros = request.Data.Outros;
                
                await _dbContext.SaveChangesAsync();

                return nota;
            }
        }
    }
}
