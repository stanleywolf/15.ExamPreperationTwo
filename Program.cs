public class Program
{
    public static void Main(string[] args)
    {
        var energyRepo = new EnergyRepository();
        var harvFactory = new HarvesterFactory();

        var harController = new HarvesterController(energyRepo, harvFactory);
        var proviController = new ProviderController(energyRepo);

        var commandInterpreter = new CommandInterpreter(harController,proviController);

        var reader = new ConsoleReader();
        var writer = new ConsoleWriter();

        Engine engine = new Engine(commandInterpreter,reader,writer);
        engine.Run();
    }
}