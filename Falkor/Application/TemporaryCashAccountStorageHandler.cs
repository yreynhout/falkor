using System;
using System.Linq;
using Falkor.EventStore;

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

      var affected = _repository.UnitOfWork.SingleOrDefault(_ => _.GetRecords().Any());
      if (affected != null)
      {
        _writer.Append(affected.Id.ToString(), affected.GetRecords());
      }
    }
  }
}