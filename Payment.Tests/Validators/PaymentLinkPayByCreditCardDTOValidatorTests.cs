using System;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Payment.Api.Models;
using Payment.Api.Resources;
using Payment.Api.Validators;
using Xunit;

namespace Payment.Tests.Validators
{
    public class PaymentLinkPayByCreditCardDTOValidatorTests
    {
        private readonly PaymentLinkPayByCreditCardDTOValidator _validator;

        public PaymentLinkPayByCreditCardDTOValidatorTests()
        {
            this._validator = new PaymentLinkPayByCreditCardDTOValidator();
        }

        #region Card Owner

        [Theory]
        [InlineData("Enis Kurtay")]
        [InlineData("Ali")]
        public async Task When_Card_Owner_Is_Valid_Should_Not_Have_Validation_Error(string cardOwner)
        {
            var model = new PaymentLinkPayByCreditCardDTO
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
        public async Task When_Card_Owner_Is_Valid_Should_Have_Validation_Error(string cardOwner)
        {
            var model = new PaymentLinkPayByCreditCardDTO
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
            var model = new PaymentLinkPayByCreditCardDTO
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
        public async Task When_Credit_Card_Number_Is_Valid_Should_Have_Validation_Error(string creditCardNumber)
        {
            var model = new PaymentLinkPayByCreditCardDTO
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
        public async Task When_Credit_Card_Number_Is_Valid_Type_Should_Have_Validation_Error(string creditCardNumber)
        {
            var model = new PaymentLinkPayByCreditCardDTO
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
            var model = new PaymentLinkPayByCreditCardDTO
            {
                IssueDate = issueDate
            };

            var result = await _validator.TestValidateAsync(model);

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
        [InlineData("12/20")]
        [InlineData("12/21")]
        public async Task When_Issue_Date_Is_Valid_Should_Have_Validation_Error(string IssueDate)
        {
            var model = new PaymentLinkPayByCreditCardDTO
            {
                IssueDate = IssueDate
            };

            var result = await _validator.TestValidateAsync(model);

            result.ShouldHaveValidationErrorFor(x => x.IssueDate)
                .WithErrorMessage(ErrorMessagesResources.IssueDateInvalid);
        }

        #endregion
    }
}