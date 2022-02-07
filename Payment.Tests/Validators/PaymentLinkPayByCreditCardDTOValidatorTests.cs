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
            
            result.ShouldNotHaveValidationErrorFor(x=> x.CardOwner);
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
            
            result.ShouldHaveValidationErrorFor(x=> x.CardOwner).WithErrorMessage(ErrorMessagesResources.CardOwnerInvalid);
        }
        

        #endregion
    }
}