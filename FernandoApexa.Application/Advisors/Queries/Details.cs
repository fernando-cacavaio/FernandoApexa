using AutoMapper;
using FernandoApexa.Application.Core;
using FernandoApexa.Application.Interfaces;
using MediatR;

namespace FernandoApexa.Application.Advisors.Queries
{
    public class Details
    {
        public class Query : IRequest<Result<AdvisorDto>>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<AdvisorDto>>
        {
            private readonly IAdvisorRepository _advisorRepository;

            private readonly IMapper _mapper;

            private readonly ICache<string, object> _mruCache;

            public Handler(IAdvisorRepository advisorRepository, IMapper mapper, ICache<string, object> mruCache)
            {
                _mapper = mapper;
                _advisorRepository = advisorRepository;
                _mruCache = mruCache;
            }

            public async Task<Result<AdvisorDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var key = $"advisor-{request.Id}";

                var getCache = _mruCache.Get(key);

                if (getCache == null)
                {
                    getCache = await _advisorRepository.GetAdvisorById(request.Id);
                    _mruCache.Put(key, getCache);
                }

                var advisorDto = _mapper.Map<AdvisorDto>(getCache);

                return Result<AdvisorDto>.Success(advisorDto);
            }
        }
    }
}