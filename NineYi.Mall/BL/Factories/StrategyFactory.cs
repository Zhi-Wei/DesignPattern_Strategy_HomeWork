using System;
using System.Collections.Generic;
using NineYi.Mall.BE.Enums;
using NineYi.Mall.BL.Strategies;
using NineYi.Mall.BL.Strategies.Interfaces;

namespace NineYi.Mall.BL.Factories
{
    /// <summary>
    /// 策略工廠。
    /// </summary>
    public class StrategyFactory
    {
        /// <summary>
        /// 已註冊的策略類別。
        /// </summary>
        private static readonly IReadOnlyDictionary<DeliveryTypeEnum, IFeeCalculationStrategy> _registry =
            new Dictionary<DeliveryTypeEnum, IFeeCalculationStrategy>
            {
                { DeliveryTypeEnum.TCat, new TCatStrategy() },
                { DeliveryTypeEnum.KTJ, new KtjStrategy() },
                { DeliveryTypeEnum.PostOffice, new PostOfficeStrategy() }
            };

        /// <summary>
        /// 依照宅配類型新建策略。
        /// </summary>
        /// <param name="deliveryType">宅配類型。</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">請檢查 deliveryType 參數。 - deliveryType</exception>
        public static IFeeCalculationStrategy CreateStrategy(DeliveryTypeEnum deliveryType)
        {
            if (_registry.TryGetValue(deliveryType, out var strategy) == false)
            {
                throw new ArgumentException($"請檢查 {nameof(deliveryType)} 參數。", nameof(deliveryType));
            }

            return strategy;
        }
    }
}