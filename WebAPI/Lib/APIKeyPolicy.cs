using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
{
    public static class APIKeyAuthorization
    {
        public const string PolicyName = "APIKeyPolicy";
        public class APIKeyRequirement : IAuthorizationRequirement { }
        public class APIKeyHandler : AuthorizationHandler<APIKeyRequirement>
        {
            private readonly DAL.DBC _dbc;
            private readonly ILogger<APIKeyRequirement> _logger;
            public APIKeyHandler(DAL.DBC dbc, ILogger<APIKeyRequirement> logger)
            {
                _dbc = dbc;
                _logger = logger;
            }
            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, APIKeyRequirement requirement)
            {
                if (context.Resource is HttpContext httpContext)
                {
                    var key = httpContext.Request.Cookies["API-Key"];
                    if (!string.IsNullOrEmpty(key) && _dbc.Clients.Any(a => a.Key.Equals(key)))
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        _logger.LogWarning("Unauthorized request from IP " + httpContext.Connection.RemoteIpAddress
                            + " sended API key: " + key);
                        context.Fail();
                    }
                }
                return Task.CompletedTask;
            }
        }
    }
}