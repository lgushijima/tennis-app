using FluentValidation;
using TennisApi.Data.Model;
using TennisApi.Shared.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TennisApi.Interactors.Account.Queries
{

    public class GetProfile : IRequest<List<Data.Model.Account>>
    {
        public Data.Model.Account Data { get; set; }
        public TokenModel User { get; set; }
    }

    public class AccountListValidator : AbstractValidator<GetProfile>
    {
        public AccountListValidator()
        {
            //RuleFor(x => x.Data.AnoMesFim).GreaterThanOrEqualTo(x=> x.Data.AnoMesInicio).WithMessage("A data final deve ser maior ou igual a data inicial");
        }
    }

    public class AccountListHandler : IRequestHandler<GetProfile, List<Data.Model.Account>>
    {
        private readonly IMediator _mediator;
        private TennisDBContext _dbContext;
        public AccountListHandler(IMediator mediator, TennisDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<List<Data.Model.Account>> Handle(GetProfile request, CancellationToken cancellationToken)
        {
            var lista = (from n in _dbContext.Account
                         where
                            (request.Data.ID == Guid.Empty || n.ID == request.Data.ID) &&
                            (request.Data.Name == null || request.Data.Name.Contains(n.Name))
                         orderby n.Name
                         select n).ToList();

            return lista;
        }
    }
}
