using System.Collections.Generic;

namespace Falkor.Application
{
  public interface IHandlerModule
  {
    IEnumerable<IHandle<object>> TryResolve(object message);
  }
}