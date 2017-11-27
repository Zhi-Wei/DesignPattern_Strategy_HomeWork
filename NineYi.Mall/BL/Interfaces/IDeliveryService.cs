using NineYi.Mall.BE.Entities;

namespace NineYi.Mall.BL.Interfaces
{
    /// <summary>
    /// 定義宅配服務的方法。
    /// </summary>
    public interface IDeliveryService
    {
        /// <summary>
        /// 計算運費。
        /// </summary>
        /// <param name="deliveryItem">宅配資料。</param>
        /// <returns>運費。</returns>
        double Calculate(DeliveryEntity deliveryItem);
    }
}