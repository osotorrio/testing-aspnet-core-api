using System.Collections.Generic;
using Xbehave;
using Silly.Bank.Domain;
using System;
using NSubstitute;
using Silly.Bank.Entities;
using System.Net.Http;

namespace Silly.Bank.Tests.Scenarios
{
    public class TransferScenarios
    {
        [Scenario]
        public void TheUserIsNotTheOwnerOfTheAccount(TestFixture fixture, TransferRequest request, HttpResponseMessage response)
        {
            "Given the user request to transfer some ammount of money".x(() => {
                fixture = new TestFixture();

                request = new TransferRequest
                {
                    UserId = Guid.NewGuid(),
                    Ammount = 1500,
                    FromAccount = "1234567890",
                    ToAccount = "0987654321"
                };
            });

            "And the user is not the owner of the account he is requesting the money from".x(() => 
            {
                fixture.FakeHttpClient
                    .GetList<Account>(Arg.Any<string>())
                    .Returns(new List<Account>
                    {
                        new Account { AccountNumber = "9999999999" }
                    });
            });

            "When the request is being processed".x(async () => {

                var jsonRequest = fixture.ConvertToJsonStringContent(request);
                response = await fixture.Factory.CreateClient().PostAsync("api/transfers", jsonRequest);
            });

            "Then the transfer is rejected".x(async () => {
                fixture.FakeTranferRepo
                    .DidNotReceive()
                    .MakeTransfer(Arg.Any<decimal>(), Arg.Any<string>(), Arg.Any<string>());

                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
            });
        }
    }
}
