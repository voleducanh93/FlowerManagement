using System;
using System.Collections.Generic;

namespace Flower_Management.Entities;

public partial class Flower
{
    public int FlowerId { get; set; }

    public string Name { get; set; } = null!;

    public string? Color { get; set; }

    public string? Type { get; set; }

    public decimal? Price { get; set; }

    public int? StockQuantity { get; set; }
}
