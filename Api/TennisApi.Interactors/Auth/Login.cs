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
using TennisApi.Shared.Crypto;

namespace TennisApi.Interactors.Auth
{
    public class Login : IRequest<TokenModel>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginValidator : AbstractValidator<Login>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }

    public class LoginHandler : IRequestHandler<Login, TokenModel>
    {
        private readonly IMediator _mediator;
        private TennisDBContext _dbContext;
        public LoginHandler(IMediator mediator, TennisDBContext dbContext)
        {
            this._mediator = mediator;
            this._dbContext = dbContext;
        }

        public async Task<TokenModel> Handle(Login request, CancellationToken cancellationToken)
        {
            var loginToken = (from l in _dbContext.Login
                              join p in _dbContext.Profile on l.ID equals p.LoginID
                              join pe in _dbContext.Person on p.PersonID equals pe.ID
                              where
                                l.Email == request.Email &&
                                l.Password == request.Password
                              select new TokenModel
                              {
                                  Email = l.Email,
                                  Nome = pe.Name,
                                  Status = l.Status,
                                  IDLogin = l.ID
                              }).FirstOrDefault();

            if (loginToken != null)
            {
                return loginToken;
            }

            return null;
        }
    }
}
