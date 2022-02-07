using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using FluentAssertions;
using FluentValidation.TestHelper;
using Payment.Api.Models;
using Payment.Api.Resources;
using Payment.Api.Utils;
using Payment.Api.Validators;
using Xunit;

namespace Payment.Tests.Validators
{
    public class PaymentLinkPayByCreditCardDTOValidatorTests
    {
        private readonly PaymentLinkPayByCreditCardRequestDTOValidator _validator;

        public PaymentLinkPayByCreditCardDTOValidatorTests()
        {
            this._validator = new PaymentLinkPayByCreditCardRequestDTOValidator();
        }

        #region Card Owner

        [Theory]
        [InlineData("Enis Kurtay")]
        [InlineData("Ali")]
        public async Task When_Card_Owner_Is_Valid_Should_Not_Have_Validation_Error(string cardOwner)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CardOwner = cardOwner
            };

            var result = await _validator.TestValidateAsync(model);

            result.ShouldNotHaveValidationErrorFor(x => x.CardOwner);
        }

        [Theory]
        [InlineData("E")]
        [InlineData("E_")]
        [InlineData("EK_")]
        [InlineData("EK")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task When_Card_Owner_Is_Invalid_Should_Have_Validation_Error(string cardOwner)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CardOwner = cardOwner
            };

            var result = await _validator.TestValidateAsync(model);

            result.ShouldHaveValidationErrorFor(x => x.CardOwner)
                .WithErrorMessage(ErrorMessagesResources.CardOwnerInvalid);
        }

        #endregion

        #region Credit Card Number

        [Theory]
        [InlineData("4012888888881881")] //Visa
        [InlineData("5204245250001488")] //Mastercard
        [InlineData("374251018720018")] //Amex
        public async Task When_Credit_Card_Number_Is_Valid_Should_Not_Have_Validation_Error(string creditCardNumber)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CreditCardNumber = creditCardNumber
            };

            var result = await _validator.TestValidateAsync(model);

            result.ShouldNotHaveValidationErrorFor(x => x.CreditCardNumber);
        }

        [Theory]
        [InlineData("5000-1234-5678-9001-0000")]
        [InlineData("5000 1234 5678 9001 0000")]
        [InlineData("5000-1234-5678-9001")]
        [InlineData("5000 1234 5678 9001")]
        [InlineData("E")]
        [InlineData("E_")]
        [InlineData("EK_")]
        [InlineData("EK")]
        [InlineData("EKEKxxxxEKEKxxxx")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task When_Credit_Card_Number_Is_Invalid_Should_Have_Validation_Error(string creditCardNumber)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CreditCardNumber = creditCardNumber
            };

            var result = await _validator.TestValidateAsync(model);

            result.ShouldHaveValidationErrorFor(x => x.CreditCardNumber)
                .WithErrorMessage(ErrorMessagesResources.CreditCardNumberInvalid);
        }


        [Theory]
        [InlineData("50001234567890010000")]
        [InlineData("6250947000000014")]
        public async Task When_Credit_Card_Number_Is_Invalid_Type_Should_Have_Validation_Error(string creditCardNumber)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CreditCardNumber = creditCardNumber
            };

            var result = await _validator.TestValidateAsync(model);

            result.ShouldHaveValidationErrorFor(x => x.CreditCardNumber)
                .WithErrorMessage(ErrorMessagesResources.CreditCardTypeInvalid);
        }

        #endregion

        #region Issue Date

        [Theory]
        [InlineData("12/24")]
        [InlineData("12/22")]
        [InlineData("01/23")]
        public async Task When_Issue_Date_Is_Valid_Should_Not_Have_Validation_Error(string issueDate)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                IssueDate = issueDate
            };


            ClockUtils.Freeze();
            ClockUtils.SetDateTime(new DateTime(2021, 12, 1));

            var result = await _validator.TestValidateAsync(model);

            ClockUtils.UnFreeze();


            result.ShouldNotHaveValidationErrorFor(x => x.IssueDate);
        }


        [Theory]
        [InlineData("E")]
        [InlineData("E_")]
        [InlineData("EK_")]
        [InlineData("EK")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task When_Issue_Date_Is_Invalid_Should_Have_Validation_Error(string issueDate)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                IssueDate = issueDate
            };

            var result = await _validator.TestValidateAsync(model);

            result.ShouldHaveValidationErrorFor(x => x.IssueDate)
                .WithErrorMessage(ErrorMessagesResources.IssueDateInvalid);
        }


        [Theory]
        [InlineData("12/20", 2020, 12)]
        [InlineData("12/21", 2021, 12)]
        [InlineData("12/22", 2022, 12)]
        [InlineData("12/22", 2023, 12)]
        [InlineData("11/22", 2022, 12)]
        [InlineData("10/21", 2023, 01)]
        [InlineData("10", 2023, 01)]
        public async Task When_Issue_Date_Is_Invalid_With_ClockUtil_Should_Have_Validation_Error(string issueDate,
            int year, int month)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                IssueDate = issueDate
            };

            ClockUtils.Freeze();
            ClockUtils.SetDateTime(new DateTime(year, month, 1));

            var result = await _validator.TestValidateAsync(model);

            ClockUtils.UnFreeze();

            result.ShouldHaveValidationErrorFor(x => x.IssueDate)
                .WithErrorMessage(ErrorMessagesResources.IssueDateInvalid);
        }

        #endregion

        #region CVC

        [Theory]
        [InlineData("012")]
        [InlineData("0123")]
        public async Task When_CVC_Is_Valid_Should_Not_Have_Validation_Error(string cvc)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CVC = cvc
            };

            var result = await _validator.TestValidateAsync(model);

            result.ShouldNotHaveValidationErrorFor(x => x.CVC);
        }

        [Theory]
        [InlineData("E")]
        [InlineData("E_")]
        [InlineData("EK_")]
        [InlineData("EK")]
        [InlineData("___")]
        [InlineData("EKE")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public async Task When_CVC_Is_Invalid_Should_Have_Validation_Error(string cvc)
        {
            var model = new PaymentLinkPayByCreditCardRequestDTO
            {
                CVC = cvc
            };

            var result = await _validator.TestValidateAsync(model);

            result.ShouldHaveValidationErrorFor(x => x.CVC);
        }

        #endregion
    }
}