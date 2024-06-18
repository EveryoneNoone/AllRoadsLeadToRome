using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using NotificationServiceAPI.Services;
using Infrustructure;

namespace NotificationServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        //private readonly IWorker worker;

        private readonly NotificationsService notificationsService;

        public HomeController(NotificationsService service)
        {
            notificationsService = service;
        }

        [HttpGet("GetTemplate")]
        public async Task<Template> GetTemplate(string id)
        {
            var template = await notificationsService.GetTemplateAsync(id);
            return template;
        }

        [HttpPost("SetTemplate")]
        public WorkerResult SetTemplate()
        {
            return new WorkerResult();
        }

        [HttpPut("UpdateTemplate")]
        public WorkerResult UpdateTemplate()
        {
            return new WorkerResult();
        }

        [HttpDelete("DeleteTemplate")]
        public WorkerResult DeleteTemplate()
        {
            return new WorkerResult();
        }
    }
}
