using System;
using System.Linq;

namespace Falkor.Application
{
  public static class ResolveHandlers
  {
    public static HandlerResolver Using(params IHandlerModule[] modules)
    {
      if (modules == null)
        throw new ArgumentNullException(nameof(modules));

      return message =>
      {
        return modules.
          SelectMany(module => module.TryResolve(message)).
          SingleOrDefault();
      };
    }
  }
}