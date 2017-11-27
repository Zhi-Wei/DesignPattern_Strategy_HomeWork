using NineYi.Mall.BE.Entities;

namespace NineYi.Mall.BL.Strategies.Interfaces
{
    /// <summary>
    /// 定義費用計算策略的方法。
    /// </summary>
    public interface IFeeCalculationStrategy
    {
        /// <summary>
        /// 計算指定宅配項目的運費。
        /// </summary>
        /// <param name="deliveryItem">宅配項目。</param>
        /// <returns>運費。</returns>
        double Calculate(DeliveryEntity deliveryItem);
    }
}