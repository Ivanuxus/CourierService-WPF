using System;
using System.Collections.Generic;

namespace CourierService.Models.Entities;

public class Courier
{
    public int CourierID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
}

