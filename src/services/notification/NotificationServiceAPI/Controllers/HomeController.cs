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

        private readonly TemplatesService notificationsService;

        public HomeController(TemplatesService service)
        {
            notificationsService = service;
        }

        [HttpGet("GetAllTemplates")]
        public async Task<List<Template>> GetAllTemplates()
        {
            return await notificationsService.GetTemplatesAsync();
        }

        [HttpGet("GetTemplate")]
        public async Task<Template?> GetTemplate(string name)
        {
            return await notificationsService.GetTemplateAsync(name);
        }

        [HttpPost("CreateTemplate")]
        public async Task CreateTemplate(Template template)
        {
            await notificationsService.CreateTemplateAsync(template);
        }

        [HttpPut("UpdateTemplate")]
        public async Task UpdateTemplate(string name, Template template)
        {
            await notificationsService.UpdateTemplateAsync(name, template);
        }

        [HttpDelete("DeleteTemplate")]
        public async Task DeleteTemplate(string name)
        {
            await notificationsService.RemoveTemplateAsync(name);
        }
    }
}
