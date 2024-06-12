using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application;

namespace NotificationServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IWorker worker;

        public HomeController(IWorker worker)
        {
            this.worker = worker;
        }

        [HttpPost]
        public WorkerResult GetTemplate()
        {
            return new WorkerResult(); 
        }

        [HttpPost]
        public WorkerResult SetTemplate()
        {
            return new WorkerResult();
        }

        [HttpPost]
        public WorkerResult UpdateTemplate() 
        { 
            return new WorkerResult(); 
        }

        [HttpPost]
        public WorkerResult DeleteTemplate()
        {
            return new WorkerResult();
        }
    }
}
