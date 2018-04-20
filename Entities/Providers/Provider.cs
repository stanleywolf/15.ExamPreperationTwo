using System;

public abstract class Provider:IProvider
{
    private const double InitialDurability = 1000;
    private const int ChangeDurabilityPerDay = 100;

    public int ID { get; private set; }
    public double EnergyOutput { get; private set; }
    private double durability;
    public double Durability {
        get { return this.durability; }
        protected set
        {
            if (value < 0)
            {
                throw new ArgumentException(string.Format(Constants.BrokeDurability, this.GetType().Name));
            }
            this.durability = value;
        }
    }

    protected Provider(int id, double energyOutput)
    {
        this.ID = id;
        this.EnergyOutput = energyOutput;
        this.Durability = InitialDurability;
    }
    public virtual double Produce()
    {
        return this.EnergyOutput;
    }

    public virtual void Broke()
    {
        this.Durability -= ChangeDurabilityPerDay;
    }

    public virtual void Repair(double val)
    {
        this.Durability += val;
    }

    public override string ToString()
    {
        return this.GetType().Name + Environment.NewLine + $"Durability: {this.Durability}";
    }
}