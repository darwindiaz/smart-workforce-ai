using Microsoft.AspNetCore.Mvc;
using SmartWorkforce.Api.Conciliations.CreateConciliation;
using SmartWorkforce.Application.Conciliations.CreateConciliation;

namespace SmartWorkforce.Api.Conciliations;

[ApiController]
[Route("api/conciliations")]
public class CreateConciliationController : ControllerBase
{
    private readonly CreateConciliationHandler _handler;

    public CreateConciliationController(CreateConciliationHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateConciliation([FromBody] CreateConciliationRequest request)
    {
        var command = new CreateConciliationCommand(
            request.ConciliationType,
            request.BankAccountId,
            request.ConciliationPeriodDate,
            request.CreatedBy
        );
        var result = await _handler.Handle(command);
        return Created(
            $"/api/conciliations/{result.ConciliationId}",
            result
        );
    }


}