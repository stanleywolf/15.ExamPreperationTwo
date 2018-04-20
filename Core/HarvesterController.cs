using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HarvesterController : IHarvesterController
{
    private const string DefaultMode = "Full";

    private string mode;
    private IEnergyRepository energyRepository;
    private IList<IHarvester> harvesters;
    public double OreProduced { get; private set; }
    private IHarvesterFactory factory;
    public IReadOnlyCollection<IEntity> Entities => (IReadOnlyCollection<IEntity>) this.harvesters;

    public HarvesterController(IEnergyRepository repository, IHarvesterFactory factory)
    {
        this.mode = DefaultMode;
        this.energyRepository = repository;
        this.harvesters = new List<IHarvester>();
        this.factory = factory;
    }
    public string Register(IList<string> args)
    {
        var harvester = this.factory.GenerateHarvester(args);
        this.harvesters.Add(harvester);

        return string.Format(Constants.SuccessfullRegistration,
            harvester.GetType().Name);
    }

    public string Produce()
    {
        //calculate needed energy
        double neededEnergy = 0;
        foreach (var harvester in this.harvesters)
        {
            if (this.mode == "Full")
            {
                neededEnergy += harvester.EnergyRequirement;
            }
            else if (this.mode == "Half")
            {
                neededEnergy += harvester.EnergyRequirement * 50 / 100;
            }
            else if (this.mode == "Energy")
            {
                neededEnergy += harvester.EnergyRequirement * 20 / 100;
            }
        }

        //check if we can mine
        double minedOres = 0;
        if (this.energyRepository.TakeEnergy(neededEnergy))
        {
            //mine
            foreach (var harvester in harvesters)
            {
                minedOres += harvester.Produce();
            }
        }

        //take the mode in mind
        if (this.mode == "Energy")
        {
            minedOres = minedOres * 20 / 100; 
        }
        else if (this.mode == "Half")
        {
            minedOres = minedOres * 50 / 100;
        }
        
        this.OreProduced += minedOres;

        return string.Format(Constants.OreOutputToday, minedOres);
    }

    

    public string ChangeMode(string mode)
    {
        this.mode = mode;
        List<IHarvester> reminder = new List<IHarvester>();

        foreach (var harvester in this.harvesters)
        {
            try
            {
                harvester.Broke();
            }
            catch (Exception ex)
            {
                reminder.Add(harvester);
            }
        }

        foreach (var entity in reminder)
        {
            this.harvesters.Remove(entity);
        }
        return string.Format(Constants.ChangeMode, mode);
    }
}
   
