using System;

public class InfinityHarvester : Harvester
{
    private const int PermanetnDurability = 1000;
    private const int OreOutputDivider = 10;
    private double durability;
    public override double Durability
    {
        get => this.durability;
        protected set => this.durability = 1000;
    }

    public InfinityHarvester(int id, double oreOutput, double energyRequirement) : base(id, oreOutput / OreOutputDivider, energyRequirement)
    {
    }

   
}