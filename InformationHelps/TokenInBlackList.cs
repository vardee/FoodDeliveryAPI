using backendTask.DataBase.Models;
using backendTask.InformationHelps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace backendTask.Authorization
{
    public class TokenInBlackListHandler : AuthorizationHandler<TokenBlackListRequirment>
    {
        private readonly IServiceProvider _serviceProvider;

        public TokenInBlackListHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenBlackListRequirment requirement)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();

                string authorizationHeader = _serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Request.Headers["Authorization"].FirstOrDefault();

                if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                {
                    var token = authorizationHeader.Substring("Bearer ".Length);


                    var blackToken = db.BlackListTokens.FirstOrDefault(b => b.BlackToken == token);

                    if (blackToken != null)
                    {
                        context.Fail();
                    }
                    else
                    {
                        context.Succeed(requirement);
                    }
                }
                else
                {
                    context.Fail();
                }
            }

            return Task.CompletedTask;
        }
    }
}
