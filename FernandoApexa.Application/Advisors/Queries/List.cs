using AutoMapper;
using FernandoApexa.Application.Core;
using FernandoApexa.Application.Interfaces;
using MediatR;

namespace FernandoApexa.Application.Advisors.Queries
{
    public class List
    {
        public class Query : IRequest<Result<List<AdvisorDto>>>
        { }

        public class Handler : IRequestHandler<Query, Result<List<AdvisorDto>>>
        {
            private readonly IAdvisorRepository _advisorRepository;

            private readonly IMapper _mapper;

            private readonly ICache<string, object> _mruCache;

            private readonly string cacheKey = "advisorsCacheKey";

            public Handler(IAdvisorRepository advisorRepository, IMapper mapper, ICache<string, object> mruCache)
            {
                _mapper = mapper;
                _advisorRepository = advisorRepository;
                _mruCache = mruCache;
            }

            public async Task<Result<List<AdvisorDto>>> Handle(Query request, CancellationToken cancellationToken)
            {
                var getCache = _mruCache.Get(cacheKey);
                if (getCache == null)
                {
                    getCache = await _advisorRepository.GetAllAdvisors();
                    _mruCache.Put(cacheKey, getCache);
                }
                var advisorDto = _mapper.Map<List<AdvisorDto>>(getCache);

                return Result<List<AdvisorDto>>.Success(advisorDto);
            }
        }
    }
}