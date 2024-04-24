using AutoMapper;
using FernandoApexa.Application.Core;
using FernandoApexa.Application.Interfaces;
using FernandoApexa.Domain;
using FluentValidation;
using MediatR;

namespace FernandoApexa.Application.Advisors.Commands
{
    public class Edit
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
            private IAdvisorRepository _advisorRepository;
            public IMapper _mapper { get; }

            private readonly ICache<string, object> _mruCache;

            private readonly string cacheKey = "advisorsCacheKey";

            public Handler(IAdvisorRepository advisorRepository, IMapper mapper, ICache<string, object> mruCache)
            {
                _mapper = mapper;
                _advisorRepository = advisorRepository;
                _mruCache = mruCache;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var advisor = await _advisorRepository.GetAdvisorById(request.Advisor.Id);

                if (advisor == null) return null;

                _mapper.Map(request.Advisor, advisor);

                await _advisorRepository.UpdateAdvisor();

                var key = $"advisor-{request.Advisor.Id}";
                _mruCache.Put(key, advisor);

                //we also need to clean the list cache
                _mruCache.Delete(cacheKey);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}