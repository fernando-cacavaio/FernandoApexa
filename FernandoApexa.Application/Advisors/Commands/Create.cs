using FernandoApexa.Application.Core;
using FernandoApexa.Application.Interfaces;
using FernandoApexa.Domain;
using FluentValidation;
using MediatR;

namespace FernandoApexa.Application.Advisors.Commands
{
    public class Create
    {
        public class Command : IRequest<Result<Unit>>
        {
            public Advisor Advisor { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Advisor).SetValidator(new AdvisorValidator());
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly IAdvisorRepository _advisorRepository;
            private readonly IHealthStatus _status;
            private readonly ICache<string, object> _mruCache;
            private readonly string cacheKey = "advisorsCacheKey";

            public Handler(IAdvisorRepository advisorRepository, IHealthStatus status, ICache<string, object> mruCache)
            {
                _advisorRepository = advisorRepository;
                _status = status;
                _mruCache = mruCache;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                //check if we already have this SIN number
                var advisors = await _advisorRepository.GetAllAdvisors();
                if (advisors.Any(x => x.SIN == request.Advisor.SIN))
                {
                    return Result<Unit>.Failure("Advisor already exists with this SIN number");
                }

                request.Advisor.HealthStatus = _status.GetRandomName();
                await _advisorRepository.CreateAdvisor(request.Advisor);

                var key = $"advisor-{request.Advisor.Id}";
                _mruCache.Put(key, request.Advisor);
                //we also need to clean the list cache
                _mruCache.Delete(cacheKey);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}