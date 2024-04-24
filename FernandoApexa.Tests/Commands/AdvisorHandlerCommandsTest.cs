using AutoMapper;
using FernandoApexa.Application.Advisors;
using FernandoApexa.Application.Advisors.Commands;
using FernandoApexa.Application.Core;
using FernandoApexa.Application.Interfaces;
using FernandoApexa.Domain;
using FluentValidation.TestHelper;
using Moq;

namespace FernandoApexa.Application.UnitTests.Commands
{
    public class AdvisorHandlerCommandsTest
    {
        private readonly IMapper _mapper;
        private readonly Domain.Advisor _advisor;
        private readonly IHealthStatus _healthStatus;

        public AdvisorHandlerCommandsTest()
        {
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfiles>();
            });

            _mapper = mapperConfig.CreateMapper();

            _advisor = new Domain.Advisor()
            {
                Name = "Fernando Teste 1",
                Address = "Fernando Address",
                Phone = "12345678",
                SIN = "123456788",
                HealthStatus = "Green"
            };

            _healthStatus = new HealthStatus();
        }

        [Fact]
        public async Task Invalid_Advisor_SIN_Existed()
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

            var status = new HealthStatus();

            mockRepo.Setup(r => r.CreateAdvisor(_advisor));
            mockRepo.Setup(r => r.GetAllAdvisors()).ReturnsAsync(advisors);

            var handler = new Create.Handler(mockRepo.Object, _healthStatus, null);

            var result = await handler.Handle(new Create.Command() { Advisor = _advisor }, CancellationToken.None);

            Assert.Equal(result.Error, "Advisor already exists with this SIN number");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Name_cannot_be_empty(string name)
        {
            // Arrange
            var request = new Advisor
            {
                Name = name
            };
            var validator = new AdvisorValidator();

            // Act
            TestValidationResult<Advisor> result =
                validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(null)]
        public void SIN_cannot_be_empty(string sin)
        {
            // Arrange
            var request = new Advisor
            {
                Name = "Teste",
                SIN = sin
            };
            var validator = new AdvisorValidator();

            // Act
            TestValidationResult<Advisor> result =
                validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SIN);
        }

        [Theory]
        [InlineData("2222")]
        public void SIN_cannot_be_less_than_9(string sin)
        {
            // Arrange
            var request = new Advisor
            {
                Name = "Teste",
                SIN = sin
            };
            var validator = new AdvisorValidator();

            // Act
            TestValidationResult<Advisor> result =
                validator.TestValidate(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SIN);
        }
    }
}