using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ProductListManagement.Controllers
{
    public abstract class AbstractController : ControllerBase
    {
        public ILogger Logger { get; set; }

        public AbstractController(ILogger logger)
        {
            Logger = logger;
        }

        protected BadRequestObjectResult BadRequest(ValidationException exception)
        {
            return new BadRequestObjectResult(ModelState)
            {
                Value = new
                {
                    message = exception.Message
                }
            };
        }
    }
}
