namespace CourierService.Models.DTO
{
    public class OrderDTO
    {
        public int OrderID { get; set; }
        public int ClientID { get; set; }
        public DateTime OrderDate { get; set; }
        public string CargoDescription { get; set; }
        public int CargoTypeID { get; set; }
        public decimal BasePrice { get; set; }
        public int CourierID { get; set; }
        public int TransportID { get; set; }
    }
}
