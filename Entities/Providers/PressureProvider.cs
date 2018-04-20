using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PressureProvider:Provider
{
    private const double DecreaseDurability = 300;

    public PressureProvider(int id, double energyOutput) : base(id, energyOutput * 2)
    {
        this.Durability -= DecreaseDurability;
    }
}
