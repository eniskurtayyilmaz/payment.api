using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payment.Api.Extensions;
using Payment.Api.Services;
using Payment.Api.Validators;

namespace Payment.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
        }


        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddSingleton<IPaymentService, PaymentService>();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        public virtual void Configure(IApplicationBuilder app)
        {

            app.UseRouting();

            app.ConfigureHealthCheck();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}