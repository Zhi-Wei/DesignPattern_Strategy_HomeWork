using System;
using System.Collections.Generic;
using System.Linq;
using NineYi.Mall.BE.Entities;
using NineYi.Mall.BE.Enums;
using NineYi.Mall.BL.Strategies.Interfaces;

namespace NineYi.Mall.BL.Strategies
{
    /// <summary>
    /// 大榮運費計算策略。
    /// </summary>
    /// <seealso cref="NineYi.Mall.BL.Strategies.Interfaces.IFeeCalculationStrategy" />
    public class KtjStrategy : IFeeCalculationStrategy
    {
        /// <summary>
        /// 計算指定宅配項目的運費。
        /// </summary>
        /// <param name="deliveryItem">宅配項目。</param>
        /// <returns>
        /// 運費。
        /// </returns>
        /// <exception cref="ArgumentException">請檢查 deliveryItem 參數。</exception>
        public double Calculate(DeliveryEntity deliveryItem)
        {
            if (deliveryItem?.DeliveryType != DeliveryTypeEnum.KTJ)
            {
                throw new ArgumentException($"請檢查 {nameof(deliveryItem)} 參數。");
            }

            var dimensions = new List<double>
            {
                deliveryItem.ProductLength,
                deliveryItem.ProductWidth,
                deliveryItem.ProductHeight
            };

            double fee;
            double size = dimensions.Sum();

            if (dimensions.Any(x => x > 50))
            {
                fee = size * 0.00001 * 110 + 50;

                return fee;
            }

            fee = size * 0.00001 * 120;

            return fee;
        }
    }
}