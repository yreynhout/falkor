using System;
using System.Collections.Generic;
using Falkor.Model;

namespace Falkor.Application
{
  public static class AggregateInitializationBehavior
  {
    public static void RestoreFrom(this IAggregateRootEntity root, object record)
    {
      if (record == null) throw new ArgumentNullException(nameof(record));

      if(root.Recorder.Records.Length != 0)
        throw new InvalidOperationException("This instance has been changed and can not be initialized.");

      root.Router.Route(record);
    }

    public static void RestoreFrom(this IAggregateRootEntity root, IEnumerable<object> records)
    {
      if (records == null) throw new ArgumentNullException(nameof(records));

      if(root.Recorder.Records.Length != 0)
        throw new InvalidOperationException("This instance has been changed and can not be initialized.");

      foreach (var @event in records)
      {
        root.Router.Route(@event);
      }
    }
  }
}