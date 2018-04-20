using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

[TestFixture]
public class Class1
{
    private ProviderController providerController;
    private EnergyRepository energyRepository;
    [SetUp]
    public void SetUpProvContr()
    {
        this.energyRepository = new EnergyRepository();
        this.providerController = new ProviderController(energyRepository);

    }

    [Test]
    public void ProduseCorrectEnergy()
    {
        List<string> providers1 = new List<string>()
        {
            "Solar","1","100"
        };
        List<string> providers2 = new List<string>()
        {
            "Solar","2","100"
        };
        this.providerController.Register(providers1);
        this.providerController.Register(providers2);

        for (int i = 0; i < 3; i++)
        {
            this.providerController.Produce();
        }

        Assert.That(this.providerController.TotalEnergyProduced,Is.EqualTo(600));
    }
    [Test]
    public void ProduseCorrectEnergy2()
    {
        List<string> providers1 = new List<string>()
        {
            "Solar","1","100"
        };
        List<string> providers2 = new List<string>()
        {
            "Solar","2","100"
        };
        this.providerController.Register(providers1);
        this.providerController.Register(providers2);

        

        Assert.That(this.providerController.Produce(), Is.EqualTo("Produced 200 energy today!"));
    }
    [Test]
    public void BrokeProvider()
    {
        List<string> providers1 = new List<string>()
        {
            "Pressure","1","100"
        };
        
        this.providerController.Register(providers1);

        for (int i = 0; i < 8; i++)
        {
            this.providerController.Produce();
        }


        Assert.That(this.providerController.Entities.Count, Is.EqualTo(0));
    }
    [Test]
    public void ProvidersGetRepaired()
    {
        List<string> providers1 = new List<string>()
        {
            "Standart","1","100"
        };

        this.providerController.Register(providers1);

        this.providerController.Repair(500);


        Assert.That(this.providerController.Entities.First().Durability, Is.EqualTo(1500));
    }
}

