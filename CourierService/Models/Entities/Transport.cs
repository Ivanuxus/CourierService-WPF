using System;
using System.Collections.Generic;

namespace CourierService.Models.Entities;

public class Transport
{
    public int TransportID { get; set; }
    public string Type { get; set; }
    public string LicensePlate { get; set; }
}

