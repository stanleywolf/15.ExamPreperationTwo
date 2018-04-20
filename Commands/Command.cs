using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Command : ICommand
{
    public IList<string> Arguments { get; protected set; }

    protected Command(IList<string> args)
    {
        this.Arguments = args;
    }
    public abstract string Execute();

}
  