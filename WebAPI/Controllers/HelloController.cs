using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using WebAPI.DAL;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloController : ControllerBase
    {
        private readonly ILogger<HelloController> _logger;
        private readonly DBC _DBC;
        public HelloController(
            ILogger<HelloController> logger,
            DBC DBC)
        {
            _logger = logger;
            _DBC = DBC;
        }
        [HttpGet("{parameter?}")]
        [Authorize(Policy = APIKeyAuthorization.PolicyName)]
        public string Get(string parameter)
        {
            Func<string> badRequest = new Func<string>(() => //I show off
            {                
                Response.StatusCode = 400;
                return "";
            });
            if (string.IsNullOrEmpty(parameter)) {
                _logger.LogWarning("Parameter expected");
                return badRequest();
            }
            var res = _DBC.Responses.SingleOrDefault(s => s.Request.ToLower().Equals(parameter.ToLower()));
            if (res == null)
            {
                _logger.LogWarning($"Bad parameter: {parameter}");
                return badRequest();
            }
            else
            {
                _logger.LogInformation($"Request: {parameter} Response: {res.Response}");
                return res.Response;
            }
        }
    }
}