using System.Collections.Generic;
using FluentAssertions;
using NineYi.Mall.BE;
using NineYi.Mall.BE.Entities;
using Xunit;

namespace NineYi.Mall.BL.Tests
{
    public class DeliveryServiceTests
    {
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

        [Theory]
        [MemberData(nameof(DeliveryItemForTCat))]
        [MemberData(nameof(DeliveryItemForKTJ))]
        [MemberData(nameof(DeliveryItemForPostOffice))]
        public void Test_Calculate(DeliveryEntity deliveryItem, double expected)
        {
            //// Arrange
            var actual = 0d;
            var target = new DeliveryService();

            //// Act
            actual = target.Calculate(deliveryItem);

            //// Arrange
            expected.Should().Be(actual);
        }
    }
}