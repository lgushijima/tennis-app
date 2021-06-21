using FluentValidation;
using TennisApi.Data.Model;
using TennisApi.Shared.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TennisApi.Interactors.NotaNegociacao.Queries
{
    public class NotaNegociacaoListRequest
    {
        public Guid IDNotaNegociacao { get; set; }
        public int? Numero { get; set; }
        public int? AnoMes { get; set; }
    }

    public class NotaNegociacaoList : IRequest<List<Data.Model.NotaNegociacao>>
    {
        public NotaNegociacaoListRequest Data { get; set; }
        public TokenModel User { get; set; }
    }

    public class NotaNegociacaoListValidator : AbstractValidator<NotaNegociacaoList>
    {
        public NotaNegociacaoListValidator()
        {
            //RuleFor(x => x.Data.AnoMesFim).GreaterThanOrEqualTo(x=> x.Data.AnoMesInicio).WithMessage("A data final deve ser maior ou igual a data inicial");
        }
    }

    public class NotaNegociacaoListHandler : IRequestHandler<NotaNegociacaoList, List<Data.Model.NotaNegociacao>>
    {
        private readonly IMediator _mediator;
        private innoFinDBContext _dbContext;
        public NotaNegociacaoListHandler(IMediator mediator, innoFinDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<List<Data.Model.NotaNegociacao>> Handle(NotaNegociacaoList request, CancellationToken cancellationToken)
        {
            var lista = (from n in _dbContext.NotaNegociacao
                         where
                            (request.Data.IDNotaNegociacao == Guid.Empty || n.IDNotaNegociacao == request.Data.IDNotaNegociacao) &&
                            (request.Data.Numero == null || n.Numero == request.Data.Numero) &&
                            (request.Data.AnoMes == null || n.AnoMesReferencia == request.Data.AnoMes) &&
                            n.IDLogin == request.User.IDLogin
                         orderby n.AnoMesReferencia
                         select n).ToList();

            return lista;
        }
    }
}
