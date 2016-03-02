using System;
using System.Collections.Generic;

namespace Falkor.Model
{
  public class Recorder
  {
    private readonly List<object> _records;

    public Recorder()
    {
      _records = new List<object>();
    }

    public void Record(object message)
    {
      if(message == null)
        throw new ArgumentNullException(nameof(message));

      _records.Add(message);
    }

    public void Reset()
    {
      _records.Clear();
    }

    public object[] Records => _records.ToArray();
  }
}