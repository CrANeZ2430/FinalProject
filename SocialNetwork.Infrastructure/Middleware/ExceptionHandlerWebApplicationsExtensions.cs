﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace SocialNetwork.Infrastructure.Middleware;

public static class ExceptionHandlerWebApplicationExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}
