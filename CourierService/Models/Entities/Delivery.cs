using System;
using System.Collections.Generic;

namespace CourierService.Models.Entities;

public class Delivery
{
    public int DeliveryID { get; set; }
    public int OrderID { get; set; }
    public DateTime DeliveryDate { get; set; }
    public decimal TotalPrice { get; set; }
}
