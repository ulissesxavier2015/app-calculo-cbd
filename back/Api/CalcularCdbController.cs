using Api.UseCases;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api;

[ApiController]
public class CalcularCdbController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpPost("/calcular-cdb")]
    public async Task<IActionResult> CalcularCdb(CalcularCdbRequest request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }
}
