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

    public class NotaNegociacaoDelete : IRequest<Unit>
    {
        public Guid IDNotaNegociacao { get; set; }
        public TokenModel User { get; set; }
    }

    public class NotaNegociacaoDeleteHandler : IRequestHandler<NotaNegociacaoDelete, Unit>
    {
        private readonly IMediator _mediator;
        private innoFinDBContext _dbContext;
        public NotaNegociacaoDeleteHandler(IMediator mediator, innoFinDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<Unit> Handle(NotaNegociacaoDelete request, CancellationToken cancellationToken)
        {
            var nota = (from n in _dbContext.NotaNegociacao
                         where
                           n.IDNotaNegociacao == request.IDNotaNegociacao &&
                           n.IDLogin == request.User.IDLogin
                         select n).FirstOrDefault();

            if(nota != null)
            {
                var items = (from n in _dbContext.NotaNegociacaoItem
                             where n.IDNotaNegociacao == request.IDNotaNegociacao
                             select n).ToList();

                _dbContext.NotaNegociacaoItem.RemoveRange(items);
                _dbContext.NotaNegociacao.Remove(nota);
                await _dbContext.SaveChangesAsync();
            }

            return Unit.Value;
        }
    }
}
