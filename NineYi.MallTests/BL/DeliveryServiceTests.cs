using System;
using System.Collections.Generic;
using FluentAssertions;
using NineYi.Mall.BE.Entities;
using NineYi.Mall.BE.Enums;
using Xunit;

namespace NineYi.Mall.BL.Tests
{
    public class DeliveryServiceTests
    {
        #region -- 前置準備 --

        private DeliveryService GetSystemUnderTestInstance()
        {
            return new DeliveryService();
        }

        /// <summary>
        /// 宅配資料For黑貓
        /// </summary>
        public static IEnumerable<object[]> DeliveryItemForTCat
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        //// 要被計算的物件
                        new DeliveryEntity()
                        {
                            ProductLength = 30,
                            ProductWidth = 40,
                            ProductHeight = 50,
                            ProductWeight = 25,
                            DeliveryType = DeliveryTypeEnum.TCat
                        },
                        //// 預期運費
                        400d
                    },
                    new object[]
                    {
                        //// 要被計算的物件
                        new DeliveryEntity()
                        {
                            ProductLength = 60,
                            ProductWidth = 60,
                            ProductHeight = 80,
                            ProductWeight = 15,
                            DeliveryType = DeliveryTypeEnum.TCat
                        },
                        //// 預期運費
                        250d
                    }
                };
            }
        }

        /// <summary>
        /// 宅配資料For大榮
        /// </summary>
        public static IEnumerable<object[]> DeliveryItemForKTJ
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        //// 要被計算的物件
                        new DeliveryEntity()
                        {
                            ProductLength = 30,
                            ProductWidth = 40,
                            ProductHeight = 50,
                            ProductWeight = 25,
                            DeliveryType = DeliveryTypeEnum.KTJ
                        },
                        //// 預期運費
                        72.000000000000014d
                    },
                    new object[]
                    {
                        //// 要被計算的物件
                        new DeliveryEntity()
                        {
                            ProductLength = 60,
                            ProductWidth = 60,
                            ProductHeight = 80,
                            ProductWeight = 15,
                            DeliveryType = DeliveryTypeEnum.KTJ
                        },
                        //// 預期運費
                        366.8d
                    }
                };
            }
        }

        /// <summary>
        /// 宅配資料For郵局
        /// </summary>
        public static IEnumerable<object[]> DeliveryItemForPostOffice
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        //// 要被計算的物件
                        new DeliveryEntity()
                        {
                            ProductLength = 30,
                            ProductWidth = 40,
                            ProductHeight = 50,
                            ProductWeight = 25,
                            DeliveryType = DeliveryTypeEnum.PostOffice
                        },
                        //// 預期運費
                        330d
                    },
                    new object[]
                    {
                        //// 要被計算的物件
                        new DeliveryEntity()
                        {
                            ProductLength = 60,
                            ProductWidth = 60,
                            ProductHeight = 80,
                            ProductWeight = 15,
                            DeliveryType = DeliveryTypeEnum.PostOffice
                        },
                        //// 預期運費
                        316.8d
                    }
                };
            }
        }

        #endregion -- 前置準備 --

        #region -- Calculate --

        [Theory]
        [MemberData(nameof(DeliveryItemForTCat))]
        [MemberData(nameof(DeliveryItemForKTJ))]
        [MemberData(nameof(DeliveryItemForPostOffice))]
        [Trait(nameof(DeliveryService), "")]
        [Trait(nameof(DeliveryService), "Calculate")]
        public void Test_Calculate(DeliveryEntity deliveryItem, double expected)
        {
            // Arrange
            var sut = this.GetSystemUnderTestInstance();

            // Act
            double actual = sut.Calculate(deliveryItem);

            // Arrange
            actual.Should().Be(expected);
        }

        [Fact]
        [Trait(nameof(DeliveryService), "")]
        [Trait(nameof(DeliveryService), "Calculate")]
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
        [Trait(nameof(DeliveryService), "")]
        [Trait(nameof(DeliveryService), "Calculate")]
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
            var message = $"請檢查 deliveryType 參數。{Environment.NewLine}參數名稱: deliveryType";
            var sut = this.GetSystemUnderTestInstance();

            // Act
            Action action = () => sut.Calculate(deliveryItem);

            // Assert
            action.ShouldThrow<ArgumentException>().And.Message.Should().Be(message);
        }

        #endregion -- Calculate --
    }
}