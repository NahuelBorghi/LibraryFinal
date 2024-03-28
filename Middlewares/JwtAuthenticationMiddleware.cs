using LibraryFinal.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly JwtService _jwtService;
    private readonly IAuthorizationPolicyProvider _policyProvider;

    public JwtAuthenticationMiddleware(RequestDelegate next, JwtService jwtService, IAuthorizationPolicyProvider policyProvider)
    {
        _next = next;
        _jwtService = jwtService;
        _policyProvider = policyProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").LastOrDefault();
        if (string.IsNullOrEmpty(token))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        var claimsPrincipal = _jwtService.ValidateToken(token);
        if (claimsPrincipal == null)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        context.User = claimsPrincipal;

        var role = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;

        var policyName = context.GetRouteValue("policyName")?.ToString();
        if (!string.IsNullOrEmpty(policyName) && !string.IsNullOrEmpty(role))
        {
            var policy = await _policyProvider.GetPolicyAsync(policyName);
            if (policy != null)
            {
                if (policyName == "AdminOnly")
                {
                    if (role != "Admin") 
                    {
                        context.Items["isAuthorized"] = true;
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }
                }
                else if (policyName == "UserOnly")
                {
                    if (role != "User")
                    {
                        context.Items["isAuthorized"] = true;
                    }
                    else
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }
                }
            }
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            return;
        }
        await _next(context);
    }
}
