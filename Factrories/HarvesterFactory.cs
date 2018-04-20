using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class HarvesterFactory:IHarvesterFactory
{
    public IHarvester GenerateHarvester(IList<string> args)
    {
        string harvType = args[0];

        var id = int.Parse(args[1]);
        var oreOutput = double.Parse(args[2]);
        var energyReq = double.Parse(args[3]);

        Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == harvType + "Harvester");
        return (IHarvester) Activator.CreateInstance(type, id, oreOutput, energyReq);
    }

    
}