using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Payment.Api;
using Payment.Api.Models;
using Payment.Api.Resources;
using Payment.IntegrationTests.Common;
using Payment.IntegrationTests.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Payment.IntegrationTests
{
    public class IntegrationTests
    {
        private HttpClient TestClient;

        [SetUp]
        public void Setup()
        {
            var application = new WebApplicationFactory<Api.Startup>();
            TestClient = application.CreateClient();
        }

        [Test]
        public async Task Can_Call_WebApi()
        {
            var reqModel = new PaymentLinkPayByCreditCardRequestDTO
            {
                CardOwner = "Enis Kurtay",
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(reqModel), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Test]
        [TestCase("E")]
        [TestCase("E_")]
        [TestCase("EK_")]
        [TestCase("EK")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task When_Card_Owner_Is_Invalid_Should_Have_Validation_Error(string cardOwner)
        {
            var reqModel = new PaymentLinkPayByCreditCardRequestDTO
            {
                CardOwner = cardOwner,
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(reqModel), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseObj = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();

            responseObj.Should().NotBeNull();
            responseObj.Error.Should().NotBeNull();
            responseObj.Error.Should().NotBeEmpty();
            responseObj.Error.Should().Contain("Card owner");
        }

        [Test]
        [TestCase("Enis Kurtay")]
        [TestCase("Ali")]
        public async Task When_Card_Owner_Is_Valid_Should_Not_Have_Validation_Error(string cardOwner)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CardOwner = cardOwner
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseObj = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();

            responseObj.Should().NotBeNull();

            responseObj.Error.Should().NotBeNull();
            responseObj.Error.Should().NotBeNull();
            responseObj.Error.Should().NotBeEmpty();
        }

        [Test]
        [TestCase("5000-1234-5678-9001-0000")]
        [TestCase("5000 1234 5678 9001 0000")]
        [TestCase("5000-1234-5678-9001")]
        [TestCase("5000 1234 5678 9001")]
        [TestCase("E")]
        [TestCase("E_")]
        [TestCase("EK_")]
        [TestCase("EK")]
        [TestCase("EKEKxxxxEKEKxxxx")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task When_Credit_Card_Number_Is_Invalid_Should_Have_Validation_Error(string creditCardNumber)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CreditCardNumber = creditCardNumber
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseObj = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();


            responseObj.Should().NotBeNull();
            responseObj.Error.Should().NotBeNull();
        }

        [Test]
        [TestCase("50001234567890010000")]
        [TestCase("6250947000000014")]
        public async Task When_Credit_Card_Number_Is_Invalid_Type_Should_Have_Validation_Error(string creditCardNumber)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CreditCardNumber = creditCardNumber
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseObj = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();

            responseObj.Should().NotBeNull();
            responseObj.Error.Should().NotBeNull();
        }

        [Test]
        [TestCase("4012888888881881")] //Visa
        [TestCase("5204245250001488")] //Mastercard
        [TestCase("374251018720018")] //Amex
        public async Task When_Credit_Card_Number_Is_Valid_Should_Not_Have_Validation_Error(string creditCardNumber)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CreditCardNumber = creditCardNumber
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseObj = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();


            responseObj.Should().NotBeNull();
            responseObj.Error.Should().NotBeNull();
        }

        [Test]
        [TestCase("E")]
        [TestCase("E_")]
        [TestCase("EK_")]
        [TestCase("EK")]
        [TestCase("___")]
        [TestCase("EKE")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task When_CVC_Is_Invalid_Should_Have_Validation_Error(string cvc)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CVC = cvc
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseObj = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();


            responseObj.Should().NotBeNull();
            responseObj.Error.Should().NotBeNull();
        }

        [Test]
        [TestCase("012")]
        [TestCase("0123")]
        public async Task When_CVC_Is_Valid_Should_Not_Have_Validation_Error(string cvc)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CVC = cvc
            };


            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseObj = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();

            responseObj.Should().NotBeNull();
            responseObj.Error.Should().NotBeNull();
        }


        [Test]
        [TestCase("E")]
        [TestCase("E_")]
        [TestCase("EK_")]
        [TestCase("EK")]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public async Task When_Issue_Date_Is_Invalid_Should_Have_Validation_Error(string issueDate)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                IssueDate = issueDate
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseObj = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();

            responseObj.Should().NotBeNull();
            responseObj.Error.Should().NotBeNull();
        }

        [Test]
        [TestCase("12/24")]
        [TestCase("12/22")]
        [TestCase("01/23")]
        public async Task When_Issue_Date_Is_Valid_Should_Not_Have_Validation_Error(string issueDate)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                IssueDate = issueDate
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var responseObj = await response.Content.ReadFromJsonAsync<ErrorResponseModel>();

            responseObj.Should().NotBeNull();
            responseObj.Error.Should().NotBeNull();
        }


        [Test]
        [TestCase("Enis Kurtay", "4012888888881881", "12/24", "468")] //Visa
        [TestCase("Uras Koray", "5204245250001488", "12/24", "468")] //Mastercard
        [TestCase("Sena Ablak", "374251018720018", "12/24", "468")] //Amex
        public async Task When_All_Data_Correct_Should_Not_Have_Validation_Error(string cardOwner, string cardNumber,
            string issueDate, string cvc)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CardOwner = cardOwner,
                CreditCardNumber = cardNumber,
                IssueDate = issueDate,
                CVC = cvc
            };

            var response = await TestClient.PostAsync(Constant.PaymentLinkEndpoint,
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, Constant.ContentType));

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var responseObj = await response.Content.ReadFromJsonAsync<PaymentLinkPayByCreditCardResponseDTO>();

            responseObj.Should().NotBeNull();

            responseObj.ReceiptId.Should().NotBeEmpty();
            responseObj.ReceiptId.Should().NotBeNull();
        }
    }
}