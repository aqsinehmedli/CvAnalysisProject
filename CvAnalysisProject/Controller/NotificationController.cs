using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CvAnalysisSystemProject.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;


        [HttpGet("GetAllNotifications")]
        public async Task<IActionResult> GetAllNotifications([FromQuery] GetAllNotifications.GetAllNotificationsCommand request)
        {
            return Ok(await _sender.Send(request));
        }

        [HttpDelete("NotificationRemove")]
        public async Task<IActionResult> Delete([FromQuery] int id)
        {
            var request = new Application.CQRS.Notifications.Handlers.NotificationRemove.NotificationDeleteCommand { Id = id };
            return Ok(await _sender.Send(request));
        }
    }
}
