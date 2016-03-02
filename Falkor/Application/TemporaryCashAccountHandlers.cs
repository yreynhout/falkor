using Falkor.Messages;
using Falkor.Model;

namespace Falkor.Application
{
  public class TemporaryCashAccountHandlers :
    IHandle<OpenTemporaryCashAccount>,
    IHandle<CreditTemporaryCashAccount>,
    IHandle<DebitTemporaryCashAccount>,
    IHandle<TransferTemporaryCashAccount>,
    IHandle<CloseTemporaryCashAccount>
  {
    private readonly ITemporaryCashAccountRepository _repository;

    public TemporaryCashAccountHandlers(ITemporaryCashAccountRepository repository)
    {
      _repository = repository;
    }

    public void Handle(OpenTemporaryCashAccount message)
    {
      var openedAccount = new TemporaryCashAccount(
        new TemporaryCashAccountId(message.TemporaryCashAccountId),
        new OwnerId(message.OwnerId));
      _repository.Add(openedAccount);
    }

    public void Handle(CreditTemporaryCashAccount message)
    {
      var account = _repository.GetMostRecent(new TemporaryCashAccountId(message.TemporaryCashAccountId));
      account.Credit(message.Amount);
    }

    public void Handle(DebitTemporaryCashAccount message)
    {
      var account = _repository.GetMostRecent(new TemporaryCashAccountId(message.TemporaryCashAccountId));
      account.Debit(message.Amount);
    }

    public void Handle(TransferTemporaryCashAccount message)
    {
      var fromAccount = _repository.Get(new TemporaryCashAccountId(message.FromTemporaryCashAccountId));
      var toAccount = fromAccount.TransferTo(new TemporaryCashAccountId(message.ToTemporaryCashAccountId));
      _repository.Add(toAccount);
    }

    public void Handle(CloseTemporaryCashAccount message)
    {
      var closeAccount = _repository.Get(new TemporaryCashAccountId(message.CloseAccountId));
      var transferAccount = _repository.Get(new TemporaryCashAccountId(message.TransferAccountId));
      closeAccount.Close(transferAccount);
    }
  }
}