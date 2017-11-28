﻿using System;
using FluentAssertions;
using NineYi.Mall.BE.Entities;
using NineYi.Mall.BE.Enums;
using NineYi.Mall.BL;
using Xunit;

namespace NineYi.MallTests.BL
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

        #endregion -- Calculate --
    }
}