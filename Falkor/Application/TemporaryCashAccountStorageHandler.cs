using System;
using System.Linq;
using Falkor.EventStore;
using Falkor.Model;

namespace Falkor.Application
{
  public class TemporaryCashAccountStorageHandler<TMessage> : IHandle<TMessage>
  {
    private readonly IHandle<TMessage> _next;
    private readonly TemporaryCashAccountRepository _repository;
    private readonly IStreamWriter _writer;

    public TemporaryCashAccountStorageHandler(IHandle<TMessage> next, TemporaryCashAccountRepository repository, IStreamWriter writer)
    {
      if (next == null) throw new ArgumentNullException(nameof(next));
      if (repository == null) throw new ArgumentNullException(nameof(repository));
      if (writer == null) throw new ArgumentNullException(nameof(writer));
      _next = next;
      _repository = repository;
      _writer = writer;
    }

    public void Handle(TMessage message)
    {
      _next.Handle(message);

      //Smells like the wrong abstraction with all this casting ...
      var affected = _repository.UnitOfWork.SingleOrDefault(_ => ((IAggregateRootEntity)_).Recorder.Records.Any());
      if (affected == null) return;

      _writer.Append(affected.Id.ToString(), ((IAggregateRootEntity)affected).Recorder.Records);
      ((IAggregateRootEntity)affected).Recorder.Reset();
    }
  }
}