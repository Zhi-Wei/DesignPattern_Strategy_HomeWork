using System;
using NineYi.Mall.BE.Entities;
using NineYi.Mall.BE.Enums;
using NineYi.Mall.BL.Strategies.Interfaces;

namespace NineYi.Mall.BL.Strategies
{
    /// <summary>
    /// 郵局運費計算策略。
    /// </summary>
    /// <seealso cref="NineYi.Mall.BL.Strategies.Interfaces.IFeeCalculationStrategy" />
    public class PostOfficeStrategy : IFeeCalculationStrategy
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
            if (deliveryItem?.DeliveryType != DeliveryTypeEnum.PostOffice)
            {
                throw new ArgumentException($"請檢查 {nameof(deliveryItem)} 參數。", nameof(deliveryItem));
            }

            double feeByWeight = deliveryItem.ProductWeight * 10 + 80;
            double feeByVolumetric =
                deliveryItem.ProductLength * deliveryItem.ProductWidth * deliveryItem.ProductHeight * 0.00001 * 110;

            double fee = Math.Max(feeByWeight, feeByVolumetric);

            return fee;
        }
    }
}