using System.Collections.Generic;
using Xbehave;
using Silly.Bank.Domain;
using System;
using NSubstitute;
using Silly.Bank.Entities;
using System.Net.Http;
using Shouldly;

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

            "Then the transfer is rejected".x(() => {
                fixture.FakeTranferRepo
                    .DidNotReceive()
                    .MakeTransfer(Arg.Any<decimal>(), Arg.Any<string>(), Arg.Any<string>());

                response.EnsureSuccessStatusCode();

                var tranferResult = fixture.ConvertHttpResponseMessageToEntity<TransferResult>(response);
                tranferResult.Result.ShouldBeFalse();
            });
        }

        [Scenario]
        [Example(1500, 10)]
        public void ThereIsNotEnoughBalanceInTheAccount(decimal requestedAmount, decimal currentBalance, 
            TestFixture fixture, TransferRequest request, HttpResponseMessage response)
        {
            $"Given the user request to transfer {requestedAmount}".x(() => {
                fixture = new TestFixture();

                request = new TransferRequest
                {
                    UserId = Guid.NewGuid(),
                    Ammount = requestedAmount,
                    FromAccount = "1234567890",
                    ToAccount = "0987654321"
                };
            });

            "And the user is the owner of the account".x(() =>
            {
                fixture.FakeHttpClient
                    .GetList<Account>(Arg.Any<string>())
                    .Returns(new List<Account>
                    {
                        new Account { AccountNumber = "1234567890" }
                    });
            });

            $"And the balance of the account is {currentBalance}".x(() =>
            {
                fixture.FakeHttpClient
                    .Get<decimal>(Arg.Any<string>())
                    .Returns(currentBalance);
            });

            "When the request is being processed".x(async () => {

                var jsonRequest = fixture.ConvertToJsonStringContent(request);
                response = await fixture.Factory.CreateClient().PostAsync("api/transfers", jsonRequest);
            });

            "Then the transfer is rejected".x(() => {
                fixture.FakeTranferRepo
                    .DidNotReceive()
                    .MakeTransfer(Arg.Any<decimal>(), Arg.Any<string>(), Arg.Any<string>());

                response.EnsureSuccessStatusCode();

                var tranferResult = fixture.ConvertHttpResponseMessageToEntity<TransferResult>(response);
                tranferResult.Result.ShouldBeFalse();
            });
        }
    }
}
