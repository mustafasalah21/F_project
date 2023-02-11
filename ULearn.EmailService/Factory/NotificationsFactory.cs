// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationsFactory.cs" company="JustProtect">
//   Copyright (C) 2017. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using ULearn.EmailService.Implementation;

namespace ULearn.EmailService.Factory
{
    public static class NotificationsFactory
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}