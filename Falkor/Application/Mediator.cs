using System;

namespace Falkor.Application
{
  public class Mediator
  {
    private readonly HandlerResolver _resolver;

    public Mediator(HandlerResolver resolver)
    {
      if (resolver == null)
        throw new ArgumentNullException(nameof(resolver));
      _resolver = resolver;
    }

    public void Send(object message)
    {
      if (message == null) throw new ArgumentNullException(nameof(message));
      var handler = _resolver(message);
      if (handler == null)
        throw new InvalidOperationException($"The handler for {message.GetType().Name} is missing.");
      handler.Handle(message);
    }
  }
}