using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NineYi.Mall.BE.Entities;
using NineYi.Mall.BE.Enums;
using Xunit;

namespace NineYi.Mall.BL.Tests
{
    public class DeliveryServiceByDelegateTests
    {
        #region -- 前置準備 --

        private DeliveryServiceByDelegate GetSystemUnderTestInstance()
        {
            return new DeliveryServiceByDelegate();
        }

        #endregion -- 前置準備 --

        #region -- Calculate --

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "Calculate")]
        public void Calculate_當傳入參數deliveryItem為Null時_應拋出ArgumentNullException的例外狀況()
        {
            // Arrange
            DeliveryEntity deliveryItem = null;
            var message = $"請檢查 deliveryItem 參數。{Environment.NewLine}參數名稱: deliveryItem";
            var sut = this.GetSystemUnderTestInstance();

            // Act
            Action action = () => sut.Calculate(deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentNullException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "Calculate")]
        public void Calculate_當傳入參數DeliveryType為未定義宅配策略的值時_應拋出ArgumentException的例外狀況()
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
            var message = $"請檢查 DeliveryType 參數。{Environment.NewLine}參數名稱: DeliveryType";
            var sut = this.GetSystemUnderTestInstance();

            // Act
            Action action = () => sut.Calculate(deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "Calculate")]
        public void Calculate_當傳入參數DeliveryType為TCat且ProductWeight大於20時_應回傳400的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 30,
                ProductWidth = 40,
                ProductHeight = 50,
                ProductWeight = 25,
                DeliveryType = DeliveryTypeEnum.TCat
            };
            double expected = 400;
            var sut = this.GetSystemUnderTestInstance();

            // Act
            double actual = sut.Calculate(deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "Calculate")]
        public void Calculate_當傳入參數DeliveryType為TCat且ProductWeight小於20時_應回傳相對的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 60,
                ProductWidth = 60,
                ProductHeight = 80,
                ProductWeight = 15,
                DeliveryType = DeliveryTypeEnum.TCat
            };
            double expected = 250;
            var sut = this.GetSystemUnderTestInstance();

            // Act
            double actual = sut.Calculate(deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "Calculate")]
        public void Calculate_當傳入參數DeliveryType為KTJ且長寬高皆小於50時_應回傳相對的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 30,
                ProductWidth = 40,
                ProductHeight = 50,
                ProductWeight = 25,
                DeliveryType = DeliveryTypeEnum.KTJ
            };
            double expected = 72.000000000000014;
            var sut = this.GetSystemUnderTestInstance();

            // Act
            double actual = sut.Calculate(deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "Calculate")]
        public void Calculate_當傳入參數DeliveryType為KTJ且長寬高皆大於50時_應回傳相對的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 60,
                ProductWidth = 60,
                ProductHeight = 80,
                ProductWeight = 15,
                DeliveryType = DeliveryTypeEnum.KTJ
            };
            double expected = 366.8;
            var sut = this.GetSystemUnderTestInstance();

            // Act
            double actual = sut.Calculate(deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "Calculate")]
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
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "Calculate")]
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

        #region -- CalculateTCatShippingFee --

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculateTCatShippingFee")]
        public void CalculateTCatShippingFee_當傳入參數deliveryItem為Null時_應拋出ArgumentException的例外狀況()
        {
            // Arrange
            DeliveryEntity deliveryItem = null;
            var message = $"請檢查 deliveryItem 參數。{Environment.NewLine}參數名稱: deliveryItem";
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            Action action = () => sut.InvokeStatic("CalculateTCatShippingFee", deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculateTCatShippingFee")]
        public void CalculateTCatShippingFee_當傳入參數DeliveryType不為TCat時_應拋出ArgumentException的例外狀況()
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
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            Action action = () => sut.InvokeStatic("CalculateTCatShippingFee", deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculateTCatShippingFee")]
        public void CalculateTCatShippingFee_當傳入參數DeliveryType為TCat且ProductWeight大於20時_應回傳400的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 30,
                ProductWidth = 40,
                ProductHeight = 50,
                ProductWeight = 25,
                DeliveryType = DeliveryTypeEnum.TCat
            };
            double expected = 400;
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            double actual = (double)sut.InvokeStatic("CalculateTCatShippingFee", deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculateTCatShippingFee")]
        public void CalculateTCatShippingFee_當傳入參數DeliveryType為TCat且ProductWeight小於20時_應回傳相對的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 60,
                ProductWidth = 60,
                ProductHeight = 80,
                ProductWeight = 15,
                DeliveryType = DeliveryTypeEnum.TCat
            };
            double expected = 250;
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            double actual = (double)sut.InvokeStatic("CalculateTCatShippingFee", deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        #endregion -- CalculateTCatShippingFee --

        #region -- CalculateKtjShippingFee --

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculateKtjShippingFee")]
        public void CalculateKtjShippingFee_當傳入參數deliveryItem為Null時_應拋出ArgumentException的例外狀況()
        {
            // Arrange
            DeliveryEntity deliveryItem = null;
            var message = $"請檢查 deliveryItem 參數。{Environment.NewLine}參數名稱: deliveryItem";
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            Action action = () => sut.InvokeStatic("CalculateKtjShippingFee", deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculateKtjShippingFee")]
        public void CalculateKtjShippingFee_當傳入參數DeliveryType不為KTJ時_應拋出ArgumentException的例外狀況()
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
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            Action action = () => sut.InvokeStatic("CalculateKtjShippingFee", deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculateKtjShippingFee")]
        public void CalculateKtjShippingFee_當傳入參數DeliveryType為KTJ且長寬高皆小於50時_應回傳相對的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 30,
                ProductWidth = 40,
                ProductHeight = 50,
                ProductWeight = 25,
                DeliveryType = DeliveryTypeEnum.KTJ
            };
            double expected = 72.000000000000014;
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            double actual = (double)sut.InvokeStatic("CalculateKtjShippingFee", deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculateKtjShippingFee")]
        public void CalculateKtjShippingFee_當傳入參數DeliveryType為KTJ且長寬高皆大於50時_應回傳相對的運費()
        {
            // Arrange
            var deliveryItem = new DeliveryEntity()
            {
                ProductLength = 60,
                ProductWidth = 60,
                ProductHeight = 80,
                ProductWeight = 15,
                DeliveryType = DeliveryTypeEnum.KTJ
            };
            double expected = 366.8;
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            double actual = (double)sut.InvokeStatic("CalculateKtjShippingFee", deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        #endregion -- CalculateKtjShippingFee --

        #region -- CalculatePostOfficeShippingFee --

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculatePostOfficeShippingFee")]
        public void CalculatePostOfficeShippingFee_當傳入參數deliveryItem為Null時_應拋出ArgumentException的例外狀況()
        {
            // Arrange
            DeliveryEntity deliveryItem = null;
            var message = $"請檢查 deliveryItem 參數。{Environment.NewLine}參數名稱: deliveryItem";
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            Action action = () => sut.InvokeStatic("CalculatePostOfficeShippingFee", deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculatePostOfficeShippingFee")]
        public void CalculatePostOfficeShippingFee_當傳入參數DeliveryType不為PostOffice時_應拋出ArgumentException的例外狀況()
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
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            Action action = () => sut.InvokeStatic("CalculatePostOfficeShippingFee", deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculatePostOfficeShippingFee")]
        public void CalculatePostOfficeShippingFee_當傳入參數DeliveryType為PostOffice且依重量計算的運費大於依材積計算的運費時_應回傳依重量計算的運費()
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
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            double actual = (double)sut.InvokeStatic("CalculatePostOfficeShippingFee", deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait(nameof(DeliveryServiceByDelegate), "")]
        [Trait(nameof(DeliveryServiceByDelegate), "CalculatePostOfficeShippingFee")]
        public void CalculatePostOfficeShippingFee_當傳入參數DeliveryType為PostOffice且依重量計算的運費小於依材積計算的運費時_應回傳依材積計算的運費()
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
            var sut = new PrivateType(typeof(DeliveryServiceByDelegate));

            // Act
            double actual = (double)sut.InvokeStatic("CalculatePostOfficeShippingFee", deliveryItem);

            // Assert
            actual.Should().Be(expected);
        }

        #endregion -- CalculatePostOfficeShippingFee --
    }
}