namespace CourierService.Models.DTO
{
    public class DeliveryDTO
    {
        public int DeliveryID { get; set; }
        public int OrderID { get; set; }
        public DateTime DeliveryDate { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
