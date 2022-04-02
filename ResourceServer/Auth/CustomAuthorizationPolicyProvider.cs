using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading.Tasks;

namespace ResourceServer.Auth
{
    public class CustomAuthorizationPolicyProvider
    : DefaultAuthorizationPolicyProvider
    {
        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var parts = policyName.Split('.');
            var type = parts.First();
            var value = parts.Last();


            if (!string.IsNullOrWhiteSpace(policyName) && policyName.StartsWith("scope") && !string.IsNullOrWhiteSpace(value))
            {
                var policy = new AuthorizationPolicyBuilder()
                                .AddRequirements(new JwtScopeRequirement(value))
                                .Build();
                return Task.FromResult(policy);

            }


            return base.GetPolicyAsync(policyName);


        }

    }
}
