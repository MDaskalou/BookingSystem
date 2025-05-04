using BookingSystem.Application.Commands.NotificationCommand.CreateNotification;
using BookingSystem.Application.Commands.NotificationCommand.DeleteNotification;
using BookingSystem.Application.Commands.NotificationCommand.UpdateNotification;
using BookingSystem.Application.DTO;
using BookingSystem.Application.Queries.QueriesNotification.GetAllNotifications;
using BookingSystem.Application.Queries.QueriesNotification.GetNotificationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationDto dto)
    {
        var notification = await _mediator.Send(new CreateNotificationCommand(dto));
        return CreatedAtAction(nameof(GetNotificationById), new { id = notification.NotificationId }, notification);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(int id)
    {
        var notification = await _mediator.Send(new GetNotificationByIdQuery(id));
        if (notification == null) return NotFound();
        return Ok(notification);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllNotifications()
    {
        var notifications = await _mediator.Send(new GetAllNotificationsQuery());
        return Ok(notifications);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotification(int id, [FromBody] CreateNotificationDto dto)
    {
        var updated = await _mediator.Send(new UpdateNotificationCommand(id, dto));
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        var deleted = await _mediator.Send(new DeleteNotificationCommand(id));
        if (!deleted) return NotFound();
        return Ok(new { message = "Notification successfully deleted" });
    }
}
