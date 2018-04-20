using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EnergyRepository:IEnergyRepository
{
    private double currentEnergy;
    public double EnergyStored { get; private set; }

    public bool TakeEnergy(double energyNeeded)
    {
        if (this.EnergyStored < energyNeeded)
        {
            return false;
        }
        this.EnergyStored -= energyNeeded;
        return true;
    }

    public void StoreEnergy(double energy)
    {
        this.EnergyStored += energy;
    }
}
