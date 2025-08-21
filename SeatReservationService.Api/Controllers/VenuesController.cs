using Microsoft.AspNetCore.Mvc;
using SeatReservation.Application;

namespace SeatReservationService.Api.Controllers;

[ApiController]
[Route("api/venues")]
public class VenuesController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromServices] CreateVenueHandler handler,
        [FromBody] CreateVenueRequest request,
        CancellationToken cancellationToken)
    {
        var result = handler.Handle(request, cancellationToken);

        return Ok(result);
    }
}
