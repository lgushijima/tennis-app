using FluentValidation;
using TennisApi.Data.Model;
using TennisApi.Shared.Auth;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TennisApi.Interactors.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class Login : IRequest<TokenModel>
    {
        public LoginRequest Data { get; set; }
    }

    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Data.Email).NotEmpty().WithMessage("Email não foi informado");
            RuleFor(x => x.Data.Senha).NotEmpty().WithMessage("Senha não informada");
        }
    }

    public class LoginHandler : IRequestHandler<Login, TokenModel>
    {
        private readonly IMediator _mediator;
        private innoFinDBContext _dbContext;
        public LoginHandler(IMediator mediator, innoFinDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<TokenModel> Handle(Login request, CancellationToken cancellationToken)
        {
            var loginToken = (from l in _dbContext.Login
                         join c in _dbContext.Cliente on l.ID equals c.IDLogin
                         where
                           l.Email == request.Data.Email &&
                           l.Senha == request.Data.Senha
                         select new TokenModel
                         {
                             Email = l.Email,
                             Nome = c.Nome,
                             IDStatus = l.IDStatus,
                             IDLogin = l.ID
                         }).FirstOrDefault();

            if (loginToken != null)
            {
                if (loginToken.IDStatus == 2)
                {
                    throw new Exception("Status inválido!");
                }

                return loginToken;
            }

            return null;
        }
    }
}
