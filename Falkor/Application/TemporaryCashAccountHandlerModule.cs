using System;
using System.Collections.Generic;
using Falkor.EventStore;
using Falkor.Messages;

namespace Falkor.Application
{
  public class TemporaryCashAccountHandlerModule : IHandlerModule
  {
    private readonly Dictionary<Type, Func<IHandle<object>>> _handlers;

    public TemporaryCashAccountHandlerModule(IStreamStore store)
    {
      _handlers = new Dictionary<Type, Func<IHandle<object>>>
      {
        {
          typeof(OpenTemporaryCashAccount),
          () =>
          {
            var repository = new TemporaryCashAccountRepository(store);
            return new HandleAdapter<OpenTemporaryCashAccount>(
              new TemporaryCashAccountStorageHandler<OpenTemporaryCashAccount>(
                new TemporaryCashAccountHandlers(repository),
                repository,
                store));
          }
        },
        {
          typeof(CreditTemporaryCashAccount),
          () =>
          {
            var repository = new TemporaryCashAccountRepository(store);
            return new HandleAdapter<CreditTemporaryCashAccount>(
              new TemporaryCashAccountStorageHandler<CreditTemporaryCashAccount>(
                new TemporaryCashAccountHandlers(repository),
                repository,
                store));
          }
        },
        {
          typeof(DebitTemporaryCashAccount),
          () =>
          {
            var repository = new TemporaryCashAccountRepository(store);
            return new HandleAdapter<DebitTemporaryCashAccount>(
              new TemporaryCashAccountStorageHandler<DebitTemporaryCashAccount>(
                new TemporaryCashAccountHandlers(repository),
                repository,
                store));
          }
        },
        {
          typeof(TransferTemporaryCashAccount),
          () =>
          {
            var repository = new TemporaryCashAccountRepository(store);
            return new HandleAdapter<TransferTemporaryCashAccount>(
              new TemporaryCashAccountStorageHandler<TransferTemporaryCashAccount>(
                new TemporaryCashAccountHandlers(repository),
                repository,
                store));
          }
        },
        {
          typeof(CloseTemporaryCashAccount),
          () =>
          {
            var repository = new TemporaryCashAccountRepository(store);
            return new HandleAdapter<CloseTemporaryCashAccount>(
              new TemporaryCashAccountStorageHandler<CloseTemporaryCashAccount>(
                new TemporaryCashAccountHandlers(repository),
                repository,
                store));
          }
        }
      };
    }

    public IEnumerable<IHandle<object>> TryResolve(object message)
    {
      if (message == null) throw new ArgumentNullException(nameof(message));
      Func<IHandle<object>> factory;
      if (_handlers.TryGetValue(message.GetType(), out factory))
      {
        yield return factory();
      }
    }
  }
}