using System;

namespace Falkor.Application
{
  public class HandleAdapter<TMessage> : IHandle<object>
  {
    private readonly IHandle<TMessage> _next;

    public HandleAdapter(IHandle<TMessage> next)
    {
      if (next == null) throw new ArgumentNullException(nameof(next));
      _next = next;
    }

    public void Handle(object message)
    {
      if (message == null) throw new ArgumentNullException(nameof(message));
      _next.Handle((TMessage) message);
    }
  }
}