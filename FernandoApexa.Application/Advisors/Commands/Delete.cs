using FernandoApexa.Application.Core;
using FernandoApexa.Application.Interfaces;
using MediatR;

namespace FernandoApexa.Application.Advisors.Commands
{
    public class Delete
    {
        public class Command : IRequest<Result<Unit>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private IAdvisorRepository _advisorRepository;

            private readonly ICache<string, object> _mruCache;

            private readonly string cacheKey = "advisorsCacheKey";

            public Handler(IAdvisorRepository advisorRepository, ICache<string, object> mruCache)
            {
                _advisorRepository = advisorRepository;
                _mruCache = mruCache;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var key = $"advisor-{request.Id}";
                await _advisorRepository.DeleteAdvisor(request.Id);
                _mruCache.Delete(key);
                //we also need to clean the list cache
                _mruCache.Delete(cacheKey);

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}