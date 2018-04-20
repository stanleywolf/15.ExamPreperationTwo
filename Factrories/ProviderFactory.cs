using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

public class ProviderFactory : IProviderFactory
{
    public IProvider GenerateProvider(IList<string> args)
    {
        string provType = args[0];
        int id = int.Parse(args[1]);
        double energyOutput = double.Parse(args[2]);

        Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == provType + "Provider");
        return (IProvider) Activator.CreateInstance(type, id, energyOutput);
    }
}