using FernandoApexa.Application.Advisors.Commands;
using FernandoApexa.Application.Advisors.Queries;
using FernandoApexa.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FernandoApexa.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvisorsController : ApiBaseController
    {
        public AdvisorsController()
        {
        }

        [HttpGet] //api/advisor
        public async Task<IActionResult> GetAdvisors()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }

        [HttpGet("{id}")] //api/advisor/{id}
        public async Task<IActionResult> GetAdvisor(int id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdvisor(Advisor advisor)
        {
            return HandleResult(await Mediator.Send(new Create.Command { Advisor = advisor }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdvisor(int id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdvisor(int id, Advisor advisor)
        {
            advisor.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Advisor = advisor }));
        }
    }
}