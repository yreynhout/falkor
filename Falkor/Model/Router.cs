using System;
using System.Collections.Generic;

namespace Falkor.Model
{
  public class Router
  {
    private readonly Dictionary<Type, Action<object>> _handlers;

    public Router()
    {
      _handlers = new Dictionary<Type, Action<object>>();
    }

    public void Configure<TRecord>(Action<TRecord> handler)
    {
      if(handler == null)
        throw new ArgumentNullException(nameof(handler));

      if(_handlers.ContainsKey(typeof(TRecord)))
        throw new InvalidOperationException($"There's already a handler for record {typeof(TRecord)}.");

      _handlers.Add(typeof(TRecord), message => handler((TRecord)message));
    }

    public void Route(object record)
    {
      if(record == null)
        throw new ArgumentNullException(nameof(record));

      Action<object> handler;
      if(_handlers.TryGetValue(record.GetType(), out handler))
      {
        handler(record);
      }
    }
  }
}