using FluentValidation;
using TennisApi.Data.Model;
using TennisApi.Shared.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TennisApi.Interactors.Profile.Queries
{

    public class GetPersonById : IRequest<List<Data.Model.Profile>>
    {
        public Data.Model.Account Data { get; set; }
        public TokenModel User { get; set; }
    }

    public class FindProfileValidator : AbstractValidator<GetPersonById>
    {
        public FindProfileValidator()
        {
            //RuleFor(x => x.Data.AnoMesFim).GreaterThanOrEqualTo(x=> x.Data.AnoMesInicio).WithMessage("A data final deve ser maior ou igual a data inicial");
        }
    }

    public class FindProfileHandler : IRequestHandler<GetPersonById, List<Data.Model.Profile>>
    {
        private readonly IMediator _mediator;
        private TennisDBContext _dbContext;
        public FindProfileHandler(IMediator mediator, TennisDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<List<Data.Model.Profile>> Handle(GetPersonById request, CancellationToken cancellationToken)
        {
            var lista = (from p in _dbContext.Profile
                         where
                            (request.Data.ID == Guid.Empty || p.ID == request.Data.ID)
                         select p).ToList();

            return lista;
        }
    }
}
