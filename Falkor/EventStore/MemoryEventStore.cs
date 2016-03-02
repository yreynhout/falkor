using System;
using System.Collections.Generic;
using System.Linq;

namespace Falkor.EventStore
{
  public class MemoryEventStore : IStreamStore
  {
    private readonly Dictionary<string, List<object>> _store;

    public MemoryEventStore()
    {
      _store = new Dictionary<string, List<object>>();
    }

    public IEnumerable<object> ReadForward(string stream)
    {
      if (stream == null) throw new ArgumentNullException(nameof(stream));
      List<object> history;
      return _store.TryGetValue(stream, out history) ? history.ToArray() : new object[0];
    }

    public IEnumerable<object> ReadBackward(string stream)
    {
      if (stream == null) throw new ArgumentNullException(nameof(stream));
      List<object> history;
      return _store.TryGetValue(stream, out history) ? history.ToArray().Reverse() : new object[0];
    }

    public void Append(string stream, IEnumerable<object> messages)
    {
      if (stream == null) throw new ArgumentNullException(nameof(stream));
      if (messages == null) throw new ArgumentNullException(nameof(messages));
      List<object> history;
      if (!_store.TryGetValue(stream, out history))
      {
        _store.Add(stream, messages.ToList());
      }
      else
      {
        history.AddRange(messages);
      }
    }
  }
}