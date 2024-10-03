using System;
using System.Collections.Generic;

namespace CourierService.Models.Entities;

public class Client
{
    public int ClientID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public decimal Discount { get; set; } = 0;
}

