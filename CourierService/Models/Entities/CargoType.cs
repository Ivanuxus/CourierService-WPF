using System;
using System.Collections.Generic;

namespace CourierService.Models.Entities;

public class CargoType
{
    public int CargoTypeID { get; set; }
    public string TypeName { get; set; }
    public string Description { get; set; }
}
