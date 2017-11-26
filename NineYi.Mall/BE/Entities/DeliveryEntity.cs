namespace NineYi.Mall.BE.Entities
{
    /// <summary>
    /// 宅配資料
    /// </summary>
    public class DeliveryEntity
    {
        /// <summary>
        /// 宅配類型
        /// </summary>
        public DeliveryTypeEnum DeliveryType { get; set; }

        /// <summary>
        /// 貨品長度
        /// </summary>
        public double ProductLength { get; set; }

        /// <summary>
        /// 貨品寬度
        /// </summary>
        public double ProductWidth { get; set; }

        /// <summary>
        /// 貨品高度
        /// </summary>
        public double ProductHeight { get; set; }

        /// <summary>
        /// 貨品重量
        /// </summary>
        public double ProductWeight { get; set; }
    }
}