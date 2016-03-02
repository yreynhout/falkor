using System;
using Falkor.Application;
using Falkor.EventStore;
using Falkor.Messages;

namespace Falkor
{
  class Program
  {
    static void Main(string[] args)
    {
      try
      {
        var store = new MemoryEventStore();
        var module = new TemporaryCashAccountHandlerModule(store);
        var mediator = new Mediator(ResolveHandlers.Using(module));

        var account1 = Guid.NewGuid();
        var account2 = Guid.NewGuid();
        var owner = Guid.NewGuid();
        mediator.Send(new OpenTemporaryCashAccount(account1, owner));
        mediator.Send(new CreditTemporaryCashAccount(account1, 100));
        mediator.Send(new DebitTemporaryCashAccount(account1, 50));
        mediator.Send(new DebitTemporaryCashAccount(account1, 25));
        mediator.Send(new TransferTemporaryCashAccount(account1, account2));
        mediator.Send(new CloseTemporaryCashAccount(account1, account2));

        //Forwards to account2 if all goes well.
        mediator.Send(new DebitTemporaryCashAccount(account1, 25));
        mediator.Send(new CreditTemporaryCashAccount(account1, 100));

        Console.WriteLine("OK");
      } catch(Exception e) {
        Console.WriteLine(e.ToString());
      }
    }
  }
}


