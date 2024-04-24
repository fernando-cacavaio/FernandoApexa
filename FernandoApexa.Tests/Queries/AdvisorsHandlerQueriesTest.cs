using AutoMapper;
using FernandoApexa.Application.Advisors;
using FernandoApexa.Application.Advisors.Queries;
using FernandoApexa.Application.Core;
using FernandoApexa.Application.Interfaces;
using FernandoApexa.Domain;
using Moq;
using Shouldly;

namespace FernandoApexa.Application.UnitTests.Queries
{
    public class AdvisorsHandlerQueriesTest
    {
        private readonly IMapper _mapper;

        public AdvisorsHandlerQueriesTest()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfiles>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetAdvisorsList_ShouldReturnList()
        {
            var advisors = new List<Advisor>
            {
                new Advisor
                {
                    Id = 1,
                    Name = "Fernando 1",
                    Address = "Address 1",
                    HealthStatus = "Green",
                    Phone = "12345678",
                    SIN = "123456789"
                },
                new Advisor
                {
                    Id = 2,
                    Name = "Fernando 2",
                    Address = "Address 2",
                    HealthStatus = "Green",
                    Phone = "12345677",
                    SIN = "123456788"
                }
            };

            var mockRepo = new Mock<IAdvisorRepository>();

            var mockCache = new Mock<ICache<string, object>>();

            mockRepo.Setup(r => r.GetAllAdvisors()).ReturnsAsync(advisors);

            var handler = new List.Handler(mockRepo.Object, _mapper, mockCache.Object);

            var result = await handler.Handle(new List.Query(), CancellationToken.None);

            result.Value.ShouldBeOfType<List<AdvisorDto>>();

            result.Value.Count.ShouldBe(2);
        }

        [Fact]
        public async Task GetAdvisorById_ShouldReturnFirst()
        {
            var advisor = new Advisor

            {
                Id = 1,
                Name = "Fernando 1",
                Address = "Address 1",
                HealthStatus = "Green",
                Phone = "12345678",
                SIN = "123456789"
            };

            var mockCache = new Mock<ICache<string, object>>();

            var mockRepo = new Mock<IAdvisorRepository>();

            mockRepo.Setup(r => r.GetAdvisorById(1)).ReturnsAsync(advisor);

            var handler = new Details.Handler(mockRepo.Object, _mapper, mockCache.Object);

            var result = await handler.Handle(new Details.Query { Id = 1 }, CancellationToken.None);

            result.Value.ShouldBeOfType<AdvisorDto>();

            Assert.Equal(result.Value.Id, 1);
        }

        [Fact]
        public async Task GetAdvisorById_ShouldReturnEmpty()
        {
            var advisor = new Advisor
            {
                Id = 1,
                Name = "Fernando 1",
                Address = "Address 1",
                HealthStatus = "Green",
                Phone = "12345678",
                SIN = "123456789"
            };

            var mockCache = new Mock<ICache<string, object>>();

            var mockRepo = new Mock<IAdvisorRepository>();

            mockRepo.Setup(r => r.GetAdvisorById(1)).ReturnsAsync(advisor);

            var handler = new Details.Handler(mockRepo.Object, _mapper, mockCache.Object);

            var result = await handler.Handle(new Details.Query { Id = 22 }, CancellationToken.None);

            Assert.Null(result.Value);
        }
    }
}