using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public class CommandInterpreter : ICommandInterpreter
{
    public IHarvesterController HarvesterController { get; private set; }

    public IProviderController ProviderController { get; private set; }

    public CommandInterpreter(IHarvesterController harvesterController, IProviderController providerController)
    {
        this.HarvesterController = harvesterController;
        this.ProviderController = providerController;
    }

    public string ProcessCommand(IList<string> args)
    {
        ICommand command = this.CreateCommand(args);
        string result = command.Execute();
        return result;
    }

    private ICommand CreateCommand(IList<string> args)
    {
        string commandName = args[0];
        Type commandType = Assembly.GetCallingAssembly().GetTypes()
            .FirstOrDefault(t => t.Name == commandName + "Command");
        if (commandType == null)
        {
            throw new ArgumentException(string.Format(Constants.CommandNotFound,commandName));
        }
        if (!typeof(ICommand).IsAssignableFrom(commandType))
        {
            throw new ArgumentException(string.Format(Constants.InvalidCommand,commandType));
        }
        ConstructorInfo ctor = commandType.GetConstructors().First();
        ParameterInfo[] parInfo = ctor.GetParameters();
        object[] parameters = new object[parInfo.Length];
        for (int i = 0; i < parInfo.Length; i++)
        {
            Type paramType = parInfo[i].ParameterType;
            if (paramType == typeof(IList<string>))
            {
                parameters[i] = args.Skip(1).ToList();
            }
            else
            {
                PropertyInfo propertyInfo =
                    this.GetType().GetProperties().FirstOrDefault(p => p.PropertyType == paramType);
                parameters[i] = propertyInfo.GetValue(this);
            }
        }
        ICommand instance = (ICommand) Activator.CreateInstance(commandType, parameters);
        return instance;
    }
}
  