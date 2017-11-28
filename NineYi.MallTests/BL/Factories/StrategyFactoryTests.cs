using System;
using FluentAssertions;
using NineYi.Mall.BE.Enums;
using NineYi.Mall.BL.Strategies;
using Xunit;

namespace NineYi.Mall.BL.Factories.Tests
{
    public class StrategyFactoryTests
    {
        #region -- CreateStrategy --

        [Fact()]
        [Trait(nameof(StrategyFactory), "")]
        [Trait(nameof(StrategyFactory), "CreateStrategy")]
        public void CreateStrategy_當傳入參數deliveryType的列舉值未定義宅配策略時_應拋出ArgumentException的例外狀況()
        {
            // Arrange
            var deliveryType = (DeliveryTypeEnum)99;
            var message = $"請檢查 deliveryType 參數。{Environment.NewLine}參數名稱: deliveryType";

            // Act
            Action action = () => StrategyFactory.CreateStrategy(deliveryType);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact()]
        [Trait(nameof(StrategyFactory), "")]
        [Trait(nameof(StrategyFactory), "CreateStrategy")]
        public void CreateStrategy_當傳入參數deliveryType為TCat時_應回傳TCatStrategy的實體()
        {
            // Arrange
            var deliveryType = DeliveryTypeEnum.TCat;

            // Act
            var actual = StrategyFactory.CreateStrategy(deliveryType);

            // Assert
            actual.Should().BeOfType<TCatStrategy>();
        }

        [Fact()]
        [Trait(nameof(StrategyFactory), "")]
        [Trait(nameof(StrategyFactory), "CreateStrategy")]
        public void CreateStrategy_當傳入參數deliveryType為KTJ時_應回傳KtjStrategy的實體()
        {
            // Arrange
            var deliveryType = DeliveryTypeEnum.KTJ;

            // Act
            var actual = StrategyFactory.CreateStrategy(deliveryType);

            // Assert
            actual.Should().BeOfType<KtjStrategy>();
        }

        [Fact()]
        [Trait(nameof(StrategyFactory), "")]
        [Trait(nameof(StrategyFactory), "CreateStrategy")]
        public void CreateStrategy_當傳入參數deliveryType為PostOffice時_應回傳PostOfficeStrategy的實體()
        {
            // Arrange
            var deliveryType = DeliveryTypeEnum.PostOffice;

            // Act
            var actual = StrategyFactory.CreateStrategy(deliveryType);

            // Assert
            actual.Should().BeOfType<PostOfficeStrategy>();
        }

        #endregion -- CreateStrategy --
    }
}