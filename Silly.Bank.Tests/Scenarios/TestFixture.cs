using Microsoft.AspNetCore.Mvc.Testing;
using NSubstitute;
using Silly.Bank.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Silly.Bank.Tests.Scenarios
{
    public class TestFixture
    {
        public WebApplicationFactory<Startup> Factory { get; }

        public IHttpClient FakeHttpClient = Substitute.For<IHttpClient>();

        public ITransferRepository FakeTranferRepo = Substitute.For<ITransferRepository>();

        public TestFixture()
        {
            Factory = new WebApplicationFactory<Startup>();

            Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddScoped(typeof(IHttpClient), x => FakeHttpClient);
                    services.AddScoped(typeof(ITransferRepository), x => FakeTranferRepo);
                });
            });
        }

        public StringContent ConvertToJsonStringContent<TEntity>(TEntity data)
        {
            var json = JsonConvert.SerializeObject(data);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
