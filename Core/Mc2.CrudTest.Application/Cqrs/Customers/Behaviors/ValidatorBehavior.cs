using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System.Text;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Mc2.CrudTest.Application.Cqrs.Customers.Behaviors
{
    public class ValidatorBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidatorBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
            ValidationResult[] validationResults =
                await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
            List<ValidationFailure> failures = validationResults.SelectMany(x => x.Errors)
                .Where(x => x != null).ToList();

            if (!failures.Any()) return await next();

            StringBuilder errorBuilder = new StringBuilder();
            errorBuilder.AppendLine("Invalid command, reason: ");
            foreach (ValidationFailure error in failures)
            {
                errorBuilder.AppendLine(error.ErrorMessage);
            }

            throw new FluentValidation.ValidationException(failures);
        }
    }
}