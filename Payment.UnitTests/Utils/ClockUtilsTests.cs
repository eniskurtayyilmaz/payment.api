using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Payment.Api.Utils;
using Xunit;

namespace Payment.UnitTests.Utils
{
    public class ClockUtilsTests
    {
        [Fact]
        public void Given_Frezeen_Then_Time_Equals_My_Previos_Freezen_Time()
        {
            //Arrange
            var datetimeNow = DateTime.Now;

            ClockUtils.Freeze(datetimeNow);

            //Action
            var result = ClockUtils.Now();

            //Assert
            result.Should().Be(datetimeNow);

            ClockUtils.UnFreeze();

        }


        [Fact]
        public void Given_Frezeen_Then_Time_Equals_My_Previos_Freezen_Time_V2()
        {
            //Arrange
            var datetimeNow = DateTime.Now;

            ClockUtils.Freeze(datetimeNow);
            ClockUtils.SetDateTime(datetimeNow);

            //Action
            var result = ClockUtils.Now();

            //Assert
            result.Should().Be(datetimeNow);

            ClockUtils.UnFreeze();

        }
    }
}
