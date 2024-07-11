using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;
using Infrustructure;
using AllRoadsLeadToRome.Core.MassTransit.Enums;

namespace NotificationServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        //private readonly IWorker worker;

        private readonly MongoDBService notificationsService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(MongoDBService service, ILogger<HomeController> logger)
        {
            notificationsService = service;
            _logger = logger;
        }

        [HttpGet("GetAllTemplates")]
        public async Task<List<Template>> GetAllTemplates()
        {
            return await notificationsService.GetTemplatesAsync();
        }

        [HttpGet("GetTemplate")]
        public async Task<Template?> GetTemplate(string name, NotificationType notificationType)
        {
            return await notificationsService.GetTemplateAsync(name, notificationType);
        }

        [HttpPost("CreateTemplate")]
        public async Task CreateTemplate(Template template)
        {
            await notificationsService.CreateTemplateAsync(template);
        }

        [HttpPut("UpdateTemplate")]
        public async Task UpdateTemplate(string name, NotificationType notificationType, Template template)
        {
            await notificationsService.UpdateTemplateAsync(name, notificationType, template);
        }

        [HttpDelete("DeleteTemplate")]
        public async Task DeleteTemplate(string name, NotificationType notificationType)
        {
            await notificationsService.RemoveTemplateAsync(name, notificationType);
        }
    }
}
