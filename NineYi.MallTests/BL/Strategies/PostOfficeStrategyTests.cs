using System;
using FluentAssertions;
using NineYi.Mall.BE.Entities;
using NineYi.Mall.BE.Enums;
using Xunit;

namespace NineYi.Mall.BL.Strategies.Tests
{
    public class PostOfficeStrategyTests
    {
        #region -- 前置準備 --

        private PostOfficeStrategy GetSystemUnderTestInstance()
        {
            return new PostOfficeStrategy();
        }

        #endregion -- 前置準備 --

        #region -- Calculate --

        [Fact]
        [Trait(nameof(PostOfficeStrategy), "")]
        [Trait(nameof(PostOfficeStrategy), "Calculate")]
        public void Calculate_當傳入參數deliveryItem為Null時_應拋出ArgumentException的例外狀況()
        {
            // Arrange
            DeliveryEntity deliveryItem = null;
            var message = $"請檢查 deliveryItem 參數。{Environment.NewLine}參數名稱: deliveryItem";
            var sut = this.GetSystemUnderTestInstance();

            // Act
            Action action = () => sut.Calculate(deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(PostOfficeStrategy), "")]
        [Trait(nameof(PostOfficeStrategy), "Calculate")]
        public void Calculate_當傳入參數DeliveryType不為PostOffice時_應拋出ArgumentException的例外狀況()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 30,
                ProductWidth = 40,
                ProductHeight = 50,
                ProductWeight = 25,
                DeliveryType = (DeliveryTypeEnum)99
            };
            var message = $"請檢查 deliveryItem 參數。{Environment.NewLine}參數名稱: deliveryItem";
            var sut = this.GetSystemUnderTestInstance();

            // Act
            Action action = () => sut.Calculate(deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(PostOfficeStrategy), "")]
        [Trait(nameof(PostOfficeStrategy), "Calculate")]
        public void Calculate_當傳入參數DeliveryType為PostOffice且依重量計算的運費大於依材積計算的運費時_應回傳依重量計算的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 30,
                ProductWidth = 40,
                ProductHeight = 50,
                ProductWeight = 25,
                DeliveryType = DeliveryTypeEnum.PostOffice
            };
            double expected = 330;
            var sut = this.GetSystemUnderTestInstance();

            // Act
            double actual = sut.Calculate(deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait(nameof(PostOfficeStrategy), "")]
        [Trait(nameof(PostOfficeStrategy), "Calculate")]
        public void Calculate_當傳入參數DeliveryType為PostOffice且依重量計算的運費小於依材積計算的運費時_應回傳依材積計算的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 60,
                ProductWidth = 60,
                ProductHeight = 80,
                ProductWeight = 15,
                DeliveryType = DeliveryTypeEnum.PostOffice
            };
            double expected = 316.8;
            var sut = this.GetSystemUnderTestInstance();

            // Act
            double actual = sut.Calculate(deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        #endregion -- Calculate --
    }
}