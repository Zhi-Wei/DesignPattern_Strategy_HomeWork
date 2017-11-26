using System;
using NineYi.Mall.BE.Entities;
using NineYi.Mall.BE.Enums;
using NineYi.Mall.BL.Interfaces;

namespace NineYi.Mall.BL.Strategies
{
    /// <summary>
    /// 黑貓運費計算策略。
    /// </summary>
    /// <seealso cref="NineYi.Mall.BL.Interfaces.IFeeCalculationStrategy" />
    public class TCatStrategy : IFeeCalculationStrategy
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
            if (deliveryItem?.DeliveryType != DeliveryTypeEnum.TCat)
            {
                throw new ArgumentException($"請檢查 {nameof(deliveryItem)} 參數。");
            }

            double fee = deliveryItem.ProductWeight > 20
                         ? 400d
                         : 100 + deliveryItem.ProductWeight * 10;

            return fee;
        }
    }
}