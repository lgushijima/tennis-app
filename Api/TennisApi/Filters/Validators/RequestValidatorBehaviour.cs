using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace TennisApi.Filters.Validators
{
    public class RequestValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            this._validators = validators;
        }

        public Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                            .Select(v => v.Validate(context))
                            .SelectMany(r => r.Errors)
                            .Where(f => f != null)
                            .ToList();

            if (failures.Count != 0)
            {
                var ex = new MissingFieldException("Alguns parametros não são válidos.");

                failures.ForEach(f => {
                    ex.Data.Add(f.PropertyName, f.ErrorMessage);
                });
                
                throw ex;
            }

            return next();
        }
    }
}
