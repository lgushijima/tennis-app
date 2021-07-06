using FluentValidation;
using TennisApi.Data.Model;
using TennisApi.Shared.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TennisApi.Interactors.Person.Models;

namespace TennisApi.Interactors.Profile.Queries
{

    public class GetPersonByID : IRequest<GetPersonModel>
    {
        public Data.Model.Account Data { get; set; }
        public TokenModel User { get; set; }
    }

    public class GetPersonByIDValidator : AbstractValidator<GetPersonByID>
    {
        public GetPersonByIDValidator()
        {
            //RuleFor(x => x.Data.AnoMesFim).GreaterThanOrEqualTo(x=> x.Data.AnoMesInicio).WithMessage("A data final deve ser maior ou igual a data inicial");
        }
    }

    public class GetPersonByIDHandler : IRequestHandler<GetPersonByID, GetPersonModel>
    {
        private readonly IMediator _mediator;
        private TennisDBContext _dbContext;
        public GetPersonByIDHandler(IMediator mediator, TennisDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<GetPersonModel> Handle(GetPersonByID request, CancellationToken cancellationToken)
        {
            var lista = (from p in _dbContext.Profile
                         join pe in _dbContext.Person  on p.PersonID equals pe.ID
                         where
                            pe.ID == request.Data.ID
                         select new GetPersonModel
                         {
                             ProfileID = p.ID,

                             Person = pe,

                             Addresses = (from pa in _dbContext.PersonAddress
                                          join a in _dbContext.Address on pa.AddressID equals a.ID
                                          where pa.PersonID == pe.ID
                                          select a).ToArray(),

                             Phones = (from pp in _dbContext.PersonPhone
                                       join po in _dbContext.Phone on pp.PhoneID equals po.ID
                                       where pp.PersonID == pe.ID
                                       select po).ToArray(),

                             Emails = (from pem in _dbContext.PersonEmail
                                       join e in _dbContext.Email on pem.EmailID equals e.ID
                                       where pem.PersonID == pe.ID
                                       select e).ToArray(),

                         }).FirstOrDefault();

            return lista;
        }
    }
}
