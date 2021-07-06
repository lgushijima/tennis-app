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

    public class AccountSave : IRequest<Data.Model.Account>
    {
        public Data.Model.Account Data { get; set; }
        public TokenModel User { get; set; }
    }

    public class NotaNegociacaoSaveHandler : IRequestHandler<AccountSave, Data.Model.Account>
    {
        private readonly IMediator _mediator;
        private TennisDBContext _dbContext;
        public NotaNegociacaoSaveHandler(IMediator mediator, TennisDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<Data.Model.Account> Handle(AccountSave request, CancellationToken cancellationToken)
        {
            var nota = (from n in _dbContext.Account
                         where n.ID == request.Data.ID
                         select n).FirstOrDefault();

            if(nota == null)
            {
                request.Data.ID = Guid.NewGuid();
                request.Data.Status = "active";

                _dbContext.Account.Add(request.Data);
                await _dbContext.SaveChangesAsync();

                return request.Data;
            }
            else
            {
                //nota.DataAlteracao = DateTime.UtcNow;
                nota.Name = request.Data.Name;
                nota.Description = request.Data.Description;
                await _dbContext.SaveChangesAsync();

                return nota;
            }
        }
    }
}
