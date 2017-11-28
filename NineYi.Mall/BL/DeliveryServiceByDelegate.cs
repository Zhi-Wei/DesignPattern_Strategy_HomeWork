using System;
using System.Collections.Generic;
using System.Linq;
using NineYi.Mall.BE.Entities;
using NineYi.Mall.BE.Enums;
using NineYi.Mall.BL.Interfaces;

namespace NineYi.Mall.BL
{
    /// <summary>
    /// 宅配服務透過委派。
    /// </summary>
    /// <seealso cref="NineYi.Mall.BL.Interfaces.IDeliveryService" />
    public class DeliveryServiceByDelegate : IDeliveryService
    {
        /// <summary>
        /// 運費計算策略集合。
        /// </summary>
        private static readonly IReadOnlyDictionary<DeliveryTypeEnum, Func<DeliveryEntity, double>> _strategies =
            new Dictionary<DeliveryTypeEnum, Func<DeliveryEntity, double>>
            {
                { DeliveryTypeEnum.TCat, CalculateTCatShippingFee },
                { DeliveryTypeEnum.KTJ, CalculateKtjShippingFee },
                { DeliveryTypeEnum.PostOffice, CalculatePostOfficeShippingFee }
            };

        /// <summary>
        /// 計算黑貓指定宅配項目的運費。
        /// </summary>
        /// <param name="deliveryItem">宅配項目。</param>
        /// <returns>運費。</returns>
        /// <exception cref="ArgumentException">deliveryItem</exception>
        private static double CalculateTCatShippingFee(DeliveryEntity deliveryItem)
        {
            if (deliveryItem?.DeliveryType != DeliveryTypeEnum.TCat)
            {
                throw new ArgumentException($"請檢查 {nameof(deliveryItem)} 參數。", nameof(deliveryItem));
            }

            double fee = deliveryItem.ProductWeight > 20
                         ? 400d
                         : 100 + deliveryItem.ProductWeight * 10;

            return fee;
        }

        /// <summary>
        /// 計算大榮指定宅配項目的運費。
        /// </summary>
        /// <param name="deliveryItem">宅配項目。</param>
        /// <returns>運費。</returns>
        /// <exception cref="ArgumentException">deliveryItem</exception>
        private static double CalculateKtjShippingFee(DeliveryEntity deliveryItem)
        {
            if (deliveryItem?.DeliveryType != DeliveryTypeEnum.KTJ)
            {
                throw new ArgumentException($"請檢查 {nameof(deliveryItem)} 參數。", nameof(deliveryItem));
            }

            var dimensions = new List<double>
            {
                deliveryItem.ProductLength,
                deliveryItem.ProductWidth,
                deliveryItem.ProductHeight
            };

            double fee;
            double size = dimensions.Aggregate((current, next) => current * next);

            if (dimensions.Any(x => x > 50))
            {
                fee = size * 0.00001 * 110 + 50;

                return fee;
            }

            fee = size * 0.00001 * 120;

            return fee;
        }

        /// <summary>
        /// 計算郵局指定宅配項目的運費。
        /// </summary>
        /// <param name="deliveryItem">宅配項目。</param>
        /// <returns>運費。</returns>
        /// <exception cref="ArgumentException">deliveryItem</exception>
        private static double CalculatePostOfficeShippingFee(DeliveryEntity deliveryItem)
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

        /// <summary>
        /// 計算運費。
        /// </summary>
        /// <param name="deliveryItem">宅配資料。</param>
        /// <returns>
        /// 運費。
        /// </returns>
        /// <exception cref="ArgumentNullException">deliveryItem - deliveryItem</exception>
        /// <exception cref="ArgumentException">DeliveryType - DeliveryType</exception>
        public double Calculate(DeliveryEntity deliveryItem)
        {
            if (deliveryItem == null)
            {
                throw new ArgumentNullException(nameof(deliveryItem), $"請檢查 {nameof(deliveryItem)} 參數。");
            }

            if (_strategies.TryGetValue(deliveryItem.DeliveryType, out var strategyDelegate) == false)
            {
                throw new ArgumentException(
                    $"請檢查 {nameof(deliveryItem.DeliveryType)} 參數。",
                    nameof(deliveryItem.DeliveryType));
            }

            double fee = strategyDelegate(deliveryItem);

            return fee;
        }
    }
}