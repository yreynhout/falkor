using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Falkor.EventStore
{
  public class MemoryEventStore : IStreamStore
  {
    private readonly ConcurrentDictionary<string, object[]> _store;

    public MemoryEventStore()
    {
      _store = new ConcurrentDictionary<string, object[]>();
    }

    public IEnumerable<object> ReadForward(string stream)
    {
      if (stream == null) throw new ArgumentNullException(nameof(stream));
      object[] history;
      return _store.TryGetValue(stream, out history) ? history : new object[0];
    }

    public IEnumerable<object> ReadBackward(string stream)
    {
      if (stream == null) throw new ArgumentNullException(nameof(stream));
      object[] history;
      return _store.TryGetValue(stream, out history) ? history.Reverse() : new object[0];
    }

    public void Append(string stream, IEnumerable<object> messages)
    {
      if (stream == null) throw new ArgumentNullException(nameof(stream));
      if (messages == null) throw new ArgumentNullException(nameof(messages));
      var snapshot = messages.ToArray();
      _store.AddOrUpdate(stream,
        snapshot,
        (_, history) =>
        {
          var copy = new object[history.Length + snapshot.Length];
          history.CopyTo(copy, 0);
          snapshot.CopyTo(copy, history.Length);
          return copy;
        });
    }
  }
}