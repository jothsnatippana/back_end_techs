using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using MovieApi;

namespace Bdd.Specs
{
    public class CustomWebApplicationFactory : WebApplicationFactory<TestStartup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return base.CreateHostBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseEnvironment("Testing")
                   .UseSetting("https_port", "443")
                   .UseStartup<TestStartup>();
                });
        }
    }
}
