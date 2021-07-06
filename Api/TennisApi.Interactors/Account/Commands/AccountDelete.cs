using FluentValidation;
using TennisApi.Data.Model;
using TennisApi.Shared.Auth;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TennisApi.Interactors.Account.Commands
{

    public class AccountDelete : IRequest<Unit>
    {
        public Guid ID { get; set; }
        public TokenModel User { get; set; }
    }

    public class NotaNegociacaoDeleteHandler : IRequestHandler<AccountDelete, Unit>
    {
        private readonly IMediator _mediator;
        private TennisDBContext _dbContext;
        public NotaNegociacaoDeleteHandler(IMediator mediator, TennisDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<Unit> Handle(AccountDelete request, CancellationToken cancellationToken)
        {
            var account = (from n in _dbContext.Account
                         where
                           n.ID == request.ID
                         select n).FirstOrDefault();

            if(account != null)
            {
                account.Status = "deleted";
                _dbContext.SaveChanges();
            }

            return Unit.Value;
        }
    }
}
