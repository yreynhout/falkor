using System;
using System.Linq;

namespace Falkor.Application
{
  public class Mediator
  {
    private readonly IHandlerModule[] _modules;

    public Mediator(params IHandlerModule[] modules)
    {
      if (modules == null) throw new ArgumentNullException(nameof(modules));
      _modules = modules;
    }

    public void Send(object message)
    {
      var handler =
        _modules.
          SelectMany(module => module.TryResolve(message)).
          SingleOrDefault();
      handler?.Handle(message);
    }
  }
}