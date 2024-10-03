namespace CourierService.Models.DTO
{
    public class ClientDTO
    {
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Discount { get; set; }
    }
}
