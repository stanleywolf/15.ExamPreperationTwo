using System;

public abstract class Harvester:IHarvester
{
    private  const int InitialDurability = 1000;
    private const int ChangeDurabilityMode = 100;

    public int ID { get; }
    private double oreOutput;
    private double energyRequirement;

    public double OreOutput { get; protected set; }

    public double EnergyRequirement { get; protected set; }
    private double durability;

    public virtual double Durability
    {
        get { return this.durability; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException(string.Format(Constants.BrokeDurability,this.GetType().Name));
            }
            this.durability = value;
        }
    }
  

    protected Harvester(int id, double oreOutput, double energyRequirement)
    {
        this.ID = id;
        this.OreOutput = oreOutput;
        this.EnergyRequirement = energyRequirement;
        this.Durability = InitialDurability;
    }

    public virtual double Produce()
    {
        return OreOutput;
    }

    public virtual void Broke()
    {
        this.Durability -= ChangeDurabilityMode;
        
    }

    public override string ToString()
    {
        return this.GetType().Name + Environment.NewLine + $"Durability: {this.Durability}";
    }
}