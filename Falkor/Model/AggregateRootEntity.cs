using System;
using System.Collections.Generic;

namespace Falkor.Model
{
  public abstract class AggregateRootEntity
  {
    private readonly Router _router = new Router();
    private readonly Recorder _recorder = new Recorder();

    protected void Configure<TRecord>(Action<TRecord> handler)
    {
      _router.Configure(handler);
    }

    protected void Apply(object record)
    {
      _router.Route(record);
      _recorder.Record(record);
    }

    public void RestoreFrom(object record)
    {
      if (record == null) throw new ArgumentNullException(nameof(record));

      if(_recorder.Records.Length != 0)
        throw new InvalidOperationException("This instance has been changed and can not be initialized.");

      _router.Route(record);
    }

    public void RestoreFrom(IEnumerable<object> records)
    {
      if (records == null) throw new ArgumentNullException(nameof(records));

      if(_recorder.Records.Length != 0)
        throw new InvalidOperationException("This instance has been changed and can not be initialized.");

      foreach (var @event in records)
      {
        _router.Route(@event);
      }
    }

    public object[] GetRecords()
    {
      return _recorder.Records;
    }

    public void ResetRecords()
    {
      _recorder.Reset();
    }
  }
}