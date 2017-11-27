using System;
using NineYi.Mall.BE.Entities;
using NineYi.Mall.BL.Factories;
using NineYi.Mall.BL.Interfaces;

namespace NineYi.Mall.BL
{
    /// <summary>
    /// 宅配服務。
    /// </summary>
    public class DeliveryService : IDeliveryService
    {
        /// <summary>
        /// 計算運費。
        /// </summary>
        /// <param name="deliveryItem">宅配資料。</param>
        /// <returns>
        /// 運費。
        /// </returns>
        /// <exception cref="ArgumentNullException">deliveryItem - 請檢查 deliveryItem 參數。</exception>
        public double Calculate(DeliveryEntity deliveryItem)
        {
            if (deliveryItem == null)
            {
                throw new ArgumentNullException(nameof(deliveryItem), $"請檢查 {nameof(deliveryItem)} 參數。");
            }

            var strategy = StrategyFactory.CreateStrategy(deliveryItem.DeliveryType);
            double fee = strategy.Calculate(deliveryItem);

            return fee;
        }
    }
}